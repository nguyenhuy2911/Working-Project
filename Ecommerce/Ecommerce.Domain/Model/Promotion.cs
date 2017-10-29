namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
        public DateTime? NgayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        [Required]
        [StringLength(128)]
        public string TenCT { get; set; }

        [Column(TypeName = "text")]
        public string AnhCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
    }
}
