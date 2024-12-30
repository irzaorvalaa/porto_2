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
    public class DeliveryTrackingHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<DeliveryTrackingResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrDataOrder = EntityHelper.Get<TrOrder>(x => x.Stsrc != "D").ToList();
                var arrDataProductCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                var arrDataOrderProduct = EntityHelper.Get<TrOrderProduct>(x => x.Stsrc != "D").ToList();
                var arrDataMerchant = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D").ToList();



                var retData = (from to in arrDataOrder
                               join mpc in arrDataProductCategory on to.ProductCategoryID equals mpc.ID
                               join top in arrDataOrderProduct on to.ProductCategoryID equals top.ProductCategoryID
                               join mc in arrDataMerchant on top.MerchantID equals mc.ID

                               select new DeliveryTrackingResponse
                               {
                                   RecipientName = to.RecipientName,
                                   Category = mpc.Name,
                                   TrackingNo = to.TrackingNo,
                                   OrderNo = to.OrderNo,
                                   Name = mc.Name,
                                   DeliveryAddress = to.DeliveryAddress
                                   
                           
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}