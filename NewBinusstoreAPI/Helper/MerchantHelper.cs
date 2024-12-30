using NewBinusstoreAPI.Model;
using Binus.WS.Pattern.Entities;
using Binus.WS.Pattern.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace NewBinusstoreAPI.Helper
{
    public class MerchantHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<MerchantResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new MerchantResponse
                               {
                                   ID = a.ID,
                                   MerchantType = a.MerchantType,
                                   Name = a.Name,
                                   IsActive = a.IsActive,
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MerchantResponse GetSingle(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrData = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D" && x.ID == ID).ToList();
                var retData = (from a in arrData
                               select new MerchantResponse
                               {
                                   ID = a.ID,
                                   MerchantType = a.MerchantType,
                                   Name = a.Name,
                                   IsActive = a.IsActive,
                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CustomResponse Save(ValidTokenModel tokenData, MerchantPayload formData)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "doReloadTable()";
            try
            {
                if (formData.Action == "Add")
                {
                    var NewID = Guid.NewGuid().ToString().ToUpper();
                    var data = new MsMerchant()
                    {
                        ID = NewID,
                        Name = formData.Name,
                        MerchantType = formData.MerchantType,
                        IsActive = true,

                        Stsrc = "A",
                        UserIn = tokenData.UserID,
                        DateIn = DateTime.Now
                    };
                    EntityHelper.Add<MsMerchant>(data);
                }
                else if (formData.Action == "Edit")
                {
                    var arrData = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Name = formData.Name;
                    arrData.MerchantType = formData.MerchantType;
                    arrData.IsActive = formData.IsActive;

                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsMerchant>(arrData);

                    var subDataToDelete = EntityHelper.Get<MsMerchant>(x => x.ID == arrData.ID).ToList();
                    if (subDataToDelete.Count > 0) EntityHelper.Delete<MsMerchant>(subDataToDelete);
                }
                else if (formData.Action == "Delete")
                {
                    var arrData = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Stsrc = "D";
                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsMerchant>(arrData);
                }
                return new CustomResponse()
                {
                    Status = retStatus,
                    Message = retMessage,
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