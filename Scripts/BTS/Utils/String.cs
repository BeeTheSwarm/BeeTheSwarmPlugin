using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomExtensions {
	public static class String {
		public static string FormatNumber(this string numString) {
			long num = long.Parse (numString);
			long i = (long)Mathf.Pow(10, (int)Mathf.Max(0, Mathf.Log10(num) - 2));
			num = num / i * i;

			if (num >= 1000000000)
				return (num / 1000000000D).ToString("0.##") + "B";
			if (num >= 1000000)
				return (num / 1000000D).ToString("0.##") + "M";
			if (num >= 1000)
				return (num / 1000D).ToString("0.##") + "K";

			return num.ToString("#,0");
		}
	}
}
