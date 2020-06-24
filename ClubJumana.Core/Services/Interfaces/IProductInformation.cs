using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.Core.DTOs;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Services.Interfaces
{
    interface IProductInformationService
    {
        public int AddTowel(AddVariantInformationViewModel product);
        public string GiveMeStyleNumber(int Category, int SubCategory);
        public string GiveMeSku(string CategoryCode,string SubCategoryCode,string ProductTypeCode);
        public Variant GiveMeVariantById(int Id);
        public Company ExistCompany(string CompanyName);

        public List<VariantViewModel> AllVariantList();
        public Product GiveMeProductWithId(int Id);
        public int AddSku(int Id, string Sku);
        public int AddBarcode(int Id);
        public int AddOrUpdateVariant(Variant variant, int ProductId);
        public int AddImageVariant(int variantFK, string imageName);
        public List<string> GiveCountOfImagesVariant();
        public Colour AddColour(string name, string pantoneNumber);
    }
    
}
