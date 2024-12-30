using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBinusstoreAPI.Model
{
    [DatabaseName("DB")]
    [Table("MsMenu")]
    public class MsMenu : BaseModel
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public Int32 SequenceNo { get; set; }
        public string MenuUrl { get; set; }
        public string ParentID { get; set; }
        public string IsParent { get; set; }




        public string Stsrc { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}

