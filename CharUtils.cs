using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace KeePassQuickUnlock
{
	class CharUtils
	{
		/// <summary>Zero out a character array.</summary>
		/// <param name="pcArray">Array of characters.</param>
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
