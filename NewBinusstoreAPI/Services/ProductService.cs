﻿using System;
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
    [Route("Product")]
    public class ProductService : BaseService
    {
        public ProductService(ILogger<BaseService> logger) : base(logger)
        {
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult Get()
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
                        objJSON.Data = Helper.ProductHelper.Get((ValidTokenModel)processToken.Data);
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
                        objJSON.Data = Helper.ProductHelper.GetSingle((ValidTokenModel)processToken.Data, ID);
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

        [Route("GetMerchant")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetMerchant(string ID)
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
                        objJSON.Data = Helper.ProductHelper.GetMerchant((ValidTokenModel)processToken.Data, ID);
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

        [Route("GetCategoryProduct")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetCategoryProduct(string ID)
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
                        objJSON.Data = Helper.ProductHelper.GetCategoryProduct((ValidTokenModel)processToken.Data, ID);
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

        [Route("GetVariant")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetVariant(string ID)
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
                        objJSON.Data = Helper.ProductHelper.GetVariant((ValidTokenModel)processToken.Data, ID);
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

        [Route("GetProduct")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetProduct(string ID)
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
                        objJSON.Data = Helper.ProductHelper.GetProduct((ValidTokenModel)processToken.Data, ID);
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



        [HttpPost]
        [Route("doSave")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult Save([FromBody] ProductPayload formData)
        {
            try
            {
                var objJSON = new CustomOutput();
                string Token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
                CustomResponse processToken = new CustomResponse
                {
                    Status = true,
                    Data = new ValidTokenModel(),
                    Message = ""
                };

                if (processToken.Status)
                {
                    CustomResponse processData = Helper.ProductHelper.Save((ValidTokenModel)processToken.Data, formData);
                    objJSON.ResultCode = processData.Status ? 1 : 0;
                    objJSON.ErrorMessage = processData.Message != "" ? processData.Message : "";
                    objJSON.Callback = processData.Callback != "" ? processData.Callback : "";

                }
                else
                {
                    objJSON.ResultCode = 0;
                    objJSON.ErrorMessage = processToken.Message;
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