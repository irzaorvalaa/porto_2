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
    public class ProductCategoryHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<ProductCategoryResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new ProductCategoryResponse
                               {
                                   ID = a.ID,
                                   UpperCategoryID = a.UpperCategoryID,
                                   Name = a.Name,
                                   IsBTSPEriod = a.IsBTSPEriod,
                                   IsActive = a.IsActive,
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ProductCategoryResponse GetSingle(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrData = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D" && x.ID == ID).ToList();
                var retData = (from a in arrData
                               select new ProductCategoryResponse
                               {

                                   ID = a.ID,
                                   UpperCategoryID = a.UpperCategoryID,
                                   Name = a.Name,
                                   IsBTSPEriod = a.IsBTSPEriod,
                                   IsActive = a.IsActive,
                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ProductCategoryResponse> GetUpperCategory(ValidTokenModel tokenData)
        {
            try
            {

                var arrData = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new ProductCategoryResponse
                               {
                                   ID = a.ID,
                                   UpperCategoryID = a.UpperCategoryID,
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CustomResponse Save(ValidTokenModel tokenData, ProductCategoryPayload formData)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "doReloadTable()";
            try
            {
                if (formData.Action == "Add")
                {
                    var NewID = Guid.NewGuid().ToString().ToUpper();
                    var data = new MsProductCategory()
                    {
                        ID = NewID,
                        Name = formData.Name,
                        UpperCategoryID = formData.UpperCategoryID,
                        IsActive = true,

                        Stsrc = "A",
                        UserIn = tokenData.UserID,
                        DateIn = DateTime.Now
                    };
                    EntityHelper.Add<MsProductCategory>(data);

                    var subDataToAdd = new List<MsProduct>();
                    foreach (var ProductCategoryID in formData.ProductCategoryID)
                    {
                        if (ProductCategoryID != "")
                        {
                            subDataToAdd.Add(new MsProduct
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                ProductCategoryID = ProductCategoryID,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToAdd.Count > 0) EntityHelper.Add<MsProduct>(subDataToAdd);
                }
                else if (formData.Action == "Edit")
                {
                    var arrData = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Name = formData.Name;
                    arrData.UpperCategoryID = formData.UpperCategoryID;
                    arrData.IsActive = formData.IsActive;

                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsProductCategory>(arrData);

                    var subDataToDelete = EntityHelper.Get<MsProductCategory>(x => x.ID == arrData.ID).ToList();
                    if (subDataToDelete.Count > 0) EntityHelper.Delete<MsProductCategory>(subDataToDelete);

                    var subDataToEdit = new List<MsProduct>();
                    foreach (var ProductCategoryID in formData.ProductCategoryID)
                    {
                        if (ProductCategoryID != "")
                        {
                            subDataToEdit.Add(new MsProduct
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                ProductCategoryID = ProductCategoryID,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToEdit.Count > 0) EntityHelper.Add<MsProduct>(subDataToEdit);
                }
                else if (formData.Action == "Delete")
                {
                    var arrData = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Stsrc = "D";
                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsProductCategory>(arrData);
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