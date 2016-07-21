using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace KeePassQuickUnlock
{
	class CharUtils
	{
#if KeePassLibSD
		[MethodImpl(MethodImplOptions.NoInlining)]
#else
		[MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
#endif
		public static void ZeroCharArray(char[] pcArray)
		{
			Debug.Assert(pcArray != null);
			if (pcArray == null) throw new ArgumentNullException("pcArray");

			Array.Clear(pcArray, 0, pcArray.Length);
		}
	}
}
