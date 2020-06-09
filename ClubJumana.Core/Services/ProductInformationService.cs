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
    public class ProductInformationService : IProductInformationService
    {
        private JummanaContext _context;
        public ProductInformationService()
        {
            _context = new JummanaContext();
        }
        public int AddTowel(AddVariantInformationViewModel TowelInformation)
        {


            if (TowelInformation.company.Id == 0)
            {
                int CompanyId = 1;
                if (EnumerableExtensions.Any(_context.Products))
                {
                    TowelInformation.company.Id = _context.Companies.Max(x => x.Id) + 1;
                }

                _context.Companies.Add(TowelInformation.company);

            }


            int Id = 1;
            if (EnumerableExtensions.Any(_context.Products))
            {
                Id = _context.Products.Max(x => x.Id) + 1;
            }

            _context.Products.Add(new Product()
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
            if (EnumerableExtensions.Any(_context.Variants))
            {
                TowelId = _context.Variants.Max(x => x.Id) + 1;
            }

            foreach (var VARIABLE in TowelInformation.Variants)
            {
                _context.Variants.Add(new Variant()
                {
                    Id = TowelId,
                    ProductFK = Id,
                    ColourFK = VARIABLE.ColourFK,
                    ProductTypeFK = VARIABLE.ProductTypeFK,
                    FobPrice = VARIABLE.FobPrice,
                    WholesaleA = VARIABLE.WholesaleA,
                    WholesaleB = VARIABLE.WholesaleB,
                    RetailPrice = VARIABLE.RetailPrice,
                    Width = VARIABLE.Width,
                    length = VARIABLE.length,
                    Size = VARIABLE.Size,
                    Note = VARIABLE.Note,
                    LastDateEdited = DateTime.Now,
                    Data1 = VARIABLE.Data1
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

            var tt = _context.Products.ToList().LastOrDefault();
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

        public string GiveMeSku(string CategoryCode, string SubCategoryCode, string ProductTypeCode, string ColourCode)

        {
            var list = _context.Variants.Where(p => p.Sku != null).ToList();
            string LastSkuNumber = "";
            int SkuNumber = 0;
            int MaxSku = 0;
            if (list.Count == 0)
            {
                MaxSku= 6068;
            }
            else
            {
                foreach (var VARIABLE in list)
                {
                    SkuNumber = Convert.ToInt16(VARIABLE.Sku.Substring(10, 6));
                    if (MaxSku < SkuNumber)
                        MaxSku = SkuNumber;

                }
            }



            string Sku = CategoryCode + SubCategoryCode + ProductTypeCode + ColourCode + (MaxSku+1).ToString().NumtoStr(6);

            return Sku;
        }


        public Company ExistCompany(string CompanyName)
        {
            return _context.Companies.FirstOrDefault(p => p.CompanyName.ToLower().CompareTo(CompanyName.ToLower()) == 0);
        }

        public List<VariantViewModel> AllVariantList()
        {
            List<VariantViewModel> list = new List<VariantViewModel>();

            var VariantslList = _context.Variants.Include(p => p.Product).Include(p=>p.Barcode).Include(p => p.ProductType).ThenInclude(p => p.CategoriesSubCategory).Include(p => p.Colour).ToList();

            foreach (var VARIABLE in VariantslList)
            {
                list.Add(new VariantViewModel()
                {
                    Id = VARIABLE.Id,
                    Sku = VARIABLE.Sku,
                    ProductFK = VARIABLE.ProductFK,
                    ColourFK = VARIABLE.ColourFK,
                    BarcodeFK = VARIABLE.BarcodeFK,
                    ProductTypeFK = VARIABLE.ProductTypeFK,
                    FobPrice = VARIABLE.FobPrice,
                    WholesaleA = VARIABLE.WholesaleA,
                    WholesaleB = VARIABLE.WholesaleB,
                    RetailPrice = VARIABLE.RetailPrice,
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
            var product = _context.Products.AsNoTracking().Include(p=>p.Variants).ThenInclude(p=>p.Barcode).Include(p => p.Variants).ThenInclude(p => p.Colour).Include(p => p.Company).Include(p => p.Material)
                .Include(p => p.Brand).Include(p => p.Variants).ThenInclude(p => p.ProductType)
                .ThenInclude(p => p.CategoriesSubCategory).ThenInclude(p => p.Category).Include(p => p.Variants).ThenInclude(p => p.ProductType)
                .ThenInclude(p => p.CategoriesSubCategory).ThenInclude(p => p.SubCategory).Include(p => p.CountryOfOrgin).FirstOrDefault(p => p.Id == Id);



            return product;
        }

        public int AddSku(int Id, string Sku)
        {
            var Variant = _context.Variants.SingleOrDefault(p => p.Id == Id);
            if (Variant == null)
            {

            }
            else
            {
                Variant.Sku = Sku;
                _context.Variants.Update(Variant);
                _context.SaveChanges();
            }
            return 1;
        }

        public int AddBarcode(int Id)
        {
            
            var Variant = _context.Variants.SingleOrDefault(p => p.Id == Id);
            var NewBarcode = _context.Barcodes.FirstOrDefault(p => p.Active == false);
            if (NewBarcode == null)
                return -1;
            else
            {
                Variant.BarcodeFK = NewBarcode.Id;
                NewBarcode.Active = true;
                _context.SaveChanges();
                return 1;
            }
            
        }
    }
}
