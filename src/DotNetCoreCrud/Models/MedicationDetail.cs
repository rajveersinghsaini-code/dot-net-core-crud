using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DotNetCoreCrud.Models
{
    public partial class MedicationDetail
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MedicineId { get; set; }

        [ForeignKey("MedicineId")]
        public virtual MedicineInfo Medicine { get; set; }

        [ForeignKey("UserId")]
        public virtual UserInformation User { get; set; }
    }
}
