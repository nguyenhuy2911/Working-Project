using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.common
{
    public class ConfigModel
    {
        private string _Logo;
        private string _FaceBook;
        private string _PhoneNumber;
        private string _WebsiteName;
        private string _Address;
        private string _Email;
        public ConfigModel()
        {
            Logo = string.Empty;
            FaceBook = string.Empty;
            PhoneNumber = string.Empty;
            WebsiteName = string.Empty;
            Address = string.Empty;
            Email = string.Empty;
        }
        public string Logo
        {
            get
            {
                return string.IsNullOrEmpty(_Logo) ? string.Empty : _Logo;
            }
            set
            {
                _Logo = value;
            }
        }
        public string FaceBook
        {
            get
            {
                return string.IsNullOrEmpty(_FaceBook) ? string.Empty : _FaceBook;
            }
            set
            {
                _FaceBook = value;
            }
        }
        public string PhoneNumber
        {
            get
            {
                return string.IsNullOrEmpty(_PhoneNumber) ? string.Empty : _PhoneNumber;
            }
            set
            {
                _PhoneNumber = value;
            }
        }
        public string WebsiteName
        {
            get
            {
                return string.IsNullOrEmpty(_WebsiteName) ? string.Empty : _WebsiteName;
            }
            set
            {
                _WebsiteName = value;
            }
        }
        public string Address
        {
            get
            {
                return string.IsNullOrEmpty(_Address) ? string.Empty : _Address;
            }
            set
            {
                _Address = value;
            }
        }
        public string Email
        {
            get
            {
                return string.IsNullOrEmpty(_Email) ? string.Empty : _Email;
            }
            set
            {
                _Email = value;
            }
        }
    }
}