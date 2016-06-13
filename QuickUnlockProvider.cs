using KeePassLib.Keys;
using KeePassLib.Utility;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KeePassQuickUnlock
{
	/// <summary>Where to check for the QuickUnlock part.</summary>
	public enum QuickUnlockWhere
	{
		Front,
		Back
	}

	public class QuickUnlockProvider : KeyProvider
	{
		/// <summary>Active settings name</summary>
		public const string CfgActive = KeePassQuickUnlockExt.ShortProductName + "_Active";
		/// <summary>Where settings name</summary>
		public const string CfgWhere = KeePassQuickUnlockExt.ShortProductName + "_Where";
		/// <summary>NumChars settings name</summary>
		public const string CfgNumChars = KeePassQuickUnlockExt.ShortProductName + "_NumChars";

		/// <summary>Maps database paths to cached passwords</summary>
		private Dictionary<string, KcpPassword> unlockCache;

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

		public bool HasCachedKeys
		{
			get { return unlockCache.Count != 0; }
		}

		public QuickUnlockProvider()
		{
			unlockCache = new Dictionary<string, KcpPassword>();
		}

		/// <summary>Adds a database-key mapping.</summary>
		/// <param name="databasePath">Full path of the database file.</param>
		/// <param name="password">The password to cache.</param>
		public void AddCachedKey(string databasePath, KcpPassword password)
		{
			unlockCache[databasePath] = password;
		}

		/// <summary>Clears the cache.</summary>
		public void ClearCache()
		{
			unlockCache.Clear();
		}

		public override byte[] GetKey(KeyProviderQueryContext ctx)
		{
			if (ctx.CreatingNewKey)
			{
				MessageService.ShowWarning("Can't use QuickUnlock to create new keys.");

				return null;
			}

			if (KeePassQuickUnlockExt.Host.CustomConfig.GetBool(CfgActive, false) == false)
			{
				MessageService.ShowWarning("QuickUnlock is not active.");

				return null;
			}

			KcpPassword cachePassword;
			if (unlockCache.TryGetValue(ctx.DatabasePath, out cachePassword) == false)
			{
				MessageService.ShowWarning("QuickUnlock is not available for this database.");

				return null;
			}

			var mode = (QuickUnlockWhere)KeePassQuickUnlockExt.Host.CustomConfig.GetLong(CfgWhere, (long)QuickUnlockWhere.Front);
			var numChars = Math.Max(3, KeePassQuickUnlockExt.Host.CustomConfig.GetULong(CfgNumChars, 3));

			using (QuickUnlockPromptForm quof = new QuickUnlockPromptForm(mode, numChars))
			{
				if (quof.ShowDialog() != DialogResult.OK)
				{
					return null;
				}

				var quickPassword = quof.QuickPassword;
				if ((ulong)quickPassword.Length != numChars)
				{
					MessageService.ShowWarning("The QuickUnlock password hasn't the needed length.");

					return null;
				}

				var password = cachePassword.Password.ReadString();
				Func<bool> cmp;
				if (mode == QuickUnlockWhere.Front)
				{
					cmp = () => password.StartsWith(quickPassword);
				}
				else
				{
					cmp = () => password.EndsWith(quickPassword);
				}

				if (!cmp())
				{
					//remove the cache entry
					unlockCache.Remove(ctx.DatabasePath);

					//return dummy password to let KeePass fail while loading the database
					return new byte[] { 0 };
				}

				return cachePassword.KeyData.ReadData();
			}
		}
	}
}
