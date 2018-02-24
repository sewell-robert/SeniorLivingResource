using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.Models
{
    public class CommunityClass
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Area { get; set; }
        public string CareLevel { get; set; }

        public CommunityClass(string name, string price, string area, string careLevel)
        {
            Name = name;
            Price = price;
            Area = area;
            CareLevel = careLevel;
        }
    } 
}
