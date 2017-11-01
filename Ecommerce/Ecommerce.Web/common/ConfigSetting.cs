using Ecommerce.Domain.Model;
using Ecommerce.Web.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Ecommerce.Web.common.ConfigSetting), "GetConfig")]
namespace Ecommerce.Web.common
{
    public class ConfigSetting
    {
        public static ConfigModel SystemConfig
        {
            get
            {
                return _systemConfig;
            }
        }
        private static ConfigModel _systemConfig;
        public static ConfigModel GetConfig()
        {
            if (_systemConfig == null)
            {
                var disPlayService = new DisplayService();
                List<Display> listItems = disPlayService.GetDisPlays().ToList();
                var config = new ConfigModel();
                if (listItems != null)
                {
                    config.Logo = listItems.Where(p => p.Type == "Logo")?.FirstOrDefault()?.Value;
                    config.FaceBook = listItems.Where(p => p.Type == "Facebook")?.FirstOrDefault()?.Value;
                    config.PhoneNumber = listItems.Where(p => p.Type == "Phone")?.FirstOrDefault()?.Value;
                    config.WebsiteName = listItems.Where(p => p.Type == "WebsiteName")?.FirstOrDefault()?.Value;
                    config.Address = listItems.Where(p => p.Type == "Address")?.FirstOrDefault()?.Value;
                    config.Email = listItems.Where(p => p.Type == "Email")?.FirstOrDefault()?.Value;
                }
                _systemConfig = config;
            }
            return _systemConfig;
        }
    }
}