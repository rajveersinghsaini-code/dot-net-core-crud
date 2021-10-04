using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DotNetCoreCrud.Models
{
    public partial class ProvinceInfo
    {
        public ProvinceInfo()
        {
            UserInformations = new List<UserInformation>();
        }
        [Key]
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual List<UserInformation> UserInformations { get; set; }
    }
}
