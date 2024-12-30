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
    public class TransactionHistoryHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<TransactionHistoryResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrDataOrder = EntityHelper.Get<TrOrder>(x => x.Stsrc != "D").ToList();
                var arrDataInvoice = EntityHelper.Get<TrInvoice>(x => x.Stsrc != "D").ToList();
                var arrDataCustomer = EntityHelper.Get<MsCustomer>(x => x.Stsrc != "D").ToList();

                var retData = (from o in arrDataOrder
                               join ti in arrDataInvoice on o.InvoiceID equals ti.ID
                               join mc in arrDataCustomer on o.Email equals mc.Email

                               select new TransactionHistoryResponse
                               {
                                   ID = o.ID,
                                   OrderNo = o.OrderNo,
                                   Notes = o.Notes,
                                   DeliveryAddress = o.DeliveryAddress,
                                   DeliveryMethod = o.DeliveryMethod,
                                   TrackingNo = o.TrackingNo,
                                   PaidDate = ti.PaidDate,
                                   ExpiredDate = ti.ExpiredDate,
                                   InvoiceNo = ti.InvoiceNo,
                                   PaymentMethodName = ti.PaymentMethodName,
                                   PaymentStatus = ti.Status,
                                   TotalPayment = ti.TotalPayment,
                                   CustomerType = mc.CustomerType,
                                   CustomerName = mc.Name,
                                   OrderStatus = o.Status

                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TransactionHistoryResponse> GetSchool(ValidTokenModel tokenData)
        {
            try
            {
                var arrDataSchool = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D").ToList();
             
                var retData = (from s in arrDataSchool

                               select new TransactionHistoryResponse
                               {
                                   SchoolID = s.SchoolID,
                                   SchoolName = s.SchoolName
                                   

                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TransactionHistoryResponse> GetCategory(ValidTokenModel tokenData)
        {
            try
            {
                var arrDataCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();

                var retData = (from pc in arrDataCategory

                               select new TransactionHistoryResponse
                               {
                                  ProductCategoryID = pc.ID,
                                  ProductCategoryName = pc.Name


                               }).ToList();
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