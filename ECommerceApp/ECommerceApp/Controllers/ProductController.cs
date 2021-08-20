using ECommerceApp.Common.Request;
using ECommerceApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        [Route("Init")]
        public ActionResult Init()
        {
            try
            {
                _productService.initProducts();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
                throw ex;
            }
        }

        [HttpPost]
        [Route("Find")]
        public ActionResult FindProduct(ProductFindRequest request)
        {
            try
            {
                var result = _productService.FindProduct(request.Id, request.Name);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
                throw ex;
            }
        }

        [HttpPost]
        [Route("SubmitOrder")]
        public ActionResult SubmitOrder(SubmitOrderRequest request)
        {
            try
            {
                var result = _productService.SubmitOrder(request.Order, request.Commands);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
                throw ex;
            }
        }
    }
}
