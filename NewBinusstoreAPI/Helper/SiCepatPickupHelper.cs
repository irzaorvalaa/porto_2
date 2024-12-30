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
    public class SiCepatPickupHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<SiCepatPickupResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrDataOrder = EntityHelper.Get<TrOrder>(x => x.Stsrc != "D").ToList();
                var arrDataShppingService = EntityHelper.Get<MsShippingService>(x => x.Stsrc != "D").ToList();
                var arrDataShipping = EntityHelper.Get<MsShipping>(x => x.Stsrc != "D").ToList();
                var arrDataCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();

                var retData = (from o in arrDataOrder
                               join ss in arrDataShppingService on o.ShippingServiceID equals ss.ID
                               join s in arrDataShipping on o.DeliveryMethodID equals s.ID
                               join pc in arrDataCategory on o.ProductCategoryID equals pc.ID


                               select new SiCepatPickupResponse
                               {
                                   OrderNo = o.OrderNo,
                                   DateIn = o.DateIn,
                                   ProductName = pc.Name,
                                   RecipientName = o.RecipientName,
                                   DeliveryAddress = o.DeliveryAddress,
                                   TrackingNo = o.TrackingNo,
                                   Status = o.Status
                                  
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