using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.Models
{
    public class UserPrefs
    {
        public int ID { get; set; }
        public string UsersPrice { get; set; }
        public string UsersArea { get; set; }
        public string UsersCareLevel { get; set; }
    }
}
