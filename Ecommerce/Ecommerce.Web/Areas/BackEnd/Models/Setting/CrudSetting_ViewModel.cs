using Ecommerce.Web.common.Const.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Web.common;
using System.ComponentModel;

namespace Ecommerce.Web.Areas.BackEnd.Models
{
    public class CrudSetting_ViewModel
    {
 
        public List<SelectListItem> ListSettingType
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                var listType = Enum.GetValues(typeof(SettingType))
                                    .Cast<SettingType>()
                                    .ToList();
                foreach (var type in listType)
                {
                    var item = new SelectListItem()
                    {
                        Value = type.ToString(),
                        Text = type.GetEnumDisplayName()
                    };
                    list.Add(item);
                }
                return list;
            }
        }

        [DisplayName("Loại")]
        public string Type { get; set; }

        [AllowHtml]
        [DisplayName("Giá trị")]
        public string Value { get; set; }
        public int Id { get; set; }

        [AllowHtml]
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Hình")]
        public string Image { get; set; }
    }
}