using NewBinusstoreAPI.Model;
using Binus.WS.Pattern.Entities;
using Binus.WS.Pattern.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DocumentFormat.OpenXml.Office2010.Excel;
using MassTransit;

namespace NewBinusstoreAPI.Helper
{
    public class ProductHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<ProductResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrDataProduct = EntityHelper.Get<MsProduct>(x => x.Stsrc != "D").ToList();
                var arrDataMerchant = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D").ToList();
                var arrDataProductCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                var arrDataOrderProduct = EntityHelper.Get<TrOrderProduct>(x => x.Stsrc != "D").ToList();

                var retData = (from mp in arrDataProduct
                               join mm in arrDataMerchant on mp.MerchantID equals mm.ID
                               join mpc in arrDataProductCategory on mp.ProductCategoryID equals mpc.ID
                               join op in arrDataOrderProduct on mpc.ID equals op.ProductCategoryID

                               select new ProductResponse
                               {
                                  ID = mp.ID,
                                  ProductSKU = mp.ProductSKU,
                                  MerchantName = mm.Name,
                                  ProductName = mp.Name,
                                  CategoryName = mpc.Name,
                                  Qty = op.Qty,
                                  Price = mp.Price

                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ProductResponse GetSingle(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataProduct = EntityHelper.Get<MsProduct>(x => x.Stsrc != "D").ToList();
                var arrDataMerchant = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D").ToList();
                var arrDataProductCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                var arrDataOrderProduct = EntityHelper.Get<TrOrderProduct>(x => x.Stsrc != "D").ToList();

                var retData = (from mp in arrDataProduct
                               join mm in arrDataMerchant on mp.MerchantID equals mm.ID
                               join mpc in arrDataProductCategory on mp.ProductCategoryID equals mpc.ID
                               join op in arrDataOrderProduct on mpc.ID equals op.ProductCategoryID
                               select new ProductResponse
                               {
                                   ID = mp.ID,
                                   ProductSKU = mp.ProductSKU,
                                   MerchantName = mm.Name,
                                   ProductName = mp.Name,
                                   CategoryName = mpc.Name,
                                   Qty = op.Qty,
                                   Price = mp.Price
                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<ProductMerchantResponse> GetMerchant(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataMerchant = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D").ToList();
                

                var retData = (from mm in arrDataMerchant
                               
                               select new ProductMerchantResponse
                               {
                                  ID = mm.ID,
                                  MerchantName = mm.Name


                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ProductFormCategoryResponse> GetCategoryProduct(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataProductCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();


                var retData = (from mpc in arrDataProductCategory

                               select new ProductFormCategoryResponse
                               {
                                   ID = mpc.ID,
                                   CategoryName = mpc.Name


                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ProductFormVariantResponse> GetVariant(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataVariant = EntityHelper.Get<MsVariant>(x => x.Stsrc != "D").ToList();
                var arrDataVariantOption = EntityHelper.Get<MsVariantOption>(x => x.Stsrc != "D").ToList();


                var retData = (from mv in arrDataVariant
                               join mvo in arrDataVariantOption on mv.ID equals mvo.VariantID

                               select new ProductFormVariantResponse
                               {
                                   ID = mv.ID,
                                   VariantName = mv.Name,
                                   VariantOptionID = mvo.ID,
                                   VariantOptionName = mvo.Name


                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ProductFormResponse> GetProduct(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataProduct = EntityHelper.Get<MsProduct>(x => x.Stsrc != "D").ToList();
                var arrDataProductMedia = EntityHelper.Get<MsProductMedia>(x => x.Stsrc != "D").ToList();
                var arrDataVariant = EntityHelper.Get<MsVariant>(x => x.Stsrc != "D").ToList();
                var arrDataVariantOption = EntityHelper.Get<MsVariantOption>(x => x.Stsrc != "D").ToList();
                var arrDataProductVariant = EntityHelper.Get<MsProductVariant>(x => x.Stsrc != "D").ToList();


                var retData = (from mp in arrDataProduct
                               join mpm in arrDataProductMedia on mp.ID equals mpm.ProductID
                               join mpv in arrDataProductVariant on mp.ID equals mpv.ProductID
                               join mv in arrDataVariant on mpv.VariantID equals mv.ID
                               join mvo in arrDataVariantOption on mv.ID equals mvo.VariantID
                               select new ProductFormResponse
                               {
                                    ID = mp.ID,
                                    VariantName = mv.Name,
                                    AdditionalPrice = mpv.AdditionalPrice,
                                    MediaUrl = mpm.MediaURL,
                                    Description = mp.Description,
                                    IsActive = mp.IsActive,
                                    ProductPhotoUrl = mpv.ProductPhotoUrl
                                 


                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static CustomResponse Save(ValidTokenModel tokenData, ProductPayload formData)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "doReloadTable()";
            try
            {
                if (formData.Action == "Add")
                {
                    var NewID = Guid.NewGuid().ToString().ToUpper();
                    var data = new MsProduct()
                    {
                        ID = NewID,
                        ProductSKU = formData.ProductSKU,
                        MerchantID = formData.MerchantID,
                        Name = formData.ProductName,
                        ProductCategoryID = formData.ProductCategoryID,
                        Description = formData.Description,
                        IsActive = formData.IsActive,
               

                        Stsrc = "A",
                        UserIn = tokenData.UserID,
                        DateIn = DateTime.Now
                    };
                    EntityHelper.Add<MsProduct>(data);

                    var subDataToAddProductMedia = new List<MsProductMedia>();
                    if (formData.MainPhoto != "")
                    {
                        subDataToAddProductMedia.Add(new MsProductMedia
                        {
                            ID = Guid.NewGuid().ToString().ToUpper(),
                            MediaURL = formData.MainPhoto,
                            IsPrimary = true,

                            Stsrc = "A",
                            UserIn = tokenData.UserID,
                            DateIn = DateTime.Now
                        });
                    }
                    foreach (var ProductPhotoUrl in formData.ProductPhotoUrl)
                    {
                        if (ProductPhotoUrl != "")
                        {
                            subDataToAddProductMedia.Add(new MsProductMedia
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                MediaURL = ProductPhotoUrl,
                                IsPrimary = false,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                        
                    }
                    if (subDataToAddProductMedia.Count > 0) EntityHelper.Add<MsProductMedia>(subDataToAddProductMedia);

                    var subDataToAddProductVariant = new List<MsProductVariant>();
                    var index = 0;
                    foreach (var ProductPhotoUrl in formData.ProductPhotoUrl)

                    {
                        if (ProductPhotoUrl != "")
                        {
                            subDataToAddProductVariant.Add(new MsProductVariant
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                AdditionalPrice = formData.AdditionalPrice[index],
                                ProductPhotoUrl = formData.ProductPhotoUrl[index],
                                VariantID = formData.VariantID[index],
                                VariantOptionID = formData.VariantOptiontID[index],
                                ProductID = NewID,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                            index++;
                        }
                    }

                   
                }
                              

                else if (formData.Action == "Edit")
                {

                    var arrData = EntityHelper.Get<MsProduct>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.ProductSKU = formData.ProductSKU;
                    arrData.MerchantID = formData.MerchantID;
                    arrData.Name = formData.ProductName;
                    arrData.ProductCategoryID = formData.ProductCategoryID;
                    arrData.Description = formData.Description;
                    arrData.IsActive = formData.IsActive;


                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsProduct>(arrData);

                    var subDataToDelete = EntityHelper.Get<MsProduct>(x => x.ID == arrData.ID).ToList();
                    if (subDataToDelete.Count > 0) EntityHelper.Delete<MsProduct>(subDataToDelete);

                    var subDataToEditProductMedia = new List<MsProductMedia>();
                    if (formData.MainPhoto != "")
                    {
                        subDataToEditProductMedia.Add(new MsProductMedia
                        {
                            ID = Guid.NewGuid().ToString().ToUpper(),
                            MediaURL = formData.MainPhoto,
                            IsPrimary = true,

                            Stsrc = "A",
                            UserIn = tokenData.UserID,
                            DateIn = DateTime.Now
                        });
                    }
                    foreach (var ProductPhotoUrl in formData.ProductPhotoUrl)
                    {
                        if (ProductPhotoUrl != "")
                        {
                            subDataToEditProductMedia.Add(new MsProductMedia
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                MediaURL = ProductPhotoUrl,
                                IsPrimary = false,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }

                    }
                    if (subDataToEditProductMedia.Count > 0) EntityHelper.Add<MsProductMedia>(subDataToEditProductMedia);

                    var subDataToEditProductVariant = new List<MsProductVariant>();
                    var index = 0;
                    foreach (var ProductPhotoUrl in formData.ProductPhotoUrl)

                    {
                        if (ProductPhotoUrl != "")
                        {
                            subDataToEditProductVariant.Add(new MsProductVariant
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                AdditionalPrice = formData.AdditionalPrice[index],
                                ProductPhotoUrl = formData.ProductPhotoUrl[index],
                                VariantID = formData.VariantID[index],
                                VariantOptionID = formData.VariantOptiontID[index],
                                ProductID = formData.ID,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                            index++;
                        }
                    }
                }
               
                else if (formData.Action == "Delete")
                {
                    var arrData = EntityHelper.Get<MsProduct>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Stsrc = "D";
                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsProduct>(arrData);
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