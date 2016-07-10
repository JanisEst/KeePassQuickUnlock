using KeePassLib.Keys;
using KeePassLib.Security;
using KeePassLib.Utility;
using System;
using System.Collections.Generic;

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
	}
}
