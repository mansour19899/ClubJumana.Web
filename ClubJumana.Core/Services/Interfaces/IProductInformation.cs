using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.Core.DTOs;

namespace ClubJumana.Core.Services.Interfaces
{
    interface IProductInformationService
    {
        public int AddTowel(AddTowelInformationViewModel product);
    }
}
