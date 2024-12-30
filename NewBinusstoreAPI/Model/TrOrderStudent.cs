using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBinusstoreAPI.Model
{
    [DatabaseName("DB")]
    [Table("TrOrderStudent")]
    public class TrOrderStudent : BaseModel
    {
        [Key]
        public string ID { get; set; }
        public string OrderID { get; set; }
        public string StudentID { get; set; }
        public string Name { get; set; }
        public string SchoolID { get; set; }
        public string SchoolName { get; set; }
        public string Grade { get; set; }
        public string StreamingID { get; set; }
        public string StreamingName { get; set; }
        public string Level { get; set; }


        public string Stsrc { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}

