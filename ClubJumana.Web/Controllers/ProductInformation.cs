using ClubJumana.Core.Convertors;
using ClubJumana.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubJumana.Core.DTOs;
using ClubJumana.DataLayer.Entities;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;

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
        [HttpGet]
        public List<VariantViewModel> Get()
        {
            //var tt = _productInformationService.AllVariantList().Take(4).ToList();
            return null;
        }

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
            if (tt.Note != null)
                tt.Note = Regex.Replace(tt.Note, @"\r\n?|\n", "<br>");
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
            //var tt = _productInformationService.AllVariantList().Take(4).ToList();
            return null;
        }

        // GET api/<ProductInformationList>/5
        [HttpGet("{id}")]

        public string Get(int id)
        {
            var ttr = _productInformationService.AllVariantList().Where(p=>p.Barcode!=null).Select(p=> new {id=p.Id,productType=p.ProductType.Name,upc=p.Barcode.BarcodeNumber
                ,size=p.Size,title=(p.Product.ProductTittle==null)?"-": p.Product.ProductTittle,
                sku=p.SKU,styleNumber=p.Product.StyleNumber,color=p.Colour.Name }).ToList();
           var tyy=  Newtonsoft.Json.JsonConvert.SerializeObject(ttr);
            return tyy;
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
    public class ProductInformationById : ControllerBase
    {
        private IProductInformationService _productInformationService;
        private IRepositoryService _repositoryService; 
        private IViewRenderService _viewRender;
        public static IWebHostEnvironment _enviroment;
        public ProductInformationById(IProductInformationService productInformationService, IRepositoryService repositoryService, IViewRenderService viewRender,IWebHostEnvironment environment)
        {
            _productInformationService = productInformationService;
            _repositoryService = repositoryService;
            _viewRender = viewRender;
            _enviroment = environment;
        }


        // GET: api/<ProductInformationById>
        [HttpGet]
        public List<VariantViewModel> Get()
        {
            //var tt = _productInformationService.AllVariantList().Take(4).ToList();
            return null;
        }

        // GET api/<ProductInformationById>/5
        [HttpGet("{id}")]

        public Variant Get(int id)
        {
            var tt = _productInformationService.GiveMeVariantById(id);
            string imagesPath = _enviroment.WebRootPath + "\\images\\ProductImages";
            if (!Directory.Exists(_enviroment.WebRootPath+ "\\images\\ProductImages"))
            {
                Directory.CreateDirectory(_enviroment.WebRootPath + "\\images\\ProductImages");
            }
            else
            {
                if(tt.Images.Count>0)
                {
                    if (!System.IO.File.Exists(Path.Combine(imagesPath, tt.Images.FirstOrDefault().ImageName)))
                    {
                        _repositoryService.DownloadFileFromFTP(Path.Combine(imagesPath, tt.Images.FirstOrDefault().ImageName), "/VariantsImage");
                    }
                    tt.Data6 = tt.Images.FirstOrDefault().ImageName;
                }
            }

            if (tt.Note != null)
                tt.Note = Regex.Replace(tt.Note, @"\r\n?|\n", "<br>");
            return tt;
        }

        // POST api/<ProductInformationById>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductInformationById>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductInformationById>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

}
