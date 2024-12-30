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
    [Route("DeliveryTracking")]
    public class DeliveryTrackingService : BaseService
    {
        public DeliveryTrackingService(ILogger<BaseService> logger) : base(logger)
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
                        objJSON.Data = Helper.DeliveryTrackingHelper.Get((ValidTokenModel)processToken.Data);
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

       

       
       
    }
}