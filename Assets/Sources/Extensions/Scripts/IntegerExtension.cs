using System.Globalization;

namespace Sources.Extensions.Scripts
{
    public static class IntegerExtension
    {
        public static string FormatPrice(this int price)
        {
            return price.ToString("N0", new CultureInfo("en-US")).Replace(",", " ");
        }
    }
}