using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ClubJumana.Core.Convertors;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Enums;
using ClubJumana.Core.Generator;
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
        private OnlineContext _onlineContext;
        public ProductInformationService()
        {
            _context = new JummanaContext();
            _onlineContext = new OnlineContext();
        }
        public int AddTowel(AddVariantInformationViewModel TowelInformation)
        {
            DetachedAllEntries();

            if (TowelInformation.company.Id == 0)
            {
                int CompanyId = 1;
                if (EnumerableExtensions.Any(_onlineContext.products))
                {
                    TowelInformation.company.Id = _onlineContext.companies.Max(x => x.Id) + 1;
                }

                _onlineContext.companies.Add(TowelInformation.company);
                if (!Consts.Consts.OnlineModeOnly)
                    _context.companies.Add(TowelInformation.company);


            }


            int Id = _onlineContext.products.Max(x => x.Id) + 1;

            _onlineContext.products.Add(new Product()
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

            if (!Consts.Consts.OnlineModeOnly)
            {
                _context.products.Add(new Product()
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
            }


            int TowelId = _onlineContext.variants.Max(x => x.Id) + 1;



            foreach (var VARIABLE in TowelInformation.Variants)
            {
                _onlineContext.variants.Add(new Variant()
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

                if (!Consts.Consts.OnlineModeOnly)
                {
                    _context.variants.Add(new Variant()
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
                }

                TowelId++;

            }

            _onlineContext.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Companies).RowVersion++;
            _onlineContext.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Products).RowVersion++;
            _onlineContext.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Variants).RowVersion++;


            if (!Consts.Consts.OnlineModeOnly)
            {
                _context.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Companies).RowVersion++;
                _context.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Products).RowVersion++;
                _context.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Variants).RowVersion++;
            }


            _onlineContext.SaveChanges();
            if (!Consts.Consts.OnlineModeOnly)
                _context.SaveChanges();

            return 1;
        }

        public string GiveMeStyleNumber(int Category, int SubCategory)
        {
            var CategoryStyleCode = _onlineContext.categories.SingleOrDefault(p => p.Id == Category).StyleNum_code;
            var SubCategoryCode = _onlineContext.subcategories.SingleOrDefault(p => p.Id == SubCategory).Code;

            var tt = _onlineContext.products.AsNoTracking().ToList().LastOrDefault();
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

        public string GiveMeSku(string CategoryCode, string SubCategoryCode, string ProductTypeCode)

        {
            var list = _onlineContext.variants.Skip(90).Where(p => p.Sku != null && p.Sku != "0").ToList();
            string LastSkuNumber = "";
            Int32 SkuNumber = 0;
            int MaxSku = 0;
            if (list.Count == 0)
            {
                MaxSku = 37121;
            }
            else
            {
                foreach (var VARIABLE in list)
                {
                    SkuNumber = Convert.ToInt32(VARIABLE.Sku.Substring(7, 7));
                    if (MaxSku < SkuNumber)
                        MaxSku = SkuNumber;
                }
            }

            if (MaxSku == 0)
                MaxSku = 37121;

            string Sku = CategoryCode + SubCategoryCode + ProductTypeCode + (MaxSku + 1).ToString().NumtoStr(7);

            return Sku;
        }

        public Variant GiveMeVariantById(int Id)
        {
            if (Consts.Consts.OnlineModeOnly)
                return _onlineContext.variants.AsNoTracking().Include(p => p.Colour).Include(p=>p.Images).Include(p => p.Product).Include(p => p.ProductType).Include(p => p.Barcode).FirstOrDefault(p => p.Id == Id);
            else
                return _context.variants.Include(p => p.Colour).Include(p => p.Product).Include(p => p.ProductType).Include(p => p.Barcode).FirstOrDefault(p => p.Id == Id);
        }


        public Company ExistCompany(string CompanyName)
        {
            if (Consts.Consts.OnlineModeOnly)
                return _onlineContext.companies.FirstOrDefault(p => p.CompanyName.ToLower().CompareTo(CompanyName.ToLower()) == 0);
            else
                return _context.companies.FirstOrDefault(p => p.CompanyName.ToLower().CompareTo(CompanyName.ToLower()) == 0);
        }

        public List<VariantViewModel> AllVariantList()
        {
            List<VariantViewModel> list = new List<VariantViewModel>();
            List<Variant> VariantslList;
            if (Consts.Consts.OnlineModeOnly)
            {
                VariantslList = _onlineContext.variants.OrderBy(p => p.ProductFK).Include(p => p.Product)
                   .Include(p => p.Barcode).Include(p => p.ProductType).ThenInclude(p => p.CategoriesSubCategory)
                   .Include(p => p.Product).ThenInclude(p => p.CountryOfOrgin).Include(p => p.Colour).ToList();
            }
            else
            {
                VariantslList = _context.variants.OrderBy(p => p.ProductFK).Include(p => p.Product)
                   .Include(p => p.Barcode).Include(p => p.ProductType).ThenInclude(p => p.CategoriesSubCategory)
                   .Include(p => p.Product).ThenInclude(p => p.CountryOfOrgin).Include(p => p.Colour).ToList();
            }

            foreach (var VARIABLE in VariantslList)
            {
                list.Add(new VariantViewModel()
                {
                    Id = VARIABLE.Id,
                    SKU = VARIABLE.Sku,
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
            Product product;
            if (Consts.Consts.OnlineModeOnly)
            {
                product = _onlineContext.products.AsNoTracking().Include(p => p.Variants).ThenInclude(p => p.Images).Include(p => p.Variants).ThenInclude(p => p.Barcode).Include(p => p.Variants).ThenInclude(p => p.Colour).Include(p => p.Company).Include(p => p.Material)
                    .Include(p => p.Brand).Include(p => p.Variants).ThenInclude(p => p.ProductType)
                    .ThenInclude(p => p.CategoriesSubCategory).ThenInclude(p => p.Category).Include(p => p.Variants).ThenInclude(p => p.ProductType)
                    .ThenInclude(p => p.CategoriesSubCategory).ThenInclude(p => p.SubCategory).Include(p => p.CountryOfOrgin).FirstOrDefault(p => p.Id == Id);

            }
            else
            {
                product = _context.products.AsNoTracking().Include(p => p.Variants).ThenInclude(p => p.Images).Include(p => p.Variants).ThenInclude(p => p.Barcode).Include(p => p.Variants).ThenInclude(p => p.Colour).Include(p => p.Company).Include(p => p.Material)
                    .Include(p => p.Brand).Include(p => p.Variants).ThenInclude(p => p.ProductType)
                    .ThenInclude(p => p.CategoriesSubCategory).ThenInclude(p => p.Category).Include(p => p.Variants).ThenInclude(p => p.ProductType)
                    .ThenInclude(p => p.CategoriesSubCategory).ThenInclude(p => p.SubCategory).Include(p => p.CountryOfOrgin).FirstOrDefault(p => p.Id == Id);

            }



            return product;
        }

        public int AddSku(int Id, string Sku)
        {
            DetachedAllEntries();
            var Variant = _onlineContext.variants.SingleOrDefault(p => p.Id == Id);
            Variant Variantt=new Variant();
            if (!Consts.Consts.OnlineModeOnly)
                 Variantt = _context.variants.SingleOrDefault(p => p.Id == Id);

            if (Variant == null)
            {

            }
            else
            {
                Variant.Sku = Sku;
                Variantt.Sku = Sku;

                _onlineContext.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Variants).RowVersion++;
                if (!Consts.Consts.OnlineModeOnly)
                    _context.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Variants).RowVersion++;

                _onlineContext.SaveChanges();
                if (!Consts.Consts.OnlineModeOnly)
                    _context.SaveChanges();
            }
            return 1;
        }

        public int AddBarcode(int Id)
        {
            DetachedAllEntries();
            var Variant = _onlineContext.variants.SingleOrDefault(p => p.Id == Id);
            var NewBarcode = _onlineContext.barcodes.OrderBy(p => p.Id).FirstOrDefault(p => p.Active == false);

            if (NewBarcode == null)
                return -1;
            else
            {
                Variant.BarcodeFK = NewBarcode.Id;
                NewBarcode.Active = true;
                if (!Consts.Consts.OnlineModeOnly)
                {
                    var Variantt = _context.variants.SingleOrDefault(p => p.Id == Id);
                    var NewBarcodee = _context.barcodes.FirstOrDefault(p => p.Id == NewBarcode.Id);
                    Variantt.BarcodeFK = NewBarcode.Id;
                    NewBarcodee.Active = true;
                }

                _onlineContext.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Variants).RowVersion++;
                _onlineContext.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Barcodes).RowVersion++;

                if (!Consts.Consts.OnlineModeOnly)
                {
                    _context.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Variants).RowVersion++;
                    _context.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Barcodes).RowVersion++;
                }
                _onlineContext.SaveChanges();
                if (!Consts.Consts.OnlineModeOnly)
                    _context.SaveChanges();
                return 1;
            }

        }

        private void DetachedAllEntries()
        {
            if (!Consts.Consts.OnlineModeOnly)
            {
                foreach (var entry in _context.ChangeTracker.Entries().ToList())
                {
                    _context.Entry(entry.Entity).State = EntityState.Detached;
                }
            }
            foreach (var entry in _onlineContext.ChangeTracker.Entries().ToList())
            {
                _onlineContext.Entry(entry.Entity).State = EntityState.Detached;
            }
        }
        public int AddOrUpdateVariant(Variant variant, int ProductId)
        {
            DetachedAllEntries();

            if (variant.Id == 0)
            {
                int id = _onlineContext.variants.Max(p => p.Id);
                variant.Data1 = _onlineContext.variants.FirstOrDefault(p => p.ProductFK == ProductId).Data1;
                variant.Id = id + 1;
                variant.ProductFK = ProductId;
                _onlineContext.variants.Add(new Variant()
                {
                    Id = variant.Id,
                    Sku = variant.Sku,
                    ProductFK = variant.ProductFK,
                    ColourFK = variant.ColourFK,

                    ProductTypeFK = variant.ProductTypeFK,
                    FobPrice = variant.FobPrice,
                    WholesaleA = variant.WholesaleA,
                    WholesaleB = variant.WholesaleB,
                    RetailPrice = variant.RetailPrice,
                    Width = variant.Width,
                    length = variant.length,
                    Size = variant.Size,
                    Note = variant.Note,
                    Data1 = variant.Data1,
                    Data2 = variant.Data2,
                    Data3 = variant.Data3,
                    Data4 = variant.Data4,
                    Data5 = variant.Data5,
                    Data6 = variant.Data6,
                    LastDateEdited = DateTime.Now,
                });
                if (!Consts.Consts.OnlineModeOnly)
                {
                    _context.variants.Add(new Variant()
                    {
                        Id = variant.Id,
                        Sku = variant.Sku,
                        ProductFK = variant.ProductFK,
                        ColourFK = variant.ColourFK,

                        ProductTypeFK = variant.ProductTypeFK,
                        FobPrice = variant.FobPrice,
                        WholesaleA = variant.WholesaleA,
                        WholesaleB = variant.WholesaleB,
                        RetailPrice = variant.RetailPrice,
                        Width = variant.Width,
                        length = variant.length,
                        Size = variant.Size,
                        Note = variant.Note,
                        Data1 = variant.Data1,
                        Data2 = variant.Data2,
                        Data3 = variant.Data3,
                        Data4 = variant.Data4,
                        Data5 = variant.Data5,
                        Data6 = variant.Data6,
                        LastDateEdited = DateTime.Now
                    });
                }

            }
            else
            {
                Variant variantdb = _onlineContext.variants.SingleOrDefault(p => p.Id == variant.Id);
                Variant variantdbb;

                if (variantdb == null)
                    return -1;
                else
                {
                    variantdb.WholesaleA = variant.WholesaleA;
                    variantdb.WholesaleB = variant.WholesaleB;
                    variantdb.RetailPrice = variant.RetailPrice;
                    variantdb.FobPrice = variant.FobPrice;
                    variantdb.ColourFK = variant.ColourFK;
                    variantdb.length = variant.length;
                    variantdb.Width = variant.Width;
                    variantdb.Size = variant.Size;
                    variantdb.LastDateEdited = DateTime.Now;

                    if (!Consts.Consts.OnlineModeOnly)
                    {
                        variantdbb = _context.variants.SingleOrDefault(p => p.Id == variant.Id);
                        variantdbb.WholesaleA = variant.WholesaleA;
                        variantdbb.WholesaleB = variant.WholesaleB;
                        variantdbb.RetailPrice = variant.RetailPrice;
                        variantdbb.FobPrice = variant.FobPrice;
                        variantdbb.ColourFK = variant.ColourFK;
                        variantdbb.length = variant.length;
                        variantdbb.Width = variant.Width;
                        variantdbb.Size = variant.Size;
                        variantdbb.LastDateEdited = DateTime.Now;

                    }

                }
            }

            _onlineContext.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Variants).RowVersion++;
            if (!Consts.Consts.OnlineModeOnly)
                _context.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Variants).RowVersion++;

            _onlineContext.SaveChanges();
            if (!Consts.Consts.OnlineModeOnly)
                _context.SaveChanges();
            return 1;
        }

        public int AddImageVariant(int variantFK, string imageName)
        {
            int NewId = 1;
            if (_onlineContext.images.Count() != 0)
            {
                NewId = _onlineContext.images.Max(p => p.Id) + 1;
            }

            var Newimage = new Image()
            {
                Id = NewId,
                VariantFK = variantFK,
                ImageName = imageName

            };
            var Newimagee = new Image()
            {
                Id = NewId,
                VariantFK = variantFK,
                ImageName = imageName

            };

            _onlineContext.images.Add(Newimage);
            _onlineContext.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Images).RowVersion++;

            if (!Consts.Consts.OnlineModeOnly)
            {
                _context.images.Add(Newimagee);
                _context.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Images).RowVersion++;
            }

            _onlineContext.SaveChanges();
            if (!Consts.Consts.OnlineModeOnly)
                _context.SaveChanges();
            return 1;
        }

        public List<string> GiveCountOfImagesVariant()
        {
            if (Consts.Consts.OnlineModeOnly)
                return _onlineContext.images.Select(p => new string(p.ImageName)).ToList();
            else
                return _context.images.Select(p => new string(p.ImageName)).ToList();
        }

        public Colour AddColour(string name, string pantoneNumber)
        {
            DetachedAllEntries();

            int newId = _onlineContext.colours.Max(p => p.Id);
            Colour colour = new Colour();
            colour.Name = name;
            colour.PantoneNumber = pantoneNumber;
            colour.Code = (newId + 1).ToString().NumtoStr(3);
            colour.Id = newId + 1;




            _onlineContext.colours.Add(colour);
            _onlineContext.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Colours).RowVersion++;


            if (!Consts.Consts.OnlineModeOnly)
            {
                _context.colours.Add(colour);
                _context.tablesversion.SingleOrDefault(p => p.Id == (int)TableName.Colours).RowVersion++;
            }
            _onlineContext.SaveChanges();
            if (!Consts.Consts.OnlineModeOnly)
                _context.SaveChanges();

            return new Colour { Id = colour.Id };

        }

        public Colour IsExistColorByPantonNumber(string pantoneNumber)
        {
            if (Consts.Consts.OnlineModeOnly)
            {
                return _onlineContext.colours.FirstOrDefault(p =>
                    p.Name.ToLower().Replace(" ", "").Trim().CompareTo(pantoneNumber.ToLower().Replace(" ", "").Trim()) == 0);
            }
            else
            {
                return _context.colours.FirstOrDefault(p =>
                    p.Name.ToLower().Replace("tcx", "").Trim().CompareTo(pantoneNumber.ToLower().Replace("tcx", "").Trim()) == 0);
            }

        }

        public int UpdateVariantNote(int Id, string note)
        {
            DetachedAllEntries();
            Variant tt = new Variant();
            var t = _onlineContext.variants.FirstOrDefault(p => p.Id == Id);

            if (!Consts.Consts.OnlineModeOnly)
                tt = _context.variants.FirstOrDefault(p => p.Id == Id);

            if (t != null)
            {
                t.Note = note;
                tt.Note = note;
            }

            _onlineContext.SaveChanges();
            if (!Consts.Consts.OnlineModeOnly)
                _context.SaveChanges();
            return 1;
        }

        public int UpdateProduct(ProductInformationViewModel productInformationView)
        {
            DetachedAllEntries();
            var product = _onlineContext.products.Include(p => p.Variants).FirstOrDefault(p => p.Id == productInformationView.Id);
            product.ProductTittle = productInformationView.ProductTittle;
            product.DescribeMaterial = productInformationView.DescriabeMaterial;
            foreach (var VARIABLE in product.Variants)
            {
                VARIABLE.Data1 = productInformationView.GSM;
            }

            Product product2;
            if (!Consts.Consts.OnlineModeOnly)
            {
                product2 = _context.products.Include(p => p.Variants).FirstOrDefault(p => p.Id == productInformationView.Id);
                product2.ProductTittle = productInformationView.ProductTittle;
                product2.DescribeMaterial = productInformationView.DescriabeMaterial;
                foreach (var VARIABLE in product2.Variants)
                {
                    VARIABLE.Data1 = productInformationView.GSM;
                }
            }


            _onlineContext.SaveChanges();
            if (!Consts.Consts.OnlineModeOnly)
                _context.SaveChanges();
            return 1;
        }

        public int TransferProductsToProductMaster(List<VariantViewModel> list)
        {
            DetachedAllEntries();

            int Id = _onlineContext.productmasters.Max(p => p.Id);
            foreach (var item in list)
            {
                Id++;
                _onlineContext.productmasters.Add(new ProductMaster()
                {
                    Id = Id,
                    Name = "Set Description",
                    StyleNumber = item.Product.StyleNumber,
                    SKU = item.SKU,
                    UPC = item.Barcode.BarcodeNumber,
                    Size = item.Size,
                    Color = item.Colour.Name,
                    MadeIn = item.Product.CountryOfOrgin.Name,
                    VariantFK = item.Id,
                    FobPrice = item.FobPrice,
                    WholesalePrice = item.WholesaleB,
                    RetailPrice = item.RetailPrice,
                    StockOnHand = 0,
                    GoodsReserved = 0,
                    LastUpdateInventory = DateTime.Now,
                    Income = 0,
                    Outcome = 0,
                });

            }

            _onlineContext.SaveChanges();
            return 1;
        }
    }
}
