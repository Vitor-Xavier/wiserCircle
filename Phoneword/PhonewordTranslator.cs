using System.Text;

namespace Core
{
	public static class PhonewordTranslator
	{
		
		public static string ToNumber(string raw)
		{
			if (string.IsNullOrWhiteSpace(raw))
			{
				return null;
			}
			raw = raw.ToUpperInvariant();

			var newNumber = new StringBuilder();
			foreach (var c in raw)
			{
				if (" -0123456789".Contains(c))
				{
					newNumber.Append(c);
				}
				else {
					var result = TranslateToNumber(c);
					if (result != null)
					{
						newNumber.Append(result);
					}
					else {
						return null;
					}
				}
			}
			return newNumber.ToString();
		}


		/*	
		 * Extension methods are defined as static methods but are called by using instance method syntax.
		 * Their first parameter specifies which type the method operates on, and the parameter is preceded by the this modifier.
		 * Extension methods are only in scope when you explicitly import the namespace into your source code with a using directive
		*/
		static bool Contains(this string keyString, char c)
		{
			return keyString.IndexOf(c) >= 0;
		}

		static readonly string[] digits = {
			"ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
		};

		static int? TranslateToNumber(char c)
		{
			for (int i = 0; i < digits.Length; i++)
			{
				if (digits[i].Contains(c))
				{
					return 2 + i;
				}
			}
			return null;
		}
	}
}