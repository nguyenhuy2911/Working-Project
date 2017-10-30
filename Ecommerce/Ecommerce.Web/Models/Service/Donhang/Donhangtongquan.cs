using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace  Ecommerce.Web.Models
{
    public class Donhangtongquan
    {
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng cung cấp số điện thoại", AllowEmptyStrings = false)]
        [RegularExpression(@"(09)\d{8}|(01)\d{9}", ErrorMessage = "Số điện thoại không hợp lệ.")]
        [Display(Name = "Điện thoại liên lạc")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string OrderBy { get; set; }

        public string ReceivedPerson { get; set; }

        public string Note { get; set; }
    }
}