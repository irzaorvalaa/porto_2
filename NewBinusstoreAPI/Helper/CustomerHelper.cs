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
    public class CustomerHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<CustomerResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsCustomer>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new CustomerResponse
                               {
                                   ID = a.ID,
                                   Name = a.Name,
                                   CustomerType = a.CustomerType,
                                   Email = a.Email,
                                   Phone = a.Phone,
                                   Gender = a.Gender,
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public static CustomerResponse GetSingle(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrData = EntityHelper.Get<MsCustomer>(x => x.Stsrc != "D" && x.ID == ID).ToList();
                var retData = (from a in arrData
                               select new CustomerResponse
                               {
                                   ID = a.ID,
                                   Name = a.Name,
                                   CustomerType = a.CustomerType,
                                   Email = a.Email,
                                   Phone = a.Phone,
                                   Gender = a.Gender,
                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CustomerFormResponse> GetSchool(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new CustomerFormResponse
                               {
                                   SchoolName = a.SchoolName,
                                   

                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CustomerFormResponse> GetGrade(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new CustomerFormResponse
                               {
                                   Grade = a.Grade,


                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CustomerFormResponse> GetStreaming(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsStudent>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new CustomerFormResponse
                               {

                                   StreamingName = a.StreamingName,


                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CustomResponse Save(ValidTokenModel tokenData, CustomerPayload formData)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "doReloadTable()";
            try
            {
                if (formData.Action == "Add")
                {
                    var NewID = Guid.NewGuid().ToString().ToUpper();
                    var data = new MsCustomer()
                    {
                        ID = NewID,
                        Name = formData.Name,
                        CustomerType = formData.CustomerType,
                        Email = formData.Email,
                        Phone = formData.PhoneNumber,
                        Gender = formData.Gender,

                        Stsrc = "A",
                        UserIn = tokenData.UserID,
                        DateIn = DateTime.Now
                    };
                    EntityHelper.Add<MsCustomer>(data);

                    var subDataToAdd = new List<MsStudent>();
                    foreach (var StudentID in formData.StudentID)
                    {
                        if (StudentID != "")
                        {
                            subDataToAdd.Add(new MsStudent
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                StudentNumber = StudentID,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToAdd.Count > 0) EntityHelper.Add<MsStudent>(subDataToAdd);
                }
                else if (formData.Action == "Edit")
                {
                    var arrData = EntityHelper.Get<MsCustomer>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Name = formData.Name;
                    arrData.CustomerType = formData.CustomerType;
                    arrData.Email = formData.Email;
                    arrData.Phone = formData.PhoneNumber;
                    arrData.Gender = formData.Gender;
                   

                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsCustomer>(arrData);

                    var subDataToDelete = EntityHelper.Get<MsCustomer>(x => x.ID == arrData.ID).ToList();
                    if (subDataToDelete.Count > 0) EntityHelper.Delete<MsCustomer>(subDataToDelete);

                    var subDataToEdit = new List<MsStudent>();
                    foreach (var StudentID in formData.StudentID)
                    {
                        if (StudentID != "")
                        {
                            subDataToEdit.Add(new MsStudent
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                StudentNumber = StudentID,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToEdit.Count > 0) EntityHelper.Add<MsStudent>(subDataToEdit);
                }
                else if (formData.Action == "Delete")
                {
                    var arrData = EntityHelper.Get<MsCustomer>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Stsrc = "D";
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