using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.Models
{
    public class Community
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }

        public string Price { get; set; }
        public string Area { get; set; }
        public string CareLevel { get; set; }

        public Community(string name, string price, string area, string careLevel)
        {
            Name = name;
            Price = price;
            Area = area;
            CareLevel = careLevel;
        }

        public Community() { }
    } 
}
