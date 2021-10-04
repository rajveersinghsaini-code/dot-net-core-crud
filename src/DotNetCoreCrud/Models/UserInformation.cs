using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DotNetCoreCrud.Models
{
    public partial class UserInformation
    {
        public UserInformation()
        {
            MedicationDetails = new List<MedicationDetail>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string ProvinceCode { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ProvinceInfo ProvinceCodeNavigation { get; set; }
        public virtual List<MedicationDetail> MedicationDetails { get; set; }
    }
}
