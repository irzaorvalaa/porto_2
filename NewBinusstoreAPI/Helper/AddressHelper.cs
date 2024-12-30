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
    public class AddressHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<AddressResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsCustomerAddress>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new AddressResponse
                               {
                                   ID = a.ID,
                                   Address = a.Address,
                                   CityID = a.CityID,
                                   StateID = a.StateID,
                                   PhoneNumber = a.PhoneNumber,
                                   PostalCode = a.PostalCode,
                                   RecipientName = a.RecipientName
                            
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static AddressResponse GetSingle(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrData = EntityHelper.Get<MsCustomerAddress>(x => x.Stsrc != "D" && x.ID == ID).ToList();
                var retData = (from a in arrData
                               select new AddressResponse
                               {
                                   ID = a.ID,
                                   Address = a.Address,
                                   CityID = a.CityID,
                                   StateID = a.StateID,
                                   PhoneNumber = a.PhoneNumber,
                                   PostalCode = a.PostalCode,
                                   RecipientName = a.RecipientName
                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<AddressResponse> GetAddress(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsCustomerAddress>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new AddressResponse
                               {
                         
                                   Address = a.Address,
                         

                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<AddressResponse> GetProvince(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsCustomerAddress>(x => x.Stsrc != "D").ToList();
                var arrDataState = EntityHelper.Get<MsState>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               join b in arrDataState on a.StateID equals b.ID
                               select new AddressResponse
                               {

                                   StateID = a.StateID,
                                   StateName = b.Name


                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<AddressResponse> GetCity(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsCustomerAddress>(x => x.Stsrc != "D").ToList();
                var arrDataCity = EntityHelper.Get<MsCity>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               join b in arrDataCity on a.CityID equals b.ID
                               select new AddressResponse
                               {

                                   CityID = a.CityID,
                                   CityName = b.Name

                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CustomResponse Save(ValidTokenModel tokenData, AddressPayload formData)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "doReloadTable()";
            try
            {
                if (formData.Action == "Add")
                {
                    var NewID = Guid.NewGuid().ToString().ToUpper();
                    var data = new MsCustomerAddress()
                    {
                        ID = NewID,
                        RecipientName = formData.RecipientName,
                        PhoneNumber = formData.PhoneNumber,
                        Email = formData.Email,
                        StateID = formData.StateID,
                        Address = formData.Address,
                        CityID = formData.CityID,
                        PostalCode = formData.PostalCode,
                         

                        Stsrc = "A",
                        UserIn = tokenData.UserID,
                        DateIn = DateTime.Now
                    };
                    EntityHelper.Add<MsCustomerAddress>(data);
                }
                else if (formData.Action == "Edit")
                {
                    var arrData = EntityHelper.Get<MsCustomerAddress>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.RecipientName = formData.RecipientName;
                    arrData.PhoneNumber = formData.PhoneNumber;
                    arrData.Email = formData.Email;
                    arrData.StateID = formData.StateID;
                    arrData.Address = formData.Address;
                    arrData.CityID = formData.CityID;
                    arrData.PostalCode = formData.PostalCode;
             

                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsCustomerAddress>(arrData);

                    var subDataToDelete = EntityHelper.Get<MsCustomerAddress>(x => x.ID == arrData.ID).ToList();
                    if (subDataToDelete.Count > 0) EntityHelper.Delete<MsCustomerAddress>(subDataToDelete);
                }
                else if (formData.Action == "Delete")
                {
                    var arrData = EntityHelper.Get<MsCustomerAddress>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Stsrc = "D";
                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsCustomerAddress>(arrData);
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