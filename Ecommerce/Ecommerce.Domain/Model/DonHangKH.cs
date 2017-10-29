namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHangKH")]
    public partial class DonHangKH
    {
      
        public DonHangKH()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [Key]
        [StringLength(5)]
        public string MaDH { get; set; }

        [Required]
        [StringLength(128)]
        public string MaKH { get; set; }

        public decimal? PhiVanChuyen { get; set; }

        [StringLength(200)]
        public string PTGiaoDich { get; set; }

        public DateTime? NgayDatMua { get; set; }

        public int? TinhTrangDH { get; set; }

        public double? Tongtien { get; set; }

        [Column(TypeName = "text")]
        public string Ghichu { get; set; }

        [StringLength(100)]
        public string Diachi { get; set; }

        [StringLength(15)]
        public string Dienthoai { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
