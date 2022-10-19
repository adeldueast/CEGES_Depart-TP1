using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEGES_Util
{    public static class StringExtension
    {
        public static string CapitalizeFirst(this string s)
        {
            return string.Join(" ", 
                s.Split(' ')
                    .ToList()
                    .ConvertAll(word =>
                        word.Substring(0, 1).ToUpper() + word.Substring(1)
                    )
                );
        }
    }
}
