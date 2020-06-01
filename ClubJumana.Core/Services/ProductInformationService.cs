using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Services.Interfaces;
using ClubJumana.DataLayer.Context;
using ClubJumana.DataLayer.Entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace ClubJumana.Core.Services
{
    class ProductInformationService: IProductInformationService
    {
        private JummanaContext _context;
        public ProductInformationService()
        {
            _context = new JummanaContext();
        }
        public int AddTowel(AddTowelInformationViewModel TowelInformation)
        {
            int Id = 1;
            if (EnumerableExtensions.Any(_context.Productw))
            {
                Id = _context.Productw.Max(x => x.Id);
            }

            _context.Productw.Add(new Product()
            {
                StyleNumber = TowelInformation.Product.StyleNumber,
                BrandFK = TowelInformation.Product.BrandFK,
                MaterialFK = TowelInformation.Product.MaterialFK,
                CompanyFK = TowelInformation.Product.CompanyFK,
                CountryOfOrginFK = TowelInformation.Product.CountryOfOrginFK,
                DescribeMaterial = TowelInformation.Product.DescribeMaterial,
                ProductTittle = TowelInformation.Product.ProductTittle,

            });

            foreach (var VARIABLE in TowelInformation.Towels)
            {
               //_context.Towels.Add(new Towel())
               //{

               //} 
            }

            _context.SaveChanges();
            return 1;
        }
    }
}
