using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DotNetCoreCrud.Models
{
    public partial class MedicineInfo
    {
        public MedicineInfo()
        {
            MedicationDetails = new List<MedicationDetail>();
        }

        [Key]
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual List<MedicationDetail> MedicationDetails { get; set; }
    }
}
