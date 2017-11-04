namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HangSanXuat")]
    public partial class HangSanXuat
    {
        private bool? _IsHomePage;


        public HangSanXuat()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [StringLength(5)]
        public string HangSX { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Tên hãng")]
        public string TenHang { get; set; }

        [StringLength(200)]
        [DisplayName("Trụ sở chính")]
        public string TruSoChinh { get; set; }

        [StringLength(50)]
        [DisplayName("Quốc gia")]
        public string QuocGia { get; set; }

        [StringLength(50)]
        public string Alias { get; set; }

        [DisplayName("Hiện trên trang chủ")]
        public bool? IsHomePage {
            get
            {
                return _IsHomePage?? false;
            }
            set
            {
                _IsHomePage = value;
            }
        }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
