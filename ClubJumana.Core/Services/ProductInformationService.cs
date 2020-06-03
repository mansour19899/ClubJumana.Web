using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ClubJumana.Core.Convertors;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Services.Interfaces;
using ClubJumana.DataLayer.Context;
using ClubJumana.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ClubJumana.Core.Services
{
  public  class ProductInformationService: IProductInformationService
    {
        private JummanaContext _context;
        public ProductInformationService()
        {
            _context = new JummanaContext();
        }
        public int AddTowel(AddTowelInformationViewModel TowelInformation)
        {


            if (TowelInformation.company.Id == 0)
            {
                int CompanyId = 1;
                if (EnumerableExtensions.Any(_context.Productw))
                {
                    TowelInformation.company.Id = _context.Companies.Max(x => x.Id) + 1;
                }

                _context.Companies.Add(TowelInformation.company);

            }


            int Id = 1;
            if (EnumerableExtensions.Any(_context.Productw))
            {
                Id = _context.Productw.Max(x => x.Id)+1;
            }

            _context.Productw.Add(new Product()
            {
                Id = Id,
                StyleNumber = TowelInformation.Product.StyleNumber,
                BrandFK = TowelInformation.Product.BrandFK,
                MaterialFK = TowelInformation.Product.MaterialFK,
                CompanyFK = TowelInformation.company.Id,
                CountryOfOrginFK = TowelInformation.Product.CountryOfOrginFK,
                DescribeMaterial = TowelInformation.Product.DescribeMaterial,
                ProductTittle = TowelInformation.Product.ProductTittle,

            });

            int TowelId = 1;
            if (EnumerableExtensions.Any(_context.Towels))
            {
                TowelId = _context.Towels.Max(x => x.Id) + 1;
            }

            foreach (var VARIABLE in TowelInformation.Towels)
            {
                _context.Towels.Add(new Towel()
                {
                    Id = TowelId,
                    ProductFK = Id,
                    ColourFK = VARIABLE.ColourFK,
                    ProductTypeFK = VARIABLE.ProductTypeFK,
                    Price = VARIABLE.Price,
                    Width = VARIABLE.Width,
                    length = VARIABLE.length,
                    Size = VARIABLE.Size,
                    Note = VARIABLE.Note,
                    LastDateEdited = DateTime.Now,
                    Gsm = VARIABLE.Gsm
                });
                TowelId++;
            }

            _context.SaveChanges();
            return 1;
        }

        public string GiveMeStyleNumber(int Category, int SubCategory)
        {
            var CategoryStyleCode = _context.Categories.SingleOrDefault(p => p.Id == Category).StyleNum_code;
            var SubCategoryCode = _context.SubCategories.SingleOrDefault(p => p.Id == SubCategory).Code;

            var tt = _context.Productw.ToList().LastOrDefault();
            string LastStyleNumber = "";
            if (tt == null)
            {
                LastStyleNumber = "11.02.007870";
            }
            else
            {
                LastStyleNumber = tt.StyleNumber.Trim();
            }

            var qt = LastStyleNumber.ToCharArray().Skip(6).ToArray();
            var qtt = qt.SkipWhile(p => p.CompareTo('0') == 0).ToArray();
            var qttt = new string(qtt);
            var newStyleNumber = Convert.ToDecimal(qttt) + 1;
            return CategoryStyleCode + "." + SubCategoryCode + "." + newStyleNumber.ToString().NumtoStr(6); ;
        }

        public Company ExistCompany(string CompanyName)
        {
            return _context.Companies.FirstOrDefault(p => p.CompanyName.ToLower().CompareTo(CompanyName.ToLower()) == 0);
        }

        public List<VariantViewModel> AllVariantList()
        {
           List<VariantViewModel> list=new List<VariantViewModel>();

           var towelList = _context.Towels.Include(p=>p.Product).Include(p=>p.ProductType).ThenInclude(p=>p.CategoriesSubCategory).Include(p=>p.Colour).ToList();

           foreach (var VARIABLE in towelList)
           {
               list.Add(new VariantViewModel()
               {
                   Id = VARIABLE.Id,
                   Sku = VARIABLE.Sku,
                   ProductFK = VARIABLE.ProductFK,
                   ColourFK = VARIABLE.ColourFK,
                   BarcodeFK = VARIABLE.BarcodeFK,
                   ProductTypeFK = VARIABLE.ProductTypeFK,
                   Price = VARIABLE.Price,
                   Size = VARIABLE.Size,
                   Product = VARIABLE.Product,
                   Colour = VARIABLE.Colour,
                   Barcode = VARIABLE.Barcode,
                   ProductType = VARIABLE.ProductType
               });
           }
           
           //Add another Variant 

           return list;
        }

        public Product GiveMeProductWithId(int Id)
        {
            var product = _context.Productw.Include(p=>p.Company).Include(p=>p.Material)
                .Include(p=>p.Brand).Include(p=>p.Towels).ThenInclude(p=>p.ProductType)
                .ThenInclude(p=>p.CategoriesSubCategory).ThenInclude(p=>p.Category).Include(p => p.Towels).ThenInclude(p => p.ProductType)
                .ThenInclude(p => p.CategoriesSubCategory).ThenInclude(p => p.SubCategory).Include(p=>p.CountryOfOrgin).FirstOrDefault(p => p.Id == Id);



            return product;
        }
    }
}
