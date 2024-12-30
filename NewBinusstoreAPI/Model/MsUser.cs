﻿using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBinusstoreAPI.Model
{
    [DatabaseName("DB")]
    [Table("MsUser")]
    public class MsUser : BaseModel
    {
        [Key]
        public string ID { get; set; }
        public string GroupRoleID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }



        public string Stsrc { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}

