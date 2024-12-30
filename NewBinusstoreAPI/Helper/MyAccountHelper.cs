using NewBinusstoreAPI.Model;
using Binus.WS.Pattern.Entities;
using Binus.WS.Pattern.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Runtime.Intrinsics.X86;

namespace NewBinusstoreAPI.Helper
{
    public class MyAccountHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<MyAccountResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrDataStudent = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D").ToList();
                var arrDataCustomer = EntityHelper.Get<MsCustomer>(x => x.Stsrc != "D").ToList();

                var retData = (from ms in arrDataStudent
                               join mc in arrDataCustomer on ms.CustomerID equals mc.ID
                               select new MyAccountResponse
                               {
                                   ID = mc.ID,
                                   SiblingGroupID = ms.SiblingGroupID,
                                   StudentNumber = ms.StudentNumber,
                                   Grade = ms.Grade,
                                   Level = ms.Level,
                                   SchoolName = ms.SchoolName,
                                   Name = mc.Name,
                                   Gender = mc.Gender,
                                   Email = mc.Email,
                                   Phone = mc.Phone
                                                               
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MyAccountResponse GetSingle(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrDataStudent = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D").ToList();
                var arrDataCustomer = EntityHelper.Get<MsCustomer>(x => x.Stsrc != "D").ToList();

                var retData = (from ms in arrDataStudent
                               join mc in arrDataCustomer on ms.CustomerID equals mc.ID
                               select new MyAccountResponse
                               {
                                   ID = ms.ID,
                                   SiblingGroupID = ms.SiblingGroupID,
                                   StudentNumber = ms.StudentNumber,
                                   Grade = ms.Grade,
                                   Level = ms.Level,
                                   SchoolName = ms.SchoolName,
                                   Name = mc.Name,
                                   Gender = mc.Gender,
                                   Email = mc.Email,
                                   Phone = mc.Phone

                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MyAccountListStationaryResponse GetListItem(ValidTokenModel tokenData, string ProductCategoryID)
        {
            try
            {
                var arrDataProduct = EntityHelper.Get<MsProduct>(x => x.Stsrc != "D" && x.ProductCategoryID == ProductCategoryID).ToList();
                var arrDataMappingProduct = EntityHelper.Get<MsMappingProduct>(x => x.Stsrc != "D").ToList();
                var arrDataMappingProductSKU = EntityHelper.Get<MsMappingProductSKU>(x => x.Stsrc != "D").ToList();

                var retData = (from p in arrDataProduct
                               join mps in arrDataMappingProductSKU on p.ProductSKU equals mps.ProductSKU
                               join mp in arrDataMappingProduct on mps.MappingProductID equals mp.ID
                               select new MyAccountListStationaryResponse
                               {
                                    ProductSKU = p.ProductSKU,
                                    ProductCategoryID = p.ProductCategoryID,
                                    ProductName = p.Name,
                                    IsMandatory = p.IsMandatory,
                                    IsReligion = p.IsReligion,
                                    IsContinuity = mp.IsContinuity,
                                    Price = p.Price
                             
                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CustomResponse Save(ValidTokenModel tokenData, MyAccountEditProfilePayload formData)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "doReloadTable()";
            try
            {
                if (formData.Action == "Edit")
                {
                    var arrData = EntityHelper.Get<MsCustomer>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Name = formData.Name;
                    arrData.Gender = formData.Gender;
                    arrData.Phone = formData.PhoneNumber;



                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsCustomer>(arrData);

                    var subDataToDelete = EntityHelper.Get<MsCustomer>(x => x.ID == arrData.ID).ToList();
                    if (subDataToDelete.Count > 0) EntityHelper.Delete<MsCustomer>(subDataToDelete);
                }
               
                else if (formData.Action == "ChangePassword")
                {
                    var arrData = EntityHelper.Get<MsCustomer>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
      
                    arrData.Password = formData.Password;


                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsCustomer>(arrData);
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