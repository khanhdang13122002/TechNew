using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace Tech_News.Helper
{
    public class PasswordHashers:PasswordHasher
    {
        public FormsAuthPasswordFormat FormsAuthPasswordFormat { get; set; }

        public PasswordHashers(FormsAuthPasswordFormat format)
        {
            FormsAuthPasswordFormat = format;
        }

        [Obsolete]
        public override string HashPassword(string password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.ToString());
        }
    }
}