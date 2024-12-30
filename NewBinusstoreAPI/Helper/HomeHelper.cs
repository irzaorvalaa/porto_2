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
    public class HomeHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static HomeProductCategoryResponse GetCategory(ValidTokenModel tokenData)
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


        public static List<HomeProductListResponse> GetProduct(ValidTokenModel tokenData,
            string ProductType,
            string CategoryID,
            string Grade,
            string SchoolID,
            string PathwayID,
            string Level)
        {
            try
            {
                var arrData = EntityHelper.Get<MsProduct>(
                    x => x.Stsrc != "D"
                    && (ProductType == "1" ? x.IsMandatory == true : (ProductType == "2" ? x.IsReligion == true : true == true )))
                    .ToList();
                var listProductID = arrData.Select(x => x.ID).Distinct().ToList();
                var listCategoryID = arrData.Select(x => x.ProductCategoryID).Distinct().ToList();
                var listMerchantID = arrData.Select(x => x.MerchantID).Distinct().ToList();
                var listProductSKU = arrData.Select(x => x.ProductSKU).Distinct().ToList();

                var arrDataProductCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D" && listCategoryID.Contains(x.ID)).ToList();
                var arrDataProductMedia = EntityHelper.Get<MsProductMedia>(x => x.Stsrc != "D" && listProductID.Contains(x.ProductID)).ToList();
                var arrDataMerchant = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D" && listMerchantID.Contains(x.ID)).ToList();

                var arrDataMappingProductSKU = EntityHelper.Get<MsMappingProductSKU>(x => x.Stsrc != "D" && listProductSKU.Contains(x.ProductSKU)).ToList();
                var listProductMappingID = arrDataMappingProductSKU.Select(x => x.MappingProductID).Distinct().ToList();
                var arrDataMappingProduct = EntityHelper.Get<MsMappingProduct>(
                    x => x.Stsrc != "D"
                    && listProductMappingID.Contains(x.ID)
                    && SchoolID == x.MerchantID
                    && Grade == x.Grade
                    && PathwayID == x.StreamingID
                    && Level == x.Level
                ).ToList();
                var retData = (from p in arrData
                               join mpc in arrDataProductCategory on p.ProductCategoryID equals mpc.ID
                               join mm in arrDataMerchant on p.MerchantID equals mm.ID
                               select new HomeProductListResponse
                               {
                                   ID = p.ID,
                                   Name = p.Name,
                                   ProductPhoto = arrDataProductMedia.Where(x => x.ProductID == p.ID && x.IsPrimary == true).FirstOrDefault().MediaURL,
                                   ProductSKU = p.ProductSKU,
                                   DiscountType = p.DiscountType,
                                   DiscountValue = p.DiscountValue,
                                   DiscountedPrice = (p.DiscountType == "1" ? (p.Price - p.DiscountValue) : p.Price - ((p.Price * p.DiscountValue)/100)),
                                   Price = p.Price,
                                   TotalSale = p.TotalSale.ToString(),
                                   ProductCategory = mpc.Name,
                                   MerchantName = mm.Name,
                               }).ToList();

                if (arrDataMappingProduct.Count() > 0)
                {
                    listProductMappingID = arrDataMappingProduct.Select(x => x.ID).Distinct().ToList();
                    var listSKU = arrDataMappingProductSKU.Where(x => listProductMappingID.Contains(x.MappingProductID)).Select(x => x.ProductSKU).Distinct().ToList();

                    retData = retData.Where(x => listSKU.Contains(x.ProductSKU)).ToList();
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