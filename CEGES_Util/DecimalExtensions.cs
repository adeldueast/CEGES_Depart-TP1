using DecimalMath;
using System;
using System.Collections.Generic;
using System.Text;

namespace CEGES_Util
{
    public static class DecimalExtensions
    {        
        public static string RoundToSignificantDigits(this decimal d, int digits)
        {
            if (d == 0)
                return "0";
            
            decimal scale = DecimalEx.Pow(10, DecimalEx.Floor(DecimalEx.Log10(Math.Abs(d))) + 1);
            return (scale * Decimal.Round(d / scale, digits)).ToString("G29");
        }

        public static string RoundToSignificantDigitsAndUnit(this decimal d, int digits)
        {
            if (d == 0)
                return "0";

            decimal pow = DecimalEx.Floor(DecimalEx.Log10(Math.Abs(d)));
            decimal sub = pow % 3;
            decimal unit = (pow - sub) / 3;
            decimal scale = DecimalEx.Pow(10, unit * 3);
            int round = Math.Max((int)(2 - pow), 0);
            return (Decimal.Round(d / scale, round)).ToString() + SelectUnit(unit);
        }

        private static string SelectUnit(decimal unit)
        {
            return unit switch
            {
                -3 => "n",
                -2 => "μ",
                -1 => "m",
                0 => "",
                1 => "k",
                2 => "M",
                3 => "G",
                _ => ""
            };
        }
    }
}
