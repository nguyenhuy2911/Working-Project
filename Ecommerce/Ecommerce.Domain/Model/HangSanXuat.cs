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
       
        public HangSanXuat()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [StringLength(5)]
        public string HangSX { get; set; }

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


        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
