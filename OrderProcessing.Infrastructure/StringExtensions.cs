using System.Text.RegularExpressions;

namespace OrderProcessing.Infrastructure
{
	public static class StringExtensions
	{
		public static string FormatCreditCard(this string creditCardNumber)
		{
			if (string.IsNullOrEmpty(creditCardNumber))
				return creditCardNumber;

			return Regex.Replace(creditCardNumber, ".{4}", "$0 ").Trim().Replace(" ", "-");
		}

		public static string RemoveCreditCardFormatting(this string creditCardNumber)
		{
			if (string.IsNullOrEmpty(creditCardNumber))
				return creditCardNumber;

			return creditCardNumber.Replace(" ", "").Replace("-", "");
		}
	}
}
