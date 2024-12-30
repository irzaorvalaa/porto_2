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
    public class MappingProductHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<MappingProductResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrDataMappingProduct = EntityHelper.Get<MsMappingProduct>(x => x.Stsrc != "D").ToList();
                var arrDataMappingProductSKU = EntityHelper.Get<MsMappingProductSKU>(x => x.Stsrc != "D").ToList();
                var arrDataMerchant = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D").ToList();
                var arrDataProductCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                var arrDataStudent = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D").ToList();


                var retData = (from mp in arrDataMappingProduct
                               join mps in arrDataMappingProductSKU on mp.ID equals mps.MappingProductID
                               join mc in arrDataMerchant on mp.MerchantID equals mc.ID
                               join mpc in arrDataProductCategory on mp.ProductCategoryID equals mpc.ID
                               join ms in arrDataStudent on mp.StreamingID equals ms.StreamingID

                               select new MappingProductResponse
                               {
                                   School = ms.SchoolName,
                                   Gender = mp.Gender,
                                   Grade = mp.Grade,
                                   Streaming = mp.StreamingID,
                                   Category = mpc.Name
                          
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MappingProductResponse GetSingle(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataMappingProduct = EntityHelper.Get<MsMappingProduct>(x => x.Stsrc != "D").ToList();
                var arrDataMappingProductSKU = EntityHelper.Get<MsMappingProductSKU>(x => x.Stsrc != "D").ToList();
                var arrDataMerchant = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D").ToList();
                var arrDataProductCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();
                var arrDataStudent = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D").ToList();


                var retData = (from mp in arrDataMappingProduct
                               join mps in arrDataMappingProductSKU on mp.ID equals mps.MappingProductID
                               join mc in arrDataMerchant on mp.MerchantID equals mc.ID
                               join mpc in arrDataProductCategory on mp.ProductCategoryID equals mpc.ID
                               join ms in arrDataStudent on mp.StreamingID equals ms.StreamingID

                               select new MappingProductResponse
                               {
                                   School = ms.SchoolName,
                                   Gender = mp.Gender,
                                   Grade = mp.Grade,
                                   Streaming = mp.StreamingID,
                                   Category = mpc.Name
                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MappingProductResponse GetSchool(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataSchool = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D").ToList();
                

                var retData = (from ms in arrDataSchool

                               select new MappingProductResponse
                               {
                                   SchoolID = ms.SchoolID,
                                   School = ms.SchoolName,
                                 
                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MappingProductResponse GetGrade(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataGrade = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D").ToList();


                var retData = (from ms in arrDataGrade

                               select new MappingProductResponse
                               {
                                   Grade = ms.Grade,

                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MappingProductResponse GetStreaming(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataStreaming = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D").ToList();


                var retData = (from ms in arrDataStreaming

                               select new MappingProductResponse
                               {
                                   StreamingID = ms.StreamingID,
                                   Streaming = ms.StreamingName

                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MappingProductResponse GetGender(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataGender = EntityHelper.Get<MsMappingProduct>(x => x.Stsrc != "D").ToList();


                var retData = (from mp in arrDataGender

                               select new MappingProductResponse
                               {
                                   Gender = mp.Gender,
                     

                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MappingProductResponse GetCategory(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataCategory = EntityHelper.Get<MsProductCategory>(x => x.Stsrc != "D").ToList();


                var retData = (from mpc in arrDataCategory

                               select new MappingProductResponse
                               {
                                   Category = mpc.Name,


                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CustomResponse Save(ValidTokenModel tokenData, MappingProductPayload formData)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "doReloadTable()";
            try
            {
                if (formData.Action == "Add")
                {
                    var NewID = Guid.NewGuid().ToString().ToUpper();
                    var data = new MsStudent()
                    {
                        ID = NewID,
                        SchoolID = formData.SchoolID,
                        Grade = formData.Grade,
                        StreamingID = formData.StreamingID,
                       
                        Stsrc = "A",
                        UserIn = tokenData.UserID,
                        DateIn = DateTime.Now
                    };
                    EntityHelper.Add<MsStudent>(data);

                    var subDataToAddGender = new List<MsMappingProduct>();
                    foreach (var Gender in formData.Gender)
                    {
                        if (Gender != "")
                        {
                            subDataToAddGender.Add(new MsMappingProduct
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                Gender = Gender,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToAddGender.Count > 0) EntityHelper.Add<MsMappingProduct>(subDataToAddGender);

                    var subDataToAddCategoryID = new List<MsProductCategory>();
                    foreach (var ProductCategoryID in formData.ProductCategoryID)
                    {
                        if (ProductCategoryID != "")
                        {
                            subDataToAddCategoryID.Add(new MsProductCategory
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                 Name = ProductCategoryID,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToAddCategoryID.Count > 0) EntityHelper.Add<MsProductCategory>(subDataToAddCategoryID);

                    var subDataToAddProductSKU = new List<MsMappingProductSKU>();
                    foreach (var ProductSKU in formData.ProductSKU)
                    {
                        if (ProductSKU != "")
                        {
                            subDataToAddProductSKU.Add(new MsMappingProductSKU
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                ProductSKU = ProductSKU,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToAddProductSKU.Count > 0) EntityHelper.Add<MsMappingProductSKU>(subDataToAddProductSKU);

                }
                else if (formData.Action == "Edit")
                {
                    var arrData = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.SchoolID = formData.SchoolID;
                    arrData.Grade = formData.Grade;
                    arrData.StreamingID = formData.StreamingID;
                    

                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsStudent>(arrData);

                    var subDataToDelete = EntityHelper.Get<MsStudent>(x => x.ID == arrData.ID).ToList();
                    if (subDataToDelete.Count > 0) EntityHelper.Delete<MsStudent>(subDataToDelete);

                    var subDataToEditGender = new List<MsMappingProduct>();
                    foreach (var Gender in formData.Gender)
                    {
                        if (Gender != "")
                        {
                            subDataToEditGender.Add(new MsMappingProduct
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                Gender = Gender,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToEditGender.Count > 0) EntityHelper.Add<MsMappingProduct>(subDataToEditGender);

                    var subDataToEditCategoryID = new List<MsProductCategory>();
                    foreach (var ProductCategoryID in formData.ProductCategoryID)
                    {
                        if (ProductCategoryID != "")
                        {
                            subDataToEditCategoryID.Add(new MsProductCategory
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                Name = ProductCategoryID,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToEditCategoryID.Count > 0) EntityHelper.Add<MsProductCategory>(subDataToEditCategoryID);

                    var subDataToEditProductSKU = new List<MsMappingProductSKU>();
                    foreach (var ProductSKU in formData.ProductSKU)
                    {
                        if (ProductSKU != "")
                        {
                            subDataToEditProductSKU.Add(new MsMappingProductSKU
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                ProductSKU = ProductSKU,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToEditProductSKU.Count > 0) EntityHelper.Add<MsMappingProductSKU>(subDataToEditProductSKU);
                }
                else if (formData.Action == "Delete")
                {
                    var arrData = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Stsrc = "D";
                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsStudent>(arrData);
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