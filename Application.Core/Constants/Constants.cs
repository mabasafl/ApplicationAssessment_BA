using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Constants
{
    public class Constants
    {
        public const string Unique_Regex = @"^[a-zA-Z0-9]+(?:-[a-zA-Z0-9]+)*(?:\s[a-zA-Z0-9]+(?:-[a-zA-Z0-9]+)*)*$";
        
        public const string Email_Regex = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
        public const string _EmailAddress = "Email Address";
        public const string _Name = "Name";
    }
}
