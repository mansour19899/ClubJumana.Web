using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.Core.DTOs;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Services.Interfaces
{
   public interface IProductInformationService
    {
        public int AddTowel(AddVariantInformationViewModel product);
        public string GiveMeStyleNumber(int Category, int SubCategory);
        public string GiveMeSku(string CategoryCode,string SubCategoryCode,string ProductTypeCode);
        public Variant GiveMeVariantById(int Id);
        public Company ExistCompany(string CompanyName);

        public List<VariantViewModel> AllVariantList();
        public List<VariantViewModel> AllStarVariantList();
        public Product GiveMeProductWithId(int Id);
        public Variant GiveMeVariantWithId(int Id);
        public Variant GiveMeVariantWithSkuUpc(string txtSearch);
        public int AddSku(int Id, string Sku);
        public int AddBarcode(int Id);
        public int AddOrUpdateVariant(Variant variant, int ProductId,bool UpdateSameVariant);
        public int AddImageVariant(int variantFK, string imageName);
        public List<string> GiveCountOfImagesVariant();
        public Colour AddColour(string name, string pantoneNumber);
        public Colour IsExistColorByPantonNumber(string pantoneNumber);
        public int UpdateVariantNote(int Id,string note);
        public int UpdateProduct(ProductInformationViewModel productInformationView);
        public int TransferProductsToProductMaster(List<VariantViewModel> list);
        public int ExcelForQuickbooks(List<VariantViewModel> list);
        public int ExportAllDataBase(List<VariantViewModel> list);
        public bool SetStar(int Id, bool Set = true);
    }
    
}
