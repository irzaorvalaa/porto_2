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
    public class BTSPeriodHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<BTSPeriodResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsBTSPeriod>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new BTSPeriodResponse
                               {
                                   ID = a.ID,
                                   Name = a.Name,
                                   DeliveryEndDate = a.DeliveryEndDate,
                                   DeliveryStartDate = a.DeliveryStartDate,
                                   IsActive = a.IsActive,
                                   EffEndDate = a.EffEndDate,
                                   EffStartDate = a.EffStartDate
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static BTSPeriodResponse GetSingle(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrData = EntityHelper.Get<MsBTSPeriod>(x => x.Stsrc != "D" && x.ID == ID).ToList();
                var retData = (from a in arrData
                               select new BTSPeriodResponse
                               {
                                   ID = a.ID,
                                   Name = a.Name,
                                   DeliveryEndDate = a.DeliveryEndDate,
                                   DeliveryStartDate = a.DeliveryStartDate,
                                   IsActive = a.IsActive,
                                   EffEndDate = a.EffEndDate,
                                   EffStartDate = a.EffStartDate
                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CustomResponse Save(ValidTokenModel tokenData, BTSPeriodPayload formData)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "doReloadTable()";
            try
            {
                if (formData.Action == "Add")
                {
                    var NewID = Guid.NewGuid().ToString().ToUpper();
                    var data = new MsBTSPeriod()
                    {
                        ID = NewID,
                        Name = formData.Name,
                        DeliveryStartDate = formData.DeliveryStartDate,
                        DeliveryEndDate = formData.DeliveryEndDate,
                        EffStartDate = formData.EffStartDate,
                        EffEndDate = formData.EffEndDate,
                        IsActive = formData.IsActive,

                        Stsrc = "A",
                        UserIn = tokenData.UserID,
                        DateIn = DateTime.Now
                    };
                    EntityHelper.Add<MsBTSPeriod>(data);
                }
                else if (formData.Action == "Edit")
                {
                    var arrData = EntityHelper.Get<MsBTSPeriod>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Name = formData.Name;
                    arrData.DeliveryEndDate = formData.DeliveryEndDate;
                    arrData.DeliveryStartDate = formData.DeliveryStartDate;
                    arrData.EffStartDate = formData.EffStartDate;
                    arrData.EffEndDate = formData.EffEndDate;
                    arrData.IsActive = formData.IsActive;

                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsBTSPeriod>(arrData);

                    var subDataToDelete = EntityHelper.Get<MsBTSPeriod>(x => x.ID == arrData.ID).ToList();
                    if (subDataToDelete.Count > 0) EntityHelper.Delete<MsBTSPeriod>(subDataToDelete);
                }
                else if (formData.Action == "Delete")
                {
                    var arrData = EntityHelper.Get<MsBTSPeriod>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Stsrc = "D";
                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsBTSPeriod>(arrData);
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