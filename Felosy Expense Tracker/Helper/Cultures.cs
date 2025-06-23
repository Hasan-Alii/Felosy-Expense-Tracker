using System.Globalization;

namespace Felosy_Expense_Tracker.Helper
{
    public class Cultures
    {
        static Cultures()
        {
            EGY = (CultureInfo)CultureInfo.GetCultureInfo("en-EG").Clone();
            EGY.NumberFormat.CurrencyPositivePattern = 2;
            EGY.NumberFormat.CurrencyNegativePattern = 12;
        }
        public static readonly CultureInfo EGY =
            CultureInfo.GetCultureInfo("en-EG");

        public static readonly CultureInfo USA =
            CultureInfo.GetCultureInfo("en-US");

        public static readonly CultureInfo UK =
            CultureInfo.GetCultureInfo("en-UK");

        public static readonly CultureInfo KSA =
            CultureInfo.GetCultureInfo("en-SA");
    }
}
