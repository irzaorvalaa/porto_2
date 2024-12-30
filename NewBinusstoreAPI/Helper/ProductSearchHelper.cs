using NewBinusstoreAPI.Model;
using Binus.WS.Pattern.Entities;
using Binus.WS.Pattern.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2019.Excel.RichData2;
using Microsoft.ProjectServer.Client;

namespace NewBinusstoreAPI.Helper
{
    public class ProductSearchHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static HomeProductCategoryResponse GetCategory(ValidTokenModel tokenData,string ProductType)
        {
            try
            {
                var retData = new HomeProductCategoryResponse();
                var arrData = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                if (arrData.Count() > 0)
                {
                    var arrDataRole = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                    retData = (from a in arrData

                               select new HomeProductCategoryResponse
                               {
                                   ID = a.ID,
                                   UpperCategoryID = a.UpperCategoryID,
                                   Name = a.Name
                               }).FirstOrDefault();
                }
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<HomeBannerResponse> GetBanner(ValidTokenModel tokenData, string BannerType)
        {
            try
            {
                var retData = new List<HomeBannerResponse>();
                var arrData = EntityHelper.Get<MsBanner>(x => x.Stsrc != "D" && x.BannerType == BannerType).ToList();
                if (arrData.Count() > 0)
                {
                    var arrDataRole = EntityHelper.Get<MsBanner>(x => x.Stsrc != "D").ToList();
                    retData = (from a in arrData
                               orderby a.SequenceNo ascending
                               select new HomeBannerResponse
                               {
                                   ID = a.ID,
                                   BannerImgUrl = a.BannerImgUrl,
                                   TargetUrl = a.TargetUrl,
                                   SequenceNo = a.SequenceNo
                               }).ToList();
                }
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ProductSearchCategoryResponse> GetMerchant(ValidTokenModel tokenData)
        {
            try
            {

                var arrData = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new ProductSearchCategoryResponse
                               {
                                   MerchantID = a.ID,
                                   MerchantName = a.Name,
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<ProductSearchCategoryResponse> GetProduct(ValidTokenModel tokenData)
        {
            try
            {
                var retData = new List<ProductSearchCategoryResponse>();
                var arrData = EntityHelper.Get<MsProduct>(x => x.Stsrc != "D").ToList();
                var arrDataProductCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                var arrDataProductMedia = EntityHelper.Get<MsProductMedia>(x => x.Stsrc != "D").ToList();
                var arrDataMerchant = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D").ToList();

                var arrDataMappingProductSKU = EntityHelper.Get<MsMappingProductSKU>(x => x.Stsrc != "D").ToList();
                if (arrData.Count() > 0)
                {
                    var arrDataRole = EntityHelper.Get<MsBanner>(x => x.Stsrc != "D").ToList();
                    retData = (from p in arrData
                               join mpc in arrDataProductCategory on p.ProductCategoryID equals mpc.ID
                               join mm in arrDataMerchant on p.MerchantID equals mm.ID
                               select new ProductSearchCategoryResponse
                               {
                                   ID = p.ID,
                                   Name = p.Name,
                                   ProductPhoto = arrDataProductMedia.Where(x => x.ProductID == p.ID && x.IsPrimary == true).FirstOrDefault().MediaURL,
                                   ProductSKU = p.ProductSKU,
                                   DiscountType = p.DiscountType,
                                   DiscountValue = p.DiscountValue,
                                   DiscountedPrice = (p.DiscountType == "1" ? (p.Price - p.DiscountValue) : p.Price - ((p.Price * p.DiscountValue) / 100)),
                                   Price = p.Price,
                                   TotalSale = p.TotalSale,
                                   ProductCategory = mpc.Name,
                                   MerchantName = mm.Name,
                               }).ToList();
                }
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}