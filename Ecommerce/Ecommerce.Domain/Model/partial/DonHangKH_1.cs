using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Model
{
    [MetadataTypeAttribute(typeof(DonHangKH.Metadata))]
    public partial class DonHangKH
    {
        internal sealed class Metadata
        {
            [Required(ErrorMessage = "Vui lòng cung cấp số điện thoại", AllowEmptyStrings = false)]
            [RegularExpression(@"(09)\d{8}|(01)\d{9}", ErrorMessage = "Số điện thoại không đúng")]
            [Display(Name = "Điện thoại liên lạc")]
            [DataType(DataType.PhoneNumber)]
            public string Dienthoai { get; set; }
        }
    }
}
