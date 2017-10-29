namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhaCungCap")]
    public partial class NhaCungCap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaCungCap()
        {
            ConfigAPIs = new HashSet<ConfigAPI>();
            DanhsachdangkisanphamNCCs = new HashSet<DanhsachdangkisanphamNCC>();
            HopDongNCCs = new HashSet<HopDongNCC>();
            Oauths = new HashSet<Oauth>();
        }

        [Key]
        [StringLength(5)]
        public string MaNCC { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNCC { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(11)]
        public string SDT_NCC { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Column(TypeName = "text")]
        public string Website { get; set; }

        [StringLength(128)]
        public string Net_user { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConfigAPI> ConfigAPIs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhsachdangkisanphamNCC> DanhsachdangkisanphamNCCs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HopDongNCC> HopDongNCCs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oauth> Oauths { get; set; }
    }
}
