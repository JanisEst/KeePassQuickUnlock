using KeePass.App.Configuration;
using KeePassLib.Keys;
using KeePassLib.Security;
using KeePassLib.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace KeePassQuickUnlock
{
	public static class Extensions
	{
		public static ProtectedBinary CombineKeys(this CompositeKey key)
		{
			var dataList = new List<byte[]>();
			int dataLength = 0;
			foreach (var pKey in key.UserKeys)
			{
				var b = pKey.KeyData;
				if (b != null)
				{
					var keyData = b.ReadData();
					dataList.Add(keyData);
					dataLength += keyData.Length;
				}
			}

			var allData = new byte[dataLength];
			int p = 0;
			foreach (var pbData in dataList)
			{
				Array.Copy(pbData, 0, allData, p, pbData.Length);
				p += pbData.Length;
				MemUtil.ZeroByteArray(pbData);
			}

			var pb = new ProtectedBinary(true, allData);
			MemUtil.ZeroByteArray(allData);
			return pb;
		}

		public static char[] GetChars(this ProtectedString ps)
		{
			var pb = ps.ReadUtf8();

			var chars = StrUtil.Utf8.GetChars(pb);

			MemUtil.ZeroByteArray(pb);

			return chars;
		}

		public static ProtectedString ToProtectedString(this char[] chars)
		{
			return chars.ToProtectedString(0, chars.Length);
		}

		public static ProtectedString ToProtectedString(this char[] chars, int charIndex, int charCount)
		{
			var pb = StrUtil.Utf8.GetBytes(chars, charIndex, charCount);
			var ps = new ProtectedString(true, pb);
			MemUtil.ZeroByteArray(pb);
			return ps;
		}

		public static void SetEnum<T>(this AceCustomConfig config, string strID, T eValue) where T : struct, IConvertible
		{
			Contract.Requires(typeof(T).IsEnum);

			config.SetLong(strID, (int)(object)eValue);
		}

		public static T GetEnum<T>(this AceCustomConfig config, string strID, T eDefault) where T : struct, IConvertible
		{
			Contract.Requires(typeof(T).IsEnum);

			var value = config.GetLong(strID, -1);
			if (value == -1)
			{
				return eDefault;
			}

			return (T)(object)(int)value;
		}
	}
}
