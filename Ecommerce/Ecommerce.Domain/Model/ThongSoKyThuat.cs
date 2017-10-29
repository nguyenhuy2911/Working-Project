namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongSoKyThuat")]
    public partial class ThongSoKyThuat
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string MaSP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ThuocTinh { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(256)]
        public string GiaTri { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
