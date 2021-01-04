using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ClubJumana.Core.Convertors
{
   public static class FixNumeric
    {
        public static string RoundNumtoStr(this decimal num)
        {
            var str = num.ToString().Split('.');
            if (str[1][3] == '0')
                return str[0] + "." + str[1][0] + str[1][1];
            else if(str[1][4] == '0')
            {
                return str[0] + "." + str[1][0] + str[1][1]+ str[1][2];
            }
            else
            {
                return str[0] + "." + str[1];
            }

        }
    }
}
