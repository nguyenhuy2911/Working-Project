namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Promotion")]
    public partial class Promotion
    {
        
        public Promotion()
        {
            SanPhamKhuyenMais = new HashSet<SanPhamKhuyenMai>();
        }

        [Key]
        [StringLength(5)]
        public string MaKM { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy/MM/dd}")]
        [DisplayName("Ngày bắt đầu")]
        public DateTime? NgayBatDau { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy/MM/dd}")]
        [DisplayName("Ngày kết thúc")]
        public DateTime? NgayKetThuc { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        [DisplayName("Nội dung")]
        public string NoiDung { get; set; }

        [Required]
        [StringLength(128)]
        [DisplayName("Tên chương trình")]
        public string TenCT { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Hình ảnh")]
        public string AnhCT { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        [DisplayName("Quảng cáo")]
        public string Advertise { get; set; }

        public virtual ICollection<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
    }
}
