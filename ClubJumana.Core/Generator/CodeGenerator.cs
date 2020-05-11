using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.Core.Generator
{
   public class CodeGenerator
    {
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
