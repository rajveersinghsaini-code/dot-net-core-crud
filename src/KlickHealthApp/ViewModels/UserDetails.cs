using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlickHealthApp.ViewModels
{
    public class UserDetails
    {
        public UserDetails()
        {
            Medicines = new List<int>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string ProvinceCode { get; set; }
        public List<int> Medicines { get; set; }
        public string CreatedBy { get; set; }
    }
}
