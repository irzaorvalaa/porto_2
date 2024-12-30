using NewBinusstoreAPI.Model;
using Binus.WS.Pattern.Entities;
using Binus.WS.Pattern.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.ProjectServer.Client;

namespace NewBinusstoreAPI.Helper
{
    public class ProductDetailHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<ProductDetailResponse> Get(ValidTokenModel tokenData,
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
                    && (ProductType == "1" ? x.IsMandatory == true : (ProductType == "2" ? x.IsReligion == true : true == true)))
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
                               join mpm in arrDataProductMedia on p.ID equals mpm.ProductID

                               select new ProductDetailResponse
                               {
                                   ID = p.ID,
                                   Name = p.Name,
                                   ProductSKU = p.ProductSKU,
                                   DiscountType = p.DiscountType,
                                   DiscountValue = p.DiscountValue,
                                   Price = p.Price,
                                   DiscountedPrice = (p.DiscountType == "1" ? (p.Price - p.DiscountValue) : p.Price - ((p.Price * p.DiscountValue) / 100)),
                                   TotalSale = p.TotalSale,
                                   Stock = p.Stock,
                                   Description = p.Description,
                                   ProductCategory = mpc.Name,
                                   MerchantName = mm.Name,
                                   MediaUrl = mpm.MediaURL,
                                   YTEmbed = mpm.YTEmbed
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

        public static ProductDetailResponse GetSingle(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrData = EntityHelper.Get<MsProduct>(
                x => x.Stsrc != "D" && x.ID == ID)
                    .ToList();
                var listProductID = arrData.Select(x => x.ID).Distinct().ToList();
                var listProductSKU = arrData.Select(x => x.ProductSKU).Distinct().ToList();


                var arrDataProductCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D" && x.ID == ID).ToList();
                var arrDataProductMedia = EntityHelper.Get<MsProductMedia>(x => x.Stsrc != "D" && x.ID == ID).ToList();
                var arrDataMerchant = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D" && x.ID == ID).ToList();

                var arrDataMappingProductSKU = EntityHelper.Get<MsMappingProductSKU>(x => x.Stsrc != "D" && x.ID == ID && listProductSKU.Contains(x.ProductSKU)).ToList();
                var listProductMappingID = arrDataMappingProductSKU.Select(x => x.MappingProductID).Distinct().ToList();

                var arrDataMappingProduct = EntityHelper.Get<MsMappingProduct>(
                   x => x.Stsrc != "D"
                   && listProductMappingID.Contains(x.ID)

               ).ToList();

                var retData = (from p in arrData
                               join mpc in arrDataProductCategory on p.ProductCategoryID equals mpc.ID
                               join mm in arrDataMerchant on p.MerchantID equals mm.ID
                               join mpm in arrDataProductMedia on p.ID equals mpm.ProductID

                               select new ProductDetailResponse
                               {
                                   ID = p.ID,
                                   Name = p.Name,
                                   ProductSKU = p.ProductSKU,
                                   DiscountType = p.DiscountType,
                                   DiscountValue = p.DiscountValue,
                                   Price = p.Price,
                                   DiscountedPrice = (p.DiscountType == "1" ? (p.Price - p.DiscountValue) : p.Price - ((p.Price * p.DiscountValue) / 100)),
                                   TotalSale = p.TotalSale,
                                   Stock = p.Stock,
                                   Description = p.Description,
                                   ProductCategory = mpc.Name,
                                   MerchantName = mm.Name,
                                   MediaUrl = mpm.MediaURL,
                                   YTEmbed = mpm.YTEmbed
                               }).FirstOrDefault();

                if (arrDataMappingProduct.Count() > 0)
                {
                    listProductMappingID = arrDataMappingProduct.Select(x => x.ID).Distinct().ToList();
                    var listSKU = arrDataMappingProductSKU.Where(x => listProductMappingID.Contains(x.MappingProductID)).Select(x => x.ProductSKU).Distinct().ToList();


                }

                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ProductDetailMediaResponse> GetProductMedia(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var retData = new List<ProductDetailMediaResponse>();
                var arrData = EntityHelper.Get<MsProductMedia>(x => x.Stsrc != "D" && x.ID == ID).ToList();
                if (arrData.Count() > 0)
                {
                    retData = (from a in arrData
                              
                               select new ProductDetailMediaResponse
                               {
                                   ProductID = a.ID,
                                   YTEmbed = a.YTEmbed,
                                   MediaUrl = a.MediaURL
                             
                               }).ToList();
                }
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static List<ProductDetailVariantResponse> GetProductVariant(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var retData = new List<ProductDetailVariantResponse>();

                var arrDataProductVariant = EntityHelper.Get<MsProductVariant>(x => x.Stsrc != "D" && x.ID == ID).ToList();
                var arrDataVariant = EntityHelper.Get<MsVariant>(x => x.Stsrc != "D" && x.ID == ID).ToList();

                
                {
                    retData = (from mpv in arrDataProductVariant
                               join mv in arrDataVariant on mpv.VariantID equals mv.ID

                               select new ProductDetailVariantResponse
                               {
                                   VariantName = mv.Name,
                                   AdditionalPrice = mpv.AdditionalPrice
                       

                               }).ToList();
                }
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static List<ProductDetailVariantResponse> GetRelatedItem(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var retData = new List<ProductDetailVariantResponse>();

                var arrDataProductVariant = EntityHelper.Get<MsProductVariant>(x => x.Stsrc != "D" && x.ID == ID).ToList();
                var arrDataVariant = EntityHelper.Get<MsVariant>(x => x.Stsrc != "D" && x.ID == ID).ToList();


                {
                    retData = (from mpv in arrDataProductVariant
                               join mv in arrDataVariant on mpv.VariantID equals mv.ID

                               select new ProductDetailVariantResponse
                               {
                                   VariantName = mv.Name,
                                   AdditionalPrice = mpv.AdditionalPrice


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
    
