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
    [Route("MappingProduct")]
    public class MappingProductService : BaseService
    {
        public MappingProductService(ILogger<BaseService> logger) : base(logger)
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
                        objJSON.Data = Helper.MappingProductHelper.Get((ValidTokenModel)processToken.Data);
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
                        objJSON.Data = Helper.MappingProductHelper.GetSingle((ValidTokenModel)processToken.Data, ID);
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

        [Route("GetSchool")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetSchool(string ID)
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
                        objJSON.Data = Helper.MappingProductHelper.GetSchool((ValidTokenModel)processToken.Data, ID);
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

        [Route("GetGrade")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetGrade(string ID)
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
                        objJSON.Data = Helper.MappingProductHelper.GetGrade((ValidTokenModel)processToken.Data, ID);
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

        [Route("GetStreaming")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetStreaming(string ID)
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
                        objJSON.Data = Helper.MappingProductHelper.GetStreaming((ValidTokenModel)processToken.Data, ID);
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

        [Route("GetGender")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetGender(string ID)
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
                        objJSON.Data = Helper.MappingProductHelper.GetGender((ValidTokenModel)processToken.Data, ID);
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

        [Route("GetCategory")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OutputBase), StatusCodes.Status200OK)]
        public IActionResult GetCategory(string ID)
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
                        objJSON.Data = Helper.MappingProductHelper.GetCategory((ValidTokenModel)processToken.Data, ID);
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
        public IActionResult Save([FromBody] MappingProductPayload formData)
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
                    CustomResponse processData = Helper.MappingProductHelper.Save((ValidTokenModel)processToken.Data, formData);
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