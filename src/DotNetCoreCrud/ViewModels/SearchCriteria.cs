using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable enable
namespace DotNetCoreCrud.ViewModels
{
    public class SearchCriteria
    {
        public string? ProvinceCode { get; set; }
        public bool? IsActive { get; set; }
        public List<int>? Medicines { get; set; }
    }
}