using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.common.Const.Enum
{
    public enum SettingType
    {
        [Display(Name = "Logo")]
        Logo,

        [Display(Name = "Facebook")]
        Facebook,

        [Display(Name = "Số điện thoại")]
        Phone,

        [Display(Name = "Địa chỉ")]
        Address,

        [Display(Name = "Email")]
        Email,

        [Display(Name = "Giới thiệu")]
        About,

        [Display(Name = "Liên hệ")]
        Contact
    }
}