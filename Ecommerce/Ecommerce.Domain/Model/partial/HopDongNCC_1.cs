using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Model
{
    [MetadataTypeAttribute(typeof(HopDongNCC.Metadata))]
    public partial class HopDongNCC
    {
        public string TenNCC { get; set; }
        internal sealed class Metadata
        {
            [Range(1, 999999999, ErrorMessage = "Mời bạn nhập trong khoảng 1 -> 999999999")]
            public Nullable<int> ThoiHanHD { get; set; }
            [Range(1, 999999, ErrorMessage = "Mời bạn nhập trong khoảng 1 -> 999999")]
            public Nullable<int> SLToiThieu { get; set; }
            [Range(1, 999999, ErrorMessage = "Mời bạn nhập trong khoảng 1 -> 999999")]
            public Nullable<int> SLCungCap { get; set; }
        }
    }
}
