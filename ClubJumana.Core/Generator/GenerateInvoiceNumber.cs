using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.Core.Generator
{
  public  static class GenerateInvoiceNumber
    {
        public static string ShowPoNumber(this int Id)
        {

            return "65- " + Id.ToString();
        }
        public static string ShowAsnNumber(this int Id)
        {

            return "76- " + Id.ToString();
        }
        public static string ShowGrnNumber(this int Id)
        {

            return "97- " + Id.ToString();
        }
    }
}
