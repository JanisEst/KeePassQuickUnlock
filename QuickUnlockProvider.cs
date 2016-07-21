using KeePassLib.Keys;
using KeePassLib.Utility;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using KeePassLib.Security;
using System.Linq;

namespace KeePassQuickUnlock
{
	/// <summary>Values that represent QuickUnlock modes.</summary>
	enum Mode
	{
		Entry,
		EntryOrPartOf,

		Default = Entry
	}

	/// <summary>Values that represent PartOf origins.</summary>
	enum PartOfOrigin
	{
		Front,
		End,

		Default = Front
	}

	public class QuickUnlockProvider : KeyProvider
	{
		/// <summary>Mode setting name</summary>
		public const string CfgMode = KeePassQuickUnlockExt.ShortProductName + "_Mode";
		/// <summary>Auto Prompt setting name</summary>
		public const string CfgAutoPrompt = KeePassQuickUnlockExt.ShortProductName + "_AutoPrompt";
		/// <summary>PIN setting name</summary>
		public const string CfgValidPeriod = KeePassQuickUnlockExt.ShortProductName + "_ValidPeriod";
		/// <summary>PartOf Origin setting name</summary>
		public const string CfgPartOfOrigin = KeePassQuickUnlockExt.ShortProductName + "_PartOfOrigin";
		/// <summary>PartOf Length setting name</summary>
		public const string CfgPartOfLength = KeePassQuickUnlockExt.ShortProductName + "_PartOfLength";
		/// <summary>The minimum PartOf length.</summary>
		public const int MinimumPartOfLength = 2;

		/// <summary>The valid periods in seconds.</summary>
		public const ulong VALID_UNLIMITED = 0;
		public const ulong VALID_1MINUTE = 60;
		public const ulong VALID_5MINUTES = VALID_1MINUTE * 5;
		public const ulong VALID_10MINUTES = VALID_5MINUTES * 2;
		public const ulong VALID_15MINUTES = VALID_5MINUTES * 3;
		public const ulong VALID_30MINUTES = VALID_15MINUTES * 2;
		public const ulong VALID_1HOUR = VALID_30MINUTES * 2;
		public const ulong VALID_2HOURS = VALID_1HOUR * 2;
		public const ulong VALID_6HOURS = VALID_2HOURS * 3;
		public const ulong VALID_12HOURS = VALID_6HOURS * 2;
		public const ulong VALID_1DAY = VALID_12HOURS * 2;
		public const ulong VALID_DEFAULT = VALID_10MINUTES;

		private class QuickUnlockData
		{
			public DateTime ValidUntil;
			public ProtectedString Pin;
			public ProtectedBinary ComposedKey;

			public bool IsValid()
			{
				return ValidUntil >= DateTime.Now;
			}
		}

		/// <summary>Maps database paths to cached passwords</summary>
		private Dictionary<string, QuickUnlockData> unlockCache;

		public override string Name
		{
			get { return KeePassQuickUnlockExt.ShortProductName; }
		}

		public override bool DirectKey
		{
			get { return true; }
		}

		public override bool SecureDesktopCompatible
		{
			get { return true; }
		}

		public QuickUnlockProvider()
		{
			unlockCache = new Dictionary<string, QuickUnlockData>();
		}

		/// <summary>Adds a database-key mapping.</summary>
		/// <param name="databasePath">Full path of the database file.</param>
		/// <param name="pin">The pin to unlock the database.</param>
		/// <param name="keys">The keys used to encrypt the database.</param>
		public void AddCachedKey(string databasePath, ProtectedString pin, CompositeKey keys)
		{
			Contract.Requires(!string.IsNullOrEmpty(databasePath));
			Contract.Requires(pin != null);
			Contract.Requires(keys != null);

			var validPeriod = KeePassQuickUnlockExt.Host.CustomConfig.GetULong(CfgValidPeriod, VALID_DEFAULT);

			lock (unlockCache)
			{
				unlockCache[databasePath] = new QuickUnlockData
				{
					ValidUntil = validPeriod == VALID_UNLIMITED ? DateTime.MaxValue : DateTime.Now.AddSeconds(validPeriod),
					Pin = pin.WithProtection(true),
					ComposedKey = keys.CombineKeys()
				};
			}
		}

		/// <summary>Removes the cached key associated with the path of the database.</summary>
		/// <param name="databasePath">Full path of the database file.</param>
		public void RemoveCachedKey(string databasePath)
		{
			lock (unlockCache)
			{
				unlockCache.Remove(databasePath);
			}
		}

		/// <summary>Attempts to get cached key from the given key.</summary>
		/// <param name="databasePath">Full path of the database file.</param>
		/// <param name="data">[out] The data.</param>
		/// <returns>true if it succeeds, false if it fails.</returns>
		private bool TryGetCachedKey(string databasePath, out QuickUnlockData data)
		{
			lock (unlockCache)
			{
				return unlockCache.TryGetValue(databasePath, out data);
			}
		}

		/// <summary>Query if the path of the database has a cached key.</summary>
		/// <param name="databasePath">Full path of the database file.</param>
		/// <returns>true if a cached key exists, false if not.</returns>
		public bool IsCachedKey(string databasePath)
		{
			QuickUnlockData temp;
			return TryGetCachedKey(databasePath, out temp);
		}

		/// <summary>Clears the expiered keys.</summary>
		public void ClearExpieredKeys()
		{
			lock (unlockCache)
			{
				foreach (var key in unlockCache.Where(kv => kv.Value.IsValid() == false).Select(kv => kv.Key).ToList())
				{
					unlockCache.Remove(key);
				}
			}
		}

		/// <summary>Clears the cache.</summary>
		public void ClearCache()
		{
			lock (unlockCache)
			{
				unlockCache.Clear();
			}
		}

		public override byte[] GetKey(KeyProviderQueryContext ctx)
		{
			if (ctx.CreatingNewKey)
			{
				MessageService.ShowWarning("Can't use QuickUnlock to create new keys.");

				return null;
			}

			QuickUnlockData data;
			if (TryGetCachedKey(ctx.DatabasePath, out data) == false ||
				data.IsValid() == false)
			{
				MessageService.ShowWarning("QuickUnlock is not available for this database.");

				return null;
			}

			using (QuickUnlockPromptForm quof = new QuickUnlockPromptForm())
			{
				if (quof.ShowDialog() != DialogResult.OK)
				{
					return null;
				}

				var pb = data.Pin.ReadUtf8();
				var same = MemUtil.ArraysEqual(pb, StrUtil.Utf8.GetBytes(quof.QuickPassword));
				MemUtil.ZeroByteArray(pb);

				if (same == false)
				{
					//remove the cache entry
					RemoveCachedKey(ctx.DatabasePath);

					//return dummy password to let KeePass fail while loading the database
					return new byte[] { 0 };
				}

				return data.ComposedKey.ReadData();
			}
		}
	}
}
