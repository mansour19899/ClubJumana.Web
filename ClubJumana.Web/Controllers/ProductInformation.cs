using ClubJumana.Core.Convertors;
using ClubJumana.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubJumana.Core.DTOs;
using ClubJumana.DataLayer.Entities;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClubJumana.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductInformation : ControllerBase
    {
        private IProductInformationService _productInformationService;
        private IViewRenderService _viewRender;
        public ProductInformation(IProductInformationService productInformationService, IViewRenderService viewRender)
        {
            _productInformationService = productInformationService;
            _viewRender = viewRender;
        }


        // GET: api/<ProductInformation>
        //[HttpGet]
        //public List<VariantViewModel> Get()
        //{
        //    var tt = _productInformationService.AllVariantList().Take(4).ToList();
        //    return tt;
        //}

        // GET api/<ProductInformation>/5
        [HttpGet("{id}")]

        public Variant Get(string id)
        {
            var tt = _productInformationService.GiveMeVariantWithSkuUpc(id);
            if(tt==null)
            {
                tt= new Variant() {Id=-1};
            }
            if (tt.ProductType == null)
                tt.ProductType = new ProductType() { Name = "-" };
            if (tt.Sku == null || tt.Sku == "")
                tt.Sku = "-";
            if (tt.Product == null)
                tt.Product = new Product() { ProductTittle = "-" ,StyleNumber="-"};
            else if(tt.Product.ProductTittle=="")
                tt.Product.ProductTittle = "-";
            else { 
            }
            if(tt.Barcode==null)
            {
                tt.Barcode = new Barcode() { BarcodeNumber = "-" };
            }
            if (tt.Colour == null)
                tt.Colour = new Colour() { Name = "-" };
            
            return tt;
        }

        // POST api/<ProductInformation>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductInformation>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductInformation>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ProductInformationList : ControllerBase
    {
        private IProductInformationService _productInformationService;
        private IViewRenderService _viewRender;
        public ProductInformationList(IProductInformationService productInformationService, IViewRenderService viewRender)
        {
            _productInformationService = productInformationService;
            _viewRender = viewRender;
        }


        // GET: api/<ProductInformationList>
        [HttpGet]
        public List<VariantViewModel> Get()
        {
            var tt = _productInformationService.AllVariantList().Take(4).ToList();
            return tt;
        }

        // GET api/<ProductInformationList>/5
        [HttpGet("{id}")]

        public List<VariantViewModel> Get(string id)
        {
            var tt = _productInformationService.AllVariantList().Take(4).ToList();

            return tt;
        }

        // POST api/<ProductInformation>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductInformation>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductInformation>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
