namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

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
        public int Id { get; set; }

        [Required]
        [StringLength(5)]
        [Index(IsUnique = true)]
        public string MaSP { get; set; }

        [Required]
        [DisplayName("Tên SP")]
        [StringLength(50)]
        public string TenSP { get; set; }

        [StringLength(5)]
        public string LoaiSP { get; set; }

        public int? SoLuotXemSP { get; set; }

       
        [StringLength(5)]
        [DisplayName("Hãng")]
        public string HangSX { get; set; }

        [StringLength(50)]
        public string XuatXu { get; set; }

        public decimal? GiaGoc { get; set; }

        [DisplayName("Giá bán")]
        public decimal? GiaTien { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        [DisplayName("Ảnh")]
        [Column(TypeName = "ntext")]
        public string AnhDaiDien { get; set; }

        [Column(TypeName = "ntext")]
        public string AnhNen { get; set; }

        [Column(TypeName = "ntext")]
        public string AnhKhac { get; set; }

        [DisplayName("Số lượng")]
        public int? SoLuong { get; set; }

        public bool? isnew { get; set; }

        public bool? ishot { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }


        public virtual ICollection<BinhLuan> BinhLuans { get; set; }


        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual HangSanXuat HangSanXuat { get; set; }


        public virtual ICollection<HopDongNCC> HopDongNCCs { get; set; }

        public virtual LoaiSP LoaiSP1 { get; set; }


        public virtual ICollection<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }

 
        public virtual ICollection<ThongSoKyThuat> ThongSoKyThuats { get; set; }


        public virtual ICollection<Sanphamcanmua> Sanphamcanmuas { get; set; }
    }
}
