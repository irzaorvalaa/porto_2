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
    public class ProductVariantHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<ProductVariantResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrDataVariant = EntityHelper.Get<MsVariant>(x => x.Stsrc != "D").ToList();
                var arrDataVariantOption = EntityHelper.Get<MsVariantOption>(x => x.Stsrc != "D").ToList();
                var arrDataProductCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                var arrDataProduct = EntityHelper.Get<MsProduct>(x => x.Stsrc != "D").ToList();
                var arrDataProductVariant = EntityHelper.Get<MsProductVariant>(x => x.Stsrc != "D").ToList();

                var retData = (from mv in arrDataVariant
                               join mvo in arrDataVariantOption on mv.ID equals mvo.VariantID
                               join mpc in arrDataProductCategory on mv.ProductCategoryID equals mpc.ID
                               join mp in arrDataProduct on mpc.ID equals mp.ProductCategoryID
                               join mpv in arrDataProductVariant on mv.ID equals mpv.VariantID
                               select new ProductVariantResponse
                               {
                                   ID = mv.ID,
                                   ProductCategoryID = mv.ProductCategoryID,
                                   VariantName = mv.Name,
                                   VariantOptionName = mvo.Name,
                                   ProductCategoryName = mpc.Name,
                                   IsActive = mpv.IsActive,
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ProductVariantResponse GetSingle(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataVariant = EntityHelper.Get<MsVariant>(x => x.Stsrc != "D").ToList();
                var arrDataVariantOption = EntityHelper.Get<MsVariantOption>(x => x.Stsrc != "D").ToList();
                var arrDataProductCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                var arrDataProduct = EntityHelper.Get<MsProduct>(x => x.Stsrc != "D").ToList();
                var arrDataProductVariant = EntityHelper.Get<MsProductVariant>(x => x.Stsrc != "D").ToList();

                var retData = (from mv in arrDataVariant
                               join mvo in arrDataVariantOption on mv.ID equals mvo.VariantID
                               join mpc in arrDataProductCategory on mv.ProductCategoryID equals mpc.ID
                               join mp in arrDataProduct on mpc.ID equals mp.ProductCategoryID
                               join mpv in arrDataProductVariant on mv.ID equals mpv.VariantID
                               select new ProductVariantResponse
                               {
                                   ID = mv.ID,
                                   ProductCategoryID = mv.ProductCategoryID,
                                   VariantName = mv.Name,
                                   VariantOptionName = mvo.Name,
                                   ProductCategoryName = mpc.Name,
                                   IsActive = mpv.IsActive,
                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ProductVariantResponse> GetCategoryProduct(ValidTokenModel tokenData, string ID)
        {
            try
            {

                var arrData = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new ProductVariantResponse
                               {
                                   ProductCategoryID = a.ID,
                                   ProductCategoryName = a.Name
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CustomResponse Save(ValidTokenModel tokenData, ProductVariantPayload formData)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "doReloadTable()";
            try
            {
                if (formData.Action == "Add")
                {
                    var NewID = Guid.NewGuid().ToString().ToUpper();
                    var data = new MsVariantOption()
                      {
                        ID = NewID,
                        Name = formData.VariantOption,
                        VariantID = formData.Name,

                        Stsrc = "A",
                        UserIn = tokenData.UserID,
                        DateIn = DateTime.Now
                    };
                    EntityHelper.Add<MsVariantOption>(data);

                    var subDataToAdd = new List<MsProductCategory>();
                    foreach (var ProductCategoryName in formData.ProductCategoryName)
                    {
                        if (ProductCategoryName != "")
                        {
                            subDataToAdd.Add(new MsProductCategory
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                Name = ProductCategoryName,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToAdd.Count > 0) EntityHelper.Add<MsProductCategory>(subDataToAdd);
                }
                else if (formData.Action == "Edit")
                {
                    var arrData = EntityHelper.Get<MsVariantOption>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Name = formData.VariantOption;
                    arrData.VariantID = formData.Name;


                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsVariantOption>(arrData);

                    var subDataToDelete = EntityHelper.Get<MsVariantOption>(x => x.ID == arrData.ID).ToList();
                    if (subDataToDelete.Count > 0) EntityHelper.Delete<MsVariantOption>(subDataToDelete);

                    var subDataToEdit = new List<MsProductCategory>();
                    foreach (var ProductCategoryName in formData.ProductCategoryName)
                    {
                        if (ProductCategoryName != "")
                        {
                            subDataToEdit.Add(new MsProductCategory
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                Name = ProductCategoryName,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToEdit.Count > 0) EntityHelper.Add<MsProductCategory>(subDataToEdit);
                }
                else if (formData.Action == "Delete")
                {
                    var arrData = EntityHelper.Get<MsVariantOption>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Stsrc = "D";
                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsVariantOption>(arrData);
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