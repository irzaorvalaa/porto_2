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
    public class BannerHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<BannerResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsBanner>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new BannerResponse
                               {
                                   ID = a.ID,
                                   BannerType = a.BannerType,
                                   Name = a.Name,
                                   IsActive = a.IsActive,
                                   TargetUrl = a.TargetUrl,
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

        public static BannerResponse GetSingle(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrData = EntityHelper.Get<MsBanner>(x => x.Stsrc != "D" && x.ID == ID).ToList();
                var retData = (from a in arrData
                               select new BannerResponse
                               {
                                   ID = a.ID,
                                   BannerType = a.BannerType,
                                   Name = a.Name,
                                   IsActive = a.IsActive,
                                   TargetUrl = a.TargetUrl,
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

        public static CustomResponse Save(ValidTokenModel tokenData, BannerPayload formData)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "doReloadTable()";
            try
            {
                if (formData.Action == "Add")
                {
                    var NewID = Guid.NewGuid().ToString().ToUpper();
                    var data = new MsBanner()
                    {
                        ID = NewID,
                        Name = formData.Name,
                        BannerImgUrl = formData.BannerImgUrl,
                        BannerMimeType = formData.BannerMimeType,
                        BannerType = formData.BannerType,
                        TargetUrl = formData.TargetUrl,
                        EffStartDate = formData.EffStartDate,
                        EffEndDate = formData.EffEndDate,
                        SequenceNo = formData.SequenceNo,
                        IsActive = formData.IsActive,
                        IsBTSPeriod = formData.IsBTSPeriod,

                        Stsrc = "A",
                        UserIn = tokenData.UserID,
                        DateIn = DateTime.Now
                    };
                    EntityHelper.Add<MsBanner>(data);
                }
                else if (formData.Action == "Edit")
                {
                    var arrData = EntityHelper.Get<MsBanner>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Name = formData.Name;
                    arrData.BannerImgUrl = formData.BannerImgUrl;
                    arrData.BannerMimeType = formData.BannerMimeType;
                    arrData.BannerType = formData.BannerType;
                    arrData.TargetUrl = formData.TargetUrl;
                    arrData.EffStartDate = formData.EffStartDate;
                    arrData.EffEndDate = formData.EffEndDate;
                    arrData.SequenceNo = formData.SequenceNo;
                    arrData.IsActive = formData.IsActive;
                    arrData.IsBTSPeriod = formData.IsBTSPeriod;

                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsBanner>(arrData);

                    var subDataToDelete = EntityHelper.Get<MsBanner>(x => x.ID == arrData.ID).ToList();
                    if (subDataToDelete.Count > 0) EntityHelper.Delete<MsBanner>(subDataToDelete);
                }
                else if (formData.Action == "Delete")
                {
                    var arrData = EntityHelper.Get<MsBanner>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Stsrc = "D";
                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsBanner>(arrData);
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