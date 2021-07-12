using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ClubJumana.Core.Comparer;
using ClubJumana.Core.Enums;
using ClubJumana.Core.Services.Interfaces;
using ClubJumana.DataLayer.Context;
using ClubJumana.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubJumana.Core.Services
{
    public class RepositoryService : IRepositoryService
    {
        private JummanaContext _context;
        private OnlineContext onlineDb;
        public RepositoryService()
        {
            _context = new JummanaContext();

            onlineDb = new OnlineContext();
        }

        public void UpdateLocalDb()
        {
            List<TablesVersion> TableOfOnline = onlineDb.tablesversion.ToList();
            List<TablesVersion> TableOfLocaldb = _context.tablesversion.ToList();

            foreach (var VARIABLE in TableOfLocaldb)
            {
                if (VARIABLE.RowVersion != TableOfOnline.ElementAt(VARIABLE.Id - 1).RowVersion)
                    VARIABLE.NeedToUpdate = true;
                else
                    VARIABLE.NeedToUpdate = false;
            }

            if (TableOfLocaldb[(int)TableName.Barcodes - 1].NeedToUpdate)
                BarcodeUpdate();
            if (TableOfLocaldb[(int)TableName.Colours - 1].NeedToUpdate)
                ColourUpdate();
            if (TableOfLocaldb[(int)TableName.Brands - 1].NeedToUpdate)
                BrandUpdate();
            if (TableOfLocaldb[(int)TableName.Countries - 1].NeedToUpdate)
                CountryUpdate();
            if (TableOfLocaldb[(int)TableName.Materials - 1].NeedToUpdate)
                MaterialUpdate();
            if (TableOfLocaldb[(int)TableName.Companies - 1].NeedToUpdate)
                CompanyUpdate();
            if (TableOfLocaldb[(int)TableName.Products - 1].NeedToUpdate)
                ProductUpdate();
            if (TableOfLocaldb[(int)TableName.Categories - 1].NeedToUpdate)
                CategoryUpdate();
            if (TableOfLocaldb[(int)TableName.SubCategories - 1].NeedToUpdate)
                SubcategoryUpdate();
            if (TableOfLocaldb[(int)TableName.CategoriesSubCategories - 1].NeedToUpdate)
                CategorySubcategoryUpdate();
            if (TableOfLocaldb[(int)TableName.ProductTypes - 1].NeedToUpdate)
                ProductTypeUpdate();
            if (TableOfLocaldb[(int)TableName.Variants - 1].NeedToUpdate)
                VariantUpdate();
            if (TableOfLocaldb[(int)TableName.Images - 1].NeedToUpdate)
                ImageUpdate();
            //if (TableOfLocaldb[(int)TableName.Barcodes - 1].NeedToUpdate)
            //    ProvinceUpdate();

        }

        public IQueryable<Variant> allVariants()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.variants.Include(p=>p.Images).Include(p=>p.Product).Include(p=>p.Barcode).OrderBy(p => p.Id).AsNoTracking();
            else
                return _context.variants;
        }

        #region Update Function


        public void BarcodeUpdate()
        {
            //var list1 = _context.countries.OrderBy(p => p.Id).Select(p => new Country() { Id = p.Id, Code = p.Code,Name = p.Name}).ToList();
            //var list2 = onlineDb.countries.OrderBy(p => p.Id).Select(p => new Country() { Id = p.Id, Code = p.Code,Name = p.Name}).ToList();

            //var list4 = _context.barcodes.AsNoTracking().Select(p => new{Id=p.Id,Bracode=p.BarcodeNumber,Active=p.Active}).ToList();
            //var list5 = onlineDb.barcodes.AsNoTracking().Select(p => new { Id = p.Id, Bracode = p.BarcodeNumber, Active = p.Active }).ToList();

            var _contextList = _context.barcodes.AsNoTracking().ToList();
            var OnlineList = onlineDb.barcodes.AsNoTracking().ToList();

            var comparer = new BarcodeComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            Barcode w;
            Barcode ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.barcodes.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.barcodes.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.barcodes.Add(w);

                }
                else
                {
                    ww.BarcodeNumber = w.BarcodeNumber;
                    ww.Active = w.Active;

                    _context.barcodes.Update(ww);
                }
            }

            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.barcodes.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.barcodes.Remove(ww);
                }

            }

            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Barcodes).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Barcodes).RowVersion = version;
            _context.SaveChanges();

        }

        public void BrandUpdate()
        {
            var _contextList = _context.brands.AsNoTracking().ToList();
            var OnlineList = onlineDb.brands.AsNoTracking().ToList();

            var comparer = new BrandComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            Brand w;
            Brand ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.brands.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.brands.SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.brands.Add(w);

                }
                else
                {
                    ww.Name = w.Name;

                    _context.brands.Update(ww);
                }
            }
            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.brands.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.brands.Remove(ww);
                }

            }

            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Brands).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Brands).RowVersion = version;
            _context.SaveChanges();

        }

        public void CategoryUpdate()
        {
            var _contextList = _context.categories.AsNoTracking().ToList();
            var OnlineList = onlineDb.categories.AsNoTracking().ToList();

            var comparer = new CategoryComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            Category w;
            Category ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.categories.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.categories.SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.categories.Add(w);

                }
                else
                {
                    ww.Name = w.Name;
                    ww.Sku_code = w.Sku_code;
                    ww.StyleNum_code = w.StyleNum_code;

                    _context.categories.Update(ww);
                }
            }
            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.categories.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.categories.Remove(ww);
                }

            }
            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Categories).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Categories).RowVersion = version;
            _context.SaveChanges();

        }
        public void SubcategoryUpdate()
        {
            var _contextList = _context.subcategories.AsNoTracking().ToList();
            var OnlineList = onlineDb.subcategories.AsNoTracking().ToList();

            var comparer = new SubcategoryComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            SubCategory w;
            SubCategory ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.subcategories.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.subcategories.SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.subcategories.Add(w);

                }
                else
                {
                    ww.Name = w.Name;

                    _context.subcategories.Update(ww);
                }
            }
            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.subcategories.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.subcategories.Remove(ww);
                }

            }
            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.SubCategories).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.SubCategories).RowVersion = version;
            _context.SaveChanges();

        }

        public void CategorySubcategoryUpdate()
        {
            var _contextList = _context.categoriessubcategories.AsNoTracking().ToList();
            var OnlineList = onlineDb.categoriessubcategories.AsNoTracking().ToList();

            var comparer = new CategorySubcategoryComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            CategoriesSubCategory w;
            CategoriesSubCategory ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.categoriessubcategories.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.categoriessubcategories.SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.categoriessubcategories.Add(w);

                }
                else
                {
                    ww.CategoryFK = w.CategoryFK;
                    ww.SubCategoryFK = w.SubCategoryFK;

                    _context.categoriessubcategories.Update(ww);
                }
            }
            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.categoriessubcategories.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.categoriessubcategories.Remove(ww);
                }

            }
            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.CategoriesSubCategories).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.CategoriesSubCategories).RowVersion = version;
            _context.SaveChanges();

        }
        public void ProductTypeUpdate()
        {
            var _contextList = _context.producttypes.AsNoTracking().ToList();
            var OnlineList = onlineDb.producttypes.AsNoTracking().ToList();

            var comparer = new ProductTypeComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            ProductType w;
            ProductType ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.producttypes.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.producttypes.SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.producttypes.Add(w);

                }
                else
                {
                    ww.Name = w.Name;
                    ww.Code = w.Code;
                    ww.CategorysubcategoreisFK = w.CategorysubcategoreisFK;
                    _context.producttypes.Update(ww);
                }
            }

            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.producttypes.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.producttypes.Remove(ww);
                }

            }
            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.ProductTypes).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.ProductTypes).RowVersion = version;
            _context.SaveChanges();

        }

        public void ColourUpdate()
        {
            var _contextList = _context.colours.AsNoTracking().ToList();
            var OnlineList = onlineDb.colours.AsNoTracking().ToList();

            var comparer = new ColourComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            Colour w;
            Colour ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.colours.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.colours.SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.colours.Add(w);

                }
                else
                {
                    ww.Name = w.Name;
                    ww.PantoneNumber = w.PantoneNumber;
                    ww.Code = w.Code;
                    _context.colours.Update(ww);
                }
            }

            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.colours.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.colours.Remove(ww);
                }

            }
            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Colours).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Colours).RowVersion = version;
            _context.SaveChanges();

        }
        public void CountryUpdate()
        {
            var _contextList = _context.countries.AsNoTracking().ToList();
            var OnlineList = onlineDb.countries.AsNoTracking().ToList();

            var comparer = new CountryComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            Country w;
            Country ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.countries.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.countries.SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.countries.Add(w);

                }
                else
                {
                    ww.Name = w.Name;
                    ww.Code = w.Code;
                    ww.Duty = w.Duty;
                    ww.ExChangeRate = w.ExChangeRate;
                    ww.Currency = w.Currency;
                    ww.CurrencyName = w.CurrencyName;
                    ww.DigitalCode = w.DigitalCode;

                    _context.countries.Update(ww);
                }
            }
            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.countries.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.countries.Remove(ww);
                }

            }
            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Countries).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Countries).RowVersion = version;
            _context.SaveChanges();

        }

        public void MaterialUpdate()
        {
            var _contextList = _context.materials.AsNoTracking().ToList();
            var OnlineList = onlineDb.materials.AsNoTracking().ToList();

            var comparer = new MaterialComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            Material w;
            Material ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.materials.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.materials.SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.materials.Add(w);

                }
                else
                {
                    ww.MaterialName = w.MaterialName;

                    _context.materials.Update(ww);
                }
            }
            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.materials.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.materials.Remove(ww);
                }

            }
            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Materials).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Materials).RowVersion = version;
            _context.SaveChanges();

        }
        public void CompanyUpdate()
        {
            var _contextList = _context.companies.AsNoTracking().ToList();
            var OnlineList = onlineDb.companies.AsNoTracking().ToList();

            var comparer = new CompanyComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            Company w;
            Company ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.companies.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.companies.SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.companies.Add(w);

                }
                else
                {
                    ww.CompanyName = w.CompanyName;
                    ww.Manufacture = w.Manufacture;
                    ww.Website = w.Website;
                    ww.Email = w.Email;
                    ww.StreetAddress = w.StreetAddress;
                    ww.AddressLine2 = w.AddressLine2;
                    ww.City = w.City;
                    ww.StateProvinceRegion = w.StateProvinceRegion;
                    ww.ZipPostlCode = w.ZipPostlCode;
                    ww.CountryFK = w.CountryFK;
                    ww.Phone = w.Phone;
                    ww.FAX = w.FAX;
                    _context.companies.Update(ww);
                }
            }
            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.companies.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.companies.Remove(ww);
                }

            }
            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Companies).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Companies).RowVersion = version;
            _context.SaveChanges();

        }

        public void ProductUpdate()
        {
            var _contextList = _context.products.AsNoTracking().ToList();
            var OnlineList = onlineDb.products.AsNoTracking().ToList();

            var comparer = new ProductComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            Product w;
            Product ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.products.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.products.SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.products.Add(w);

                }
                else
                {
                    ww.StyleNumber = w.StyleNumber;
                    ww.ProductTittle = w.ProductTittle;
                    ww.BrandFK = w.BrandFK;
                    ww.MaterialFK = w.MaterialFK;
                    ww.CompanyFK = w.CompanyFK;
                    ww.CountryOfOrginFK = w.CountryOfOrginFK;
                    ww.DescribeMaterial = w.DescribeMaterial;
                    _context.products.Update(ww);
                }
            }
            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.products.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.products.Remove(ww);
                }

            }
            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Products).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Products).RowVersion = version;
            _context.SaveChanges();
        }
        public void VariantUpdate()
        {
            var _contextList = _context.variants.AsNoTracking().ToList();
            var OnlineList = onlineDb.variants.AsNoTracking().ToList();

            var comparer = new VariantComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            Variant w;
            Variant ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.variants.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.variants.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.variants.Add(new Variant()
                    {
                        Id = w.Id,
                        Sku = w.Sku,
                        ProductFK = w.ProductFK,
                        ColourFK = w.ColourFK,
                        BarcodeFK = w.BarcodeFK,
                        ProductTypeFK = w.ProductTypeFK,
                        FobPrice = w.FobPrice,
                        WholesaleA = w.WholesaleA,
                        WholesaleB = w.WholesaleB,
                        RetailPrice = w.RetailPrice,
                        Width = w.Width,
                        length = w.length,
                        Size = w.Size,
                        Note = w.Note,
                        Data1 = w.Data1,
                        Data2 = w.Data2,
                        Data3 = w.Data3,
                        Data4 = w.Data4,
                        Data5 = w.Data5,
                        Data6 = w.Data6,
                        LastDateEdited = w.LastDateEdited
                    });


                }
                else
                {
                    ww.Sku = w.Sku;
                    ww.ProductFK = w.ProductFK;
                    ww.ColourFK = w.ColourFK;
                    ww.BarcodeFK = w.BarcodeFK;
                    ww.ProductTypeFK = w.ProductTypeFK;
                    ww.FobPrice = w.FobPrice;
                    ww.WholesaleA = w.WholesaleA;
                    ww.WholesaleB = w.WholesaleB;
                    ww.RetailPrice = w.RetailPrice;
                    ww.Width = w.Width;
                    ww.length = w.length;
                    ww.Size = w.Size;
                    ww.Note = w.Note;
                    ww.Data1 = w.Data1;
                    ww.Data2 = w.Data2;
                    ww.Data3 = w.Data3;
                    ww.Data4 = w.Data4;
                    ww.Data5 = w.Data5;
                    ww.Data6 = w.Data6;
                    ww.LastDateEdited = w.LastDateEdited;
                    _context.variants.Update(ww);
                }
            }
            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.variants.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.variants.Remove(ww);
                }

            }
            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Variants).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Variants).RowVersion = version;
            _context.SaveChanges();

        }

        public void ImageUpdate()
        {
            var _contextList = _context.images.AsNoTracking().ToList();
            var OnlineList = onlineDb.images.AsNoTracking().ToList();

            var comparer = new imageComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            Image w;
            Image ww;
            List<string> ImagesName = new List<string>();
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.images.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.images.SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.images.Add(w);

                }
                else
                {
                    ww.ImageName = w.ImageName;
                    ww.VariantFK = w.VariantFK;

                    _context.images.Update(ww);
                }
            }

            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.images.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.images.Remove(ww);
                }

            }
            var version = onlineDb.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Images).RowVersion;
            _context.tablesversion.FirstOrDefault(p => p.Id == (int)TableName.Images).RowVersion = version;
            _context.SaveChanges();

        }


        public void ProvinceUpdate()
        {
            var _contextList = _context.provinces.AsNoTracking().ToList();
            var OnlineList = onlineDb.provinces.AsNoTracking().ToList();

            var comparer = new ProvinceComparer();
            var DiffrentItems = OnlineList.Except(_contextList, comparer).ToList();

            var IdForAdd = OnlineList.Select(p => p.Id).Except(_contextList.Select(p => p.Id));

            Province w;
            Province ww;
            foreach (var VARIABLE in DiffrentItems)
            {
                w = onlineDb.provinces.AsNoTracking().SingleOrDefault(p => p.Id == VARIABLE.Id);
                ww = _context.provinces.SingleOrDefault(p => p.Id == VARIABLE.Id);

                if (IdForAdd.Contains(VARIABLE.Id))
                {
                    _context.provinces.Add(w);

                }
                else
                {
                    ww.Name = w.Name;
                    ww.HST = w.HST;
                    ww.GST = w.GST;
                    ww.QST = w.QST;
                    ww.Active = w.Active;

                    _context.provinces.Update(ww);
                }
            }

            var IdForDelete = _contextList.Select(p => p.Id).Except(OnlineList.Select(p => p.Id));

            if (IdForDelete.Count() != 0)
            {
                foreach (var VARIABLE in IdForDelete)
                {
                    ww = _context.provinces.SingleOrDefault(p => p.Id == VARIABLE);
                    _context.provinces.Remove(ww);
                }

            }
            _context.SaveChanges();

        }
        #endregion

        private void DetachedAllEntries()
        {
            if (!Consts.Consts.OnlineModeOnly)
            {
                foreach (var entry in _context.ChangeTracker.Entries().ToList())
                {
                    _context.Entry(entry.Entity).State = EntityState.Detached;
                }
            }
            foreach (var entry in onlineDb.ChangeTracker.Entries().ToList())
            {
                onlineDb.Entry(entry.Entity).State = EntityState.Detached;
            }
        }



        public IQueryable<ProductMaster> AllProductMasterList()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.productmasters.OrderBy(p => p.Id).AsNoTracking();
            else
                return _context.productmasters;


        }

        public IQueryable<PurchaseOrder> AllPurchaseOrder()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.purchaseorders.OrderBy(p => p.Id).AsNoTracking();
            else
                return _context.purchaseorders.AsNoTracking();
        }

        public IQueryable<SaleOrder> AllOrders()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.saleorders.Include(p => p.SoItems).ThenInclude(p => p.ProductMaster)
                    
                    .Include(p => p.Customer).OrderByDescending(p => p.Id).AsNoTracking();
            else
                return _context.saleorders.Include(p => p.SoItems).ThenInclude(p => p.ProductMaster)
                    
                    .Include(p => p.Customer).AsNoTracking();
        }

        public IQueryable<Vendor> AllVendor()
        {
            return _context.vendors.OrderByDescending(p => p.Id);
        }

        public IQueryable<Province> AllProvinces()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.provinces.OrderBy(p => p.Id);
            else
                return _context.provinces.OrderBy(p => p.Id);
        }

        public IQueryable<TaxRate> AlltaTaxRates()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.taxrates.OrderBy(p => p.Id);
            else
                return _context.taxrates.OrderBy(p => p.Id);
        }

        public IQueryable<Customer> AllCustomers()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.customers.Include(p => p.Country).Include(p => p.Province).OrderByDescending(p => p.Id);
            else
                return _context.customers.Include(p => p.Country).Include(p => p.Province).OrderByDescending(p => p.Id);
        }

        public IQueryable<PurchaseOrder> AsnPurchaseOrder()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.purchaseorders.Where(p => p.CreatedPO == true).OrderByDescending(p => p.Id).AsNoTracking();
            else
                return _context.purchaseorders.Where(p => p.CreatedPO == true).OrderByDescending(p => p.Id).AsNoTracking();
        }

        public IQueryable<PurchaseOrder> GrnPurchaseOrder()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.purchaseorders.Where(p => p.CreatedAsn == true).OrderByDescending(p => p.Id).AsNoTracking();
            else
                return _context.purchaseorders.Where(p => p.CreatedAsn == true).OrderByDescending(p => p.Id).AsNoTracking();
        }

        public IQueryable<Country> AllCountriesList()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.countries.OrderBy(p => p.Id);
            else
                return _context.countries.OrderBy(p => p.Id);
        }

        public IQueryable<Company> AllCompaniesList()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.companies.OrderBy(p => p.Id);
            else
                return _context.companies.OrderBy(p => p.Id);
        }

        public IQueryable<Category> AllCategoriesList()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.categories.OrderBy(p => p.Id);
            else
                return _context.categories.OrderBy(p => p.Id);
        }

        public IQueryable<SubCategory> AllSubCategoriesList()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.subcategories.OrderBy(p => p.Id);
            else
                return _context.subcategories.OrderBy(p => p.Id);
        }

        public IQueryable<CategoriesSubCategory> AllCategoriesSubCategoryList()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.categoriessubcategories.OrderBy(p => p.Id);
            else
                return _context.categoriessubcategories.OrderBy(p => p.Id);
        }

        public IQueryable<ProductType> AllProductTypeList()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.producttypes.OrderBy(p => p.Id);
            else
                return _context.producttypes.OrderBy(p => p.Id);
        }

        public IQueryable<Brand> AllBrandList()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.brands.OrderBy(p => p.Id);
            else
                return _context.brands.OrderBy(p => p.Id);
        }

        public IQueryable<Colour> AllColourList()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.colours.OrderBy(p => p.Name).AsNoTracking();
            else
                return _context.colours.OrderBy(p => p.Name).AsNoTracking();
        }

        public IQueryable<Material> AllMaterialList()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.materials.OrderBy(p => p.Id);
            else
                return _context.materials.OrderBy(p => p.Id);
        }

        public IQueryable<Product> AllProductList()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.products.Include(p => p.Variants).ThenInclude(p => p.ProductType.CategoriesSubCategory).OrderBy(p => p.Id);
            else
                return _context.products.Include(p => p.Variants).ThenInclude(p => p.ProductType.CategoriesSubCategory).OrderBy(p => p.Id);
        }

        public IQueryable<Term> AllTerms()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.terms.OrderBy(p => p.Id);
            else
                return _context.terms.OrderBy(p => p.Id);
        }

        public IQueryable<PaymentMethod> AllPaymentMethods()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.paymentmethods.OrderBy(p => p.Id);
            else
                return _context.paymentmethods.OrderBy(p => p.Id);
        }

        public IQueryable<DepositTo> AllDepositTos()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.deposittos.OrderBy(p => p.Id);
            else
                return _context.deposittos.OrderBy(p => p.Id);
        }

        public IQueryable<UOM> AllUoms()
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.uoms.OrderBy(p => p.Id);
            else
                return _context.uoms.OrderBy(p => p.Id);
        }

        public Country GiveMeCountryByID(int Id)
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.countries.FirstOrDefault(p => p.Id == Id);
            else
                return _context.countries.FirstOrDefault(p => p.Id == Id);
        }

        public ProductMaster GiveMeProductMasterByUPC(string UPC)
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.productmasters.FirstOrDefault(p => p.UPC.Trim().CompareTo(UPC.Trim()) == 0);
            else
                return _context.productmasters.FirstOrDefault(p => p.UPC.Trim().CompareTo(UPC.Trim()) == 0);
        }

        public Customer GiveMeCustomerById(int Id)
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.customers.FirstOrDefault(p => p.Id == Id);
            else
                return _context.customers.FirstOrDefault(p => p.Id == Id);
        }

        public Vendor GiveMeVendorById(int Id)
        {
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.vendors.FirstOrDefault(p => p.Id == Id);
            else
                return _context.vendors.FirstOrDefault(p => p.Id == Id);
        }


        public IQueryable<Warehouse> AllWarehouse()
        {
            DetachedAllEntries();
            if (Consts.Consts.OnlineModeOnly)
                return onlineDb.warehouses;
            else
                return _context.warehouses;
        }

        public int AddAndUpdateCustomer(Customer customer, bool isSave = true)
        {
            if (isSave)
                DetachedAllEntries();
            if (customer.Id == 0)
            {
                customer.Id = onlineDb.customers.Max(p => p.Id) + 1;
                onlineDb.customers.Add(customer);
            }
            else if (!isSave)
            {
                onlineDb.customers.Add(customer);
            }

            else
            {
                onlineDb.Update(customer);
            }

            if (isSave)
                onlineDb.SaveChanges();
            return 1;
        }

        public int AddAndUpdateVendor(Vendor vendor, bool isSave = true)
        {
            if (isSave)
                DetachedAllEntries();
            if (vendor.Id == 0)
            {
                vendor.Id = onlineDb.vendors.Max(p => p.Id) + 1;
                onlineDb.vendors.Add(vendor);
            }
            else if (!isSave)
            {
                onlineDb.vendors.Add(vendor);
            }

            else
            {
                onlineDb.Update(vendor);
            }

            if (isSave)
                onlineDb.SaveChanges();
            return 1;
        }

        public int AddAndUpdateTaxRate(TaxRate taxRate, bool isSave = true)
        {
            if (isSave)
                DetachedAllEntries();
            if (taxRate.Id == 0)
            {
                // Later dont Import duplicate 
                taxRate .Id = _context.taxrates.Count()>0? onlineDb.taxrates.Max(p => p.Id) + 1:1;
                onlineDb.taxrates.Add(taxRate);
            }
            else
            {
               // onlineDb.Update(taxRate);
            }

            if (isSave)
                onlineDb.SaveChanges();
            return 1;
        }

        public int AddTerm(Term term, bool isSave = true)
        {
            if (isSave)
                DetachedAllEntries();
            if (term.Id == 0)
            {
                //product.Id = _context.customers.Max(p => p.Id) + 1;
                //_context.productmasters.Add(product);
            }
            else if (!isSave)
            {
                onlineDb.terms.Add(term);
            }

            else
            {
                onlineDb.terms.Add(term);
            }
            if (isSave)
                onlineDb.SaveChanges();
            return 1;
        }

        public int UpdateTerm(Term term, bool isSave = true)
        {
            if (isSave)
                DetachedAllEntries();
            if (term.Id == 0)
            {
                //product.Id = _context.customers.Max(p => p.Id) + 1;
                //_context.productmasters.Add(product);
            }
            else if (!isSave)
            {
                onlineDb.terms.Update(term);
            }
            else
            {
                var tt = onlineDb.terms.FirstOrDefault(p => p.Id == term.Id);
                if (tt != null)
                {
                    tt.Description = term.Description;
                    tt.Active = term.Active;
                    tt.LastUpdateTime = term.LastUpdateTime;
                    tt.Name = term.Name;
                    tt.DueDateCalculation = term.DueDateCalculation;

                    onlineDb.terms.Update(tt);
                }

            }
            if (isSave)
                onlineDb.SaveChanges();
            return 1;
        }


        public string UploadFileToFTP(string fileName, string UploadDirectory)
        {
            string FtpUrl = "ftp://mansour1989%2540clubjummana.com@132.148.182.213";
            string filePhath = "";
            string userName = "mansour1989@clubjummana.com";
            string password = "Mansour1989M";
            UploadDirectory = "/VariantsImage";

            string PureFileName = new FileInfo(fileName).Name;
            String uploadUrl = String.Format("{0}{1}/{2}", FtpUrl, UploadDirectory, PureFileName);
            FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(uploadUrl);
            req.Proxy = null;
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.Credentials = new NetworkCredential(userName, password);
            req.UseBinary = true;
            req.UsePassive = true;
            byte[] data = File.ReadAllBytes(fileName);
            req.ContentLength = data.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            FtpWebResponse res = (FtpWebResponse)req.GetResponse();
            return res.StatusDescription;
        }

        public string DownloadFileFromFTP(string fileName, string UploadDirectory)
        {
            string FtpUrl = "ftp://mansour1989%2540clubjummana.com@132.148.182.213";
            string PureFileName = new FileInfo(fileName).Name;
            String DownloadUrl = String.Format("{0}{1}/{2}", FtpUrl, UploadDirectory, PureFileName);
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(DownloadUrl);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            request.Credentials = new NetworkCredential("mansour1989@clubjummana.com", "Mansour1989M");

            string ResponseDescription = "";
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                byte[] buffer = new byte[2048];
                FileStream fs = new FileStream(fileName, FileMode.Create);
                int ReadCount = stream.Read(buffer, 0, buffer.Length);
                while (ReadCount > 0)
                {
                    fs.Write(buffer, 0, ReadCount);
                    ReadCount = stream.Read(buffer, 0, buffer.Length);
                }
                ResponseDescription = response.StatusDescription;
                fs.Close();
                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ResponseDescription;
        }

        public IQueryable<ProductMaster> AllProductMasterWithInventoryList()
        {
            return onlineDb.productmasters.OrderBy(p => p.Id).Include(p => p.ProductInventoryWarehouses)
                .ThenInclude(p => p.Warehouse).AsNoTracking();
        }

        public bool SaveDatabase()
        {
            try
            {
                onlineDb.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
