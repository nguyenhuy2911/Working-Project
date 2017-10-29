namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPhamKhuyenMai")]
    public partial class SanPhamKhuyenMai
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string MaKM { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string MaSP { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        public int? GiamGia { get; set; }

        public virtual Promotion Promotion { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
