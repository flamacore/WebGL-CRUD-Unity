using System.Collections;
using System.Linq;
using UnityEngine;

namespace UnityCRUD.Scripts.Plugins
{
	public static class ExtensionMethods
	{
		public static int ToInt(this string trans)
		{
			int.TryParse(trans, out var outer);
			return outer;
		}

		public static int ExtractToInt(this string input)
		{
			return input.ToDigitsOnly().ToInt();
		}

		private static string ToDigitsOnly(this string input)
		{
			return string.Concat(input.ToArray().Reverse().TakeWhile(char.IsNumber).Reverse());
		}

		public static IEnumerator DelayedCoroutine(this MonoBehaviour mb, float delay, System.Action a)
		{
			yield return new WaitForSeconds(delay);
			a();
		}

		public static Coroutine RunDelayed(this MonoBehaviour mb, float delay, System.Action a)
		{
			return mb.StartCoroutine(mb.DelayedCoroutine(delay, a));
		}
	}
}