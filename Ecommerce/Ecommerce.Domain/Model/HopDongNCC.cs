namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HopDongNCC")]
    public partial class HopDongNCC
    {
        [Key]
        [StringLength(5)]
        public string MaHD { get; set; }

        [StringLength(5)]
        public string MaNCC { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKy { get; set; }

        public int? ThoiHanHD { get; set; }

        public DateTime? TGGiaoHang { get; set; }

        [StringLength(5)]
        public string MaSP { get; set; }

        public int? SLToiThieu { get; set; }

        public int? SLCungCap { get; set; }

        public DateTime? Dateaccept { get; set; }

        public bool? IsBuy { get; set; }

        public int? SoNgayGiao { get; set; }

        public decimal? DonGia { get; set; }

        public bool? TinhTrang { get; set; }

        public bool? TTThanhToan { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }
    }

    
}
