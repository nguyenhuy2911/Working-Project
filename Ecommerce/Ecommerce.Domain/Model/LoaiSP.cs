namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiSP")]
    public partial class LoaiSP
    {
      
        public LoaiSP()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [StringLength(5)]
        public string MaLoai { get; set; }

        [Required]
        [StringLength(50)]
        public string TenLoai { get; set; }

        public string Alias { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
