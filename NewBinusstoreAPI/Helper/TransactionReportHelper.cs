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
    public class TransactionReportHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<TransactionReportResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrDataOrder = EntityHelper.Get<TrOrder>(x => x.Stsrc != "D").ToList();
                var arrDataInvoice = EntityHelper.Get<TrInvoice>(x => x.Stsrc != "D").ToList();
                var arrDataCustomer = EntityHelper.Get<MsCustomer>(x => x.Stsrc != "D").ToList();
                var arrDataCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                var arrDataProduct = EntityHelper.Get<MsProduct>(x => x.Stsrc != "D").ToList();
                var arrDataOrderProduct = EntityHelper.Get<TrOrderProduct>(x => x.Stsrc != "D").ToList();



                var retData = (from o in arrDataOrder
                               join ti in arrDataInvoice on o.CustomerID equals ti.CustomerID
                               join mpc in arrDataCategory on o.ProductCategoryID equals mpc.ID
                               join p in arrDataProduct on mpc.ID equals p.ProductCategoryID
                               join top in arrDataOrderProduct on mpc.ID equals top.ProductCategoryID
                               join mc in arrDataCustomer on o.Email equals mc.Email

                               select new TransactionReportResponse
                               {
                                   OrderNo = o.OrderNo,
                                   TransactionDate = o.DateIn,
                                   CustomerType = mc.CustomerType,
                                   Institution = o.Institution,
                                   Username = mc.Username,
                                   RecipientName = o.RecipientName,
                                   DeliveryMethod = o.DeliveryMethod,
                                   TrackingNo = o.TrackingNo,
                                   ScheduleDelivery = o.ScheduleDelivery,
                                   ActualDeliveryDate = o.ActualDeliveryDate,
                                   Weight = o.Weight,
                                   InvoiceNo = ti.InvoiceNo,
                                   PaymentMethodName = ti.PaymentMethodName,
                                   PaymentStatus = ti.Status,
                                   PaidDate = ti.PaidDate,
                                   TotalPayment = ti.TotalPayment,
                                   OrderStatus = o.Status,
                                   CompletedDate = o.CompletedDate,
                                   Notes = o.Notes,
                                   ProductCategoryName = mpc.Name,
                                   ProductSKU = p.ProductSKU,
                                   ProductName = p.Name,
                                   Qty = top.Qty,
                                   UnitWeight = top.UnitWeight
                                   
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