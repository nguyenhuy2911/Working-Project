namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        
        public SanPham()
        {
            BinhLuans = new HashSet<BinhLuan>();
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            HopDongNCCs = new HashSet<HopDongNCC>();
            SanPhamKhuyenMais = new HashSet<SanPhamKhuyenMai>();
            ThongSoKyThuats = new HashSet<ThongSoKyThuat>();
            Sanphamcanmuas = new HashSet<Sanphamcanmua>();
        }

        [Key]
        [StringLength(5)]
        public string MaSP { get; set; }

        [Required]
        [StringLength(50)]
        public string TenSP { get; set; }

        [Required]
        [StringLength(5)]
        public string LoaiSP { get; set; }

        public int? SoLuotXemSP { get; set; }

        [Required]
        [StringLength(5)]
        public string HangSX { get; set; }

        [StringLength(50)]
        public string XuatXu { get; set; }

        public decimal? GiaGoc { get; set; }

        public decimal? GiaTien { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        [Column(TypeName = "ntext")]
        public string AnhDaiDien { get; set; }

        [Column(TypeName = "ntext")]
        public string AnhNen { get; set; }

        [Column(TypeName = "ntext")]
        public string AnhKhac { get; set; }

        public int? SoLuong { get; set; }

        public bool? isnew { get; set; }

        public bool? ishot { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual HangSanXuat HangSanXuat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HopDongNCC> HopDongNCCs { get; set; }

        public virtual LoaiSP LoaiSP1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongSoKyThuat> ThongSoKyThuats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sanphamcanmua> Sanphamcanmuas { get; set; }
    }
}
