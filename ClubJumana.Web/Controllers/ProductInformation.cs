using ClubJumana.Core.Convertors;
using ClubJumana.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IEnumerable<string> Get()
        {
            var tt = _productInformationService.AllVariantList().ToList();
            return new string[] { "value1", tt.ElementAt(0).SKU };
        }

        // GET api/<ProductInformation>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var tt = _productInformationService.GiveMeProductWithId(id);
            return tt.StyleNumber;
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
