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

namespace NewBinusstoreAPI.Services
{
    [ApiController]
    [Route("ProductDetail")]
    public class ProductDetailService : BaseService
    {
        public ProductDetailService(ILogger<BaseService> logger) : base(logger)
        {
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult Get( string ProductType ,string CategoryID = "", string Grade = "", string SchoolID = "", string PathwayID = "", string Level = "")
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
                        objJSON.Data = Helper.ProductDetailHelper.Get((ValidTokenModel)processToken.Data, ProductType, CategoryID, Grade, SchoolID, PathwayID, Level);
                    }
                    else
                    {
                        objJSON.ResultCode = 0;
                        objJSON.ErrorMessage = processToken.Message;
                    }
                }
                else
                {
                    objJSON.ResultCode = 0;
                    objJSON.ErrorMessage = "Not Authorized";
                }
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

      
        [Route("{ID}")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetSingle(string ID)
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
                        objJSON.Data = Helper.ProductDetailHelper.GetSingle((ValidTokenModel)processToken.Data, ID);

                        objJSON.Callback = "onCompleteDetail(e.data)";
                    }
                    else
                    {
                        objJSON.ResultCode = 0;
                        objJSON.ErrorMessage = processToken.Message;
                    }
                }
                else
                {
                    objJSON.ResultCode = 0;
                    objJSON.ErrorMessage = "Not Authorized";
                }
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }

        }

        [Route("GetProductMedia")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetProductMedia(string ID)
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
                        objJSON.Data = Helper.ProductDetailHelper.GetProductMedia((ValidTokenModel)processToken.Data, ID);

                        objJSON.Callback = "onCompleteDetail(e.data)";
                    }
                    else
                    {
                        objJSON.ResultCode = 0;
                        objJSON.ErrorMessage = processToken.Message;
                    }
                }
                else
                {
                    objJSON.ResultCode = 0;
                    objJSON.ErrorMessage = "Not Authorized";
                }
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

        [Route("GetProductVariant")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetProductVariant(string ID)
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
                        objJSON.Data = Helper.ProductDetailHelper.GetProductVariant((ValidTokenModel)processToken.Data, ID);

                        objJSON.Callback = "onCompleteDetail(e.data)";
                    }
                    else
                    {
                        objJSON.ResultCode = 0;
                        objJSON.ErrorMessage = processToken.Message;
                    }
                }
                else
                {
                    objJSON.ResultCode = 0;
                    objJSON.ErrorMessage = "Not Authorized";
                }
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

        [Route("GetRelatedItem")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetRelatedItem(string ID)
        {
            try
            {
                var objJSON = new CustomOutput();

                //objJSON.Data = Helper.MasterAccessHelper.GetSingle((ValidTokenModel)processToken.Data, ID);

                objJSON.Callback = "onCompleteDetail(e.data)";


                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }




    }
}