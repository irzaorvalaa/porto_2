using Binus.WS.Pattern.Entities;
using MailKit.Net.Smtp;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using NewBinusstoreAPI;
using NewBinusstoreAPI.Model;
using Newtonsoft.Json;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NewBinusstoreAPI.Helper
{
    public class AuthHelper
    {

        public static CustomResponse ValidateToken(string Token)
        {
            bool retStatus = false;
            string retMessage = "";
            ValidTokenModel retData = new ValidTokenModel();
            try
            {
                String tokenIssuer = "binus.ac.id";
                String tokenSecret = "BinusShared.JWTToken.version1.0-Beta";
                String tokenAudience = "Binus.HKI";
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSecret));
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                ClaimsPrincipal principal = handler.ValidateToken(Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = tokenIssuer,
                    ValidAudience = tokenAudience,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);

                retData.UserID = principal.Claims.ToList().Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                retData.Name = principal.Claims.ToList().Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
                retData.Email = principal.Claims.ToList().Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
                retData.AccessID = principal.Claims.ToList().Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;
                retData.IsExternal = principal.Claims.ToList().Where(x => x.Type == ClaimTypes.Locality).FirstOrDefault().Value;
                retData.ExternalID = principal.Claims.ToList().Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value;
                retStatus = true;
            }
            catch (Exception ex)
            {
                retStatus = false;
                retMessage = "Token expired, please relogin";
                //row ex;
            }
            return new CustomResponse()
            {
                Status = retStatus,
                Message = retMessage,
                Data = retData
            };
        }

        public static CustomResponse Validate(ValidTokenModel arrData)
        {
            bool retStatus = false;
            string retMessage = "";
            string retCallback = "onCompleteFetch(e)";
            AuthResponse retData = new AuthResponse();
            try
            {
                //var accessData = EntityHelper.Get<MasterAccessMenu>(null, null).Where(x => x.MenuID == "40289834-C692-4B3D-8601-C1036206D99F").Where(x => x.Stsrc != "D").ToList(); // Get Access that has Pengesahan
                //var listAccessData = accessData.Select(x => x.AccessID).Distinct().ToList();


                //var accessMenu = EntityHelper.Get<MasterAccessMenu>(null, null).Where(x => x.Stsrc != "D").Where(x => x.AccessID == arrData.AccessID).ToList();
                //var menuData = EntityHelper.Get<MasterMenu>(null, null).Where(x => x.Stsrc != "D").ToList();
                //retData.UserID = arrData.UserID;
                //retData.Name = arrData.Name;
                //retData.Email = arrData.Email;
                //retData.IsExternal = arrData.IsExternal;
                //retData.ExternalID = arrData.ExternalID;
                //retData.IsAdmin = listAccessData.Contains(arrData.AccessID) ? "T" : "F";
                //retData.AuthMenu = (from a in accessMenu
                //                    join m in menuData on a.MenuID equals m.MenuID
                //                    orderby m.SequenceNo ascending
                //                    select new AuthMenu()
                //                    {
                //                        Name = m.Name,
                //                        MenuUrl = m.MenuUrl,
                //                        SequenceNo = m.SequenceNo,
                //                        IsParent = m.IsParent
                //                    }).ToList();
                retStatus = true;
                return new CustomResponse()
                {
                    Status = retStatus,
                    Message = retMessage,
                    Data = retData,
                    Callback = retCallback
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CustomResponse Login(LoginModel post)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "onCompleteFetch(e)";
            string retData = "";
            try
            {
                //MasterUser existUser = EntityHelper.Get<MasterUser>(null, null).Where(x => x.Email.ToLower() == post.Username.ToLower()).FirstOrDefault(); // all user including deleted
                //if (existUser != null) // not exist user
                //{
                //    if (existUser.Stsrc != "A")
                //    {
                //        retStatus = false;
                //        retMessage = "User is inactive!";
                //    }
                //    else
                //    {
                //        try
                //        {
                //            String tokenExpirationTime = "24";
                //            String tokenIssuer = "binus.ac.id";
                //            String tokenSecret = "BinusShared.JWTToken.version1.0-Beta";
                //            String tokenAudience = "Binus.HKI";
                //            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSecret));
                //            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                //            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
                //            {
                //                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, existUser.UserID),
                //                                                new Claim(ClaimTypes.Email, existUser.Email),
                //                                                new Claim(ClaimTypes.Name, existUser.Name),
                //                                                new Claim(ClaimTypes.Role, existUser.AccessID),
                //                                                new Claim(ClaimTypes.Locality, existUser.IsExternal),
                //                                                new Claim(ClaimTypes.PrimarySid, existUser.ExternalID)}),
                //                Expires = DateTime.Now.AddHours(int.Parse(tokenExpirationTime)),
                //                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                //                Issuer = tokenIssuer,
                //                Audience = tokenAudience
                //            };
                //            retData = handler.WriteToken(handler.CreateToken(securityTokenDescriptor));
                //        }
                //        catch (Exception ex)
                //        {
                //            retStatus = false;
                //            retMessage = ex.Message;
                //        }
                //    }
                //}
                //else
                //{
                //    retStatus = false;
                //    retMessage = "User not found!";
                //}
                return new CustomResponse()
                {
                    Status = retStatus,
                    Message = retMessage,
                    Data = retData,
                    Callback = retCallback
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static async Task<CustomResponse> LoginAD(LoginModel post)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "";
            string retData = "";
            try
            {
                //MasterUser existUser = EntityHelper.Get<MasterUser>(null, null).Where(x => x.Email.ToLower() == post.Username.ToLower()).FirstOrDefault(); // all user including deleted
                //if (existUser != null) // exist user
                //{
                //    if (existUser.Stsrc != "A")
                //    {
                //        retStatus = false;
                //        retMessage = "User is inactive!";
                //    }
                //    else
                //    {
                //        try
                //        {
                //            String tokenExpirationTime = "24";
                //            String tokenIssuer = "binus.ac.id";
                //            String tokenSecret = "BinusShared.JWTToken.version1.0-Beta";
                //            String tokenAudience = "Binus.HKI";
                //            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSecret));
                //            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                //            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
                //            {
                //                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, existUser.UserID),
                //                                                new Claim(ClaimTypes.Email, existUser.Email),
                //                                                new Claim(ClaimTypes.Name, existUser.Name),
                //                                                new Claim(ClaimTypes.Role, existUser.AccessID),
                //                                                new Claim(ClaimTypes.Locality, existUser.IsExternal),
                //                                                new Claim(ClaimTypes.PrimarySid, existUser.ExternalID)}),
                //                Expires = DateTime.Now.AddHours(int.Parse(tokenExpirationTime)),
                //                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                //                Issuer = tokenIssuer,
                //                Audience = tokenAudience
                //            };
                //            retData = handler.WriteToken(handler.CreateToken(securityTokenDescriptor));
                //            retCallback = "onCompleteFetchLoginAD(e)";
                //        }
                //        catch (Exception ex)
                //        {
                //            retStatus = false;
                //            retMessage = ex.Message;
                //        }
                //    }
                //}
                //else
                //{
                //    var Token = "";
                //    using (HttpClient client = new HttpClient())
                //    {
                //        string url = Program.Configuration.GetSection("GeneralSetting").GetSection("HKI_Oracle_Path").Value + "/Integrate/login";
                //        var postData = new
                //        {
                //            username = Program.Configuration.GetSection("GeneralSetting").GetSection("HKI_Oracle_Username").Value,
                //            password = Program.Configuration.GetSection("GeneralSetting").GetSection("HKI_Oracle_Pass").Value
                //        };
                //        var jsonPostData = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
                //        HttpResponseMessage resp = await client.PostAsync(url, jsonPostData);
                //        string rawResponse = await resp.Content.ReadAsStringAsync();
                //        var response = JsonConvert.DeserializeObject<CustomResponse>(rawResponse);
                //        Token = response.Data.ToString();
                //    }

                //    var binusianData = new BinusianModel();
                //    using (HttpClient client = new HttpClient())
                //    {
                //        string url = Program.Configuration.GetSection("GeneralSetting").GetSection("HKI_Oracle_Path").Value + "/Integrate/lookupBinusian?Email=" + post.Username;
                //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                //        HttpResponseMessage resp = await client.GetAsync(url).ConfigureAwait(false);
                //        string rawResponse = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                //        var response = JsonConvert.DeserializeObject<CustomResponse>(rawResponse);
                //        binusianData = JsonConvert.DeserializeObject<BinusianModel>(response.Data.ToString());
                //    }

                //    if (binusianData.BinusianID != "" && binusianData.BinusianID != null)
                //    {
                //        var data = new MasterUser()
                //        {
                //            UserID = binusianData.BinusianID,
                //            Name = binusianData.Name,
                //            Email = binusianData.Email,
                //            ExternalID = binusianData.ExternalID,
                //            IsExternal = "F",
                //            AccessID = "USER",

                //            Stsrc = "A",
                //            UserIn = "SYSTEM",
                //            DateIn = DateTime.Now
                //        };
                //        await EntityHelper.Add<MasterUser>(data);

                //        try
                //        {
                //            String tokenExpirationTime = "24";
                //            String tokenIssuer = "binus.ac.id";
                //            String tokenSecret = "BinusShared.JWTToken.version1.0-Beta";
                //            String tokenAudience = "Binus.HKI";
                //            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSecret));
                //            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                //            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
                //            {
                //                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, data.UserID),
                //                                                new Claim(ClaimTypes.Email, data.Email),
                //                                                new Claim(ClaimTypes.Name, data.Name),
                //                                                new Claim(ClaimTypes.Role, data.AccessID),
                //                                                new Claim(ClaimTypes.Locality, data.IsExternal),
                //                                                new Claim(ClaimTypes.PrimarySid, data.ExternalID)}),
                //                Expires = DateTime.Now.AddHours(int.Parse(tokenExpirationTime)),
                //                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                //                Issuer = tokenIssuer,
                //                Audience = tokenAudience
                //            };
                //            retData = handler.WriteToken(handler.CreateToken(securityTokenDescriptor));
                //            retCallback = "onCompleteFetchLoginAD(e)";
                //        }
                //        catch (Exception ex)
                //        {
                //            retStatus = false;
                //            retMessage = ex.Message;
                //        }
                //    }
                //    else
                //    {
                //        retStatus = false;
                //        retMessage = "User is not registered, please contact Admin!";
                //    }
                //}
                return new CustomResponse()
                {
                    Status = retStatus,
                    Message = retMessage,
                    Data = retData,
                    Callback = retCallback
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}