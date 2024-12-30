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
    public class UserManagementHelper
    {
        private static RedisHelper redis = new RedisHelper();
        public static List<UserManagementResponse> Get(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsUser>(x => x.Stsrc != "D").ToList();
                var arrDataRole = EntityHelper.Get<MsGroupRole>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               join b in arrDataRole on a.GroupRoleID equals b.ID
                               select new UserManagementResponse
                               {
                                   ID = a.ID,
                                   Name = a.Name,
                                   Email = a.Email,
                                   GroupRole = b.ID,
                                   UserType = a.UserType,
                                   IsActive = a.IsActive,
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public static UserManagementResponse GetSingle(ValidTokenModel tokenData, string ID)
        {
            try
            {
                var arrData = EntityHelper.Get<MsUser>(x => x.Stsrc != "D" && x.ID == ID).ToList();
                var arrDataRole = EntityHelper.Get<MsGroupRole>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               join b in arrDataRole on a.GroupRoleID equals b.ID
                               select new UserManagementResponse
                               {
                                   ID = a.ID,
                                   Name = a.Name,
                                   Email = a.Email,
                                   GroupRole = b.ID,
                                   UserType = a.UserType,
                                   IsActive = a.IsActive,
                               }).FirstOrDefault();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CustomResponse Save(ValidTokenModel tokenData, UserManagementPayload formData)
        {
            bool retStatus = true;
            string retMessage = "";
            string retCallback = "doReloadTable()";
            try
            {
                if (formData.Action == "Add")
                {
                    var NewID = Guid.NewGuid().ToString().ToUpper();
                    var data = new MsUser()
                    {
                        ID = NewID,
                        Name = formData.Name,
                        Email = formData.Email,
                        UserType = formData.UserType,
                        GroupRoleID = formData.RoleID,
                        IsActive = true,

                        Stsrc = "A",
                        UserIn = tokenData.UserID,
                        DateIn = DateTime.Now
                    };
                    EntityHelper.Add<MsUser>(data);

                    var subDataToAdd = new List<MsUserMerchant>();
                    foreach (var MerchantID in formData.MerchantID)
                    {
                        if (MerchantID != "")
                        {
                            subDataToAdd.Add(new MsUserMerchant
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                UserID = data.ID,
                                MerchantID = MerchantID,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToAdd.Count > 0) EntityHelper.Add<MsUserMerchant>(subDataToAdd);

                   

                }
                else if (formData.Action == "Edit")
                {
                    var arrData = EntityHelper.Get<MsUser>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Name = formData.Name;
                    arrData.Email = formData.Email;
                    arrData.UserType = formData.UserType;
                    arrData.GroupRoleID = formData.RoleID;
                    arrData.IsActive = formData.IsActive;

                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsUser>(arrData);


                    var subDataToDelete = EntityHelper.Get<MsUserMerchant>(x => x.UserID == arrData.ID).ToList();
                    if (subDataToDelete.Count > 0) EntityHelper.Delete<MsUserMerchant>(subDataToDelete);


                    var subDataToEdit = new List<MsUserMerchant>();
                    foreach (var MerchantID in formData.MerchantID)
                    {
                        if (MerchantID != "")
                        {
                            subDataToEdit.Add(new MsUserMerchant
                            {
                                ID = Guid.NewGuid().ToString().ToUpper(),
                                UserID = arrData.ID,
                                MerchantID = MerchantID,

                                Stsrc = "A",
                                UserIn = tokenData.UserID,
                                DateIn = DateTime.Now
                            });
                        }
                    }
                    if (subDataToEdit.Count > 0) EntityHelper.Add<MsUserMerchant>(subDataToEdit);

                    
                }
                else if (formData.Action == "Delete")
                {
                    var arrData = EntityHelper.Get<MsUser>(x => x.Stsrc != "D" && x.ID == formData.ID).FirstOrDefault();
                    arrData.Stsrc = "D";
                    arrData.UserUp = tokenData.UserID;
                    arrData.DateUp = DateTime.Now;
                    EntityHelper.Update<MsUser>(arrData);
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

        public static List<UserManagementMerchantResponse> GetMerchant(ValidTokenModel tokenData)
        {
            try
            {

                var arrData = EntityHelper.Get<MsMerchant>(x => x.Stsrc != "D").ToList();
                var retData = (from a in arrData
                               select new UserManagementMerchantResponse
                               {
                                   ID = a.ID,
                                   MerchantName = a.Name,
                               }).ToList();
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static  List<UserManagementRoleResponse> GetRole(ValidTokenModel tokenData)
        {
            try
            {
                var arrData = EntityHelper.Get<MsGroupRole>(x => x.Stsrc != "D" ).ToList();

              
                
                    var retData = (from a in arrData
                               select new UserManagementRoleResponse
                               {
                                   ID = a.ID,
                                   RoleName = a.Name,
                               }).ToList();
                
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}