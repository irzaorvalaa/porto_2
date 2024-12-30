using System;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using NewBinusstoreAPI.Model;
using NewBinusstoreAPI.Output;

using Binus.WS.Pattern.Output;
using Binus.WS.Pattern.Service;
using Binus.WS.Pattern.RouteGuard;
using Microsoft.ProjectServer.Client;

namespace NewBinusstoreAPI.Services
{
    [ApiController]
    [Route("ProductSearch")]
    public class ProductSearchService : BaseService
    {
        public ProductSearchService(ILogger<BaseService> logger) : base(logger)
        {
        }

        [Route("GetProduct")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetProduct()
        {
            try
            {
                var objJSON = new CustomOutput();
                string Token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
                if (Token != "")
                {
                    CustomResponse processToken = new CustomResponse
                    {
                        Status = true,
                        Data = new ValidTokenModel(),
                        Message = ""
                    };

                    if (processToken.Status)
                    { 
                        objJSON.Data = Helper.ProductSearchHelper.GetProduct((ValidTokenModel)processToken.Data);
                        var Category = HttpContext.Request.Query["Category"].ToString();
                        var Merchant = HttpContext.Request.Query["Merchant"].ToString();
                    }
                objJSON.Callback = "onCompleteDetail(e.data)";
                }

                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }


        [Route("GetMerchant")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetMerchant()
        {
            try
            {
                var objJSON = new CustomOutput();
                string Token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
                if (Token != "")
                {
                    CustomResponse processToken = new CustomResponse
                    {
                        Status = true,
                        Data = new ValidTokenModel(),
                        Message = ""
                    };

                    if (processToken.Status)
                    {
                        objJSON.Data = Helper.ProductSearchHelper.GetMerchant((ValidTokenModel)processToken.Data);
                    }
                    objJSON.Callback = "onCompleteDetail(e.data)";
                }

                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

        [Route("GetCategory")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetCategory(string ProductType)
        {
            try
            {
                var objJSON = new CustomOutput();
                string Token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
                if (Token != "")
                {
                    CustomResponse processToken = new CustomResponse
                    {
                        Status = true,
                        Data = new ValidTokenModel(),
                        Message = ""
                    };

                    if (processToken.Status)
                    {
                        objJSON.Data = Helper.ProductSearchHelper.GetCategory((ValidTokenModel)processToken.Data, ProductType);
                    }
                    objJSON.Callback = "onCompleteDetail(e.data)";
                }

                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

        


    }
}