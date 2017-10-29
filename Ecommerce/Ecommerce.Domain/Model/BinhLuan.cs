namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        [Key]
        public int MaBL { get; set; }

        [StringLength(5)]
        public string MaSP { get; set; }

        [StringLength(128)]
        public string MaKH { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        public DateTime? NgayDang { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(128)]
        public string Email { get; set; }

        [StringLength(1)]
        public string DaTraLoi { get; set; }

        public int? Parent { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
