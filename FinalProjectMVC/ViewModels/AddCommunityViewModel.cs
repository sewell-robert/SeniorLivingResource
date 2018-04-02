using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.ViewModels
{
    public class AddCommunityViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Price { get; set; }

        [Required]
        public string Area { get; set; }

        [Required]
        public string CareLevel { get; set; }

        public AddCommunityViewModel() { }
    }
}
