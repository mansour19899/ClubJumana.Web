using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClubJumana.Core.Convertors
{
  public static class ConvertNumberToString
    {
        public static string NumtoStr(this string str, int count)
        {
            var stringcount = str.Count();
            int dif = count - stringcount;
            for (int i = 0; i < dif; i++)
            {
                str = "0" + str;
            }
            return str;
        }
    }
}
