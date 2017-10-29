namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhsachdangkisanphamNCC")]
    public partial class DanhsachdangkisanphamNCC
    {
        [Key]
        public int ID { get; set; }

        public int? MaSPCanMua { get; set; }

        [StringLength(5)]
        public string MaNCC { get; set; }

        [Column(TypeName = "text")]
        public string Ghichu { get; set; }

        public DateTime? NgayDK { get; set; }

        public int? Trangthai { get; set; }

        public int? TienmoiSP { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }

        public virtual Sanphamcanmua Sanphamcanmua { get; set; }
    }
}
