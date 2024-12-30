using System;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Binus.WS.Pattern.Helper;
using Binus.WS.Pattern.Output;
using Binus.WS.Pattern.RouteGuard;
using Binus.WS.Pattern.Service;

namespace NewBinusstoreAPI.Services
{
    [ApiController]
    [Route("auth")]
    public class AuthService : BaseService
    {
        public AuthService(ILogger<BaseService> logger) : base(logger)
        {
        }

        /// <summary>
        /// Generate Token
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /auth/token
        ///
        /// </remarks>
        /// <returns>Generate Token</returns>
        /// <response code="200">Returns Generated Token</response> 
        [HttpGet]
        [Route("token")]
        [RouteGuard(Authentication = "Basic")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TokenOutput), StatusCodes.Status200OK)]
        public IActionResult GetToken()
        {
            try
            {
                string encodedDivisionId = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenOutput = AuthHelper.GenerateJSONWebToken(Encoding.UTF8.GetString(Convert.FromBase64String(encodedDivisionId)));
                return new OkObjectResult(new
                {
                    Data = tokenOutput.TokenData,
                    tokenOutput.ResultCode,
                    tokenOutput.ErrorMessage,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }
    }
}
