namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
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
        public string TenHang { get; set; }

        [StringLength(200)]
        public string TruSoChinh { get; set; }

        [StringLength(50)]
        public string QuocGia { get; set; }


        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
