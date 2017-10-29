namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonHang")]
    public partial class ChiTietDonHang
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string MaDH { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string MaSP { get; set; }

        public int? SoLuong { get; set; }

        public decimal? ThanhTien { get; set; }

        public virtual DonHangKH DonHangKH { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
