namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AspNetUser
    {
       
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            BinhLuans = new HashSet<BinhLuan>();
            DonHangKHs = new HashSet<DonHangKH>();
            NhaCungCaps = new HashSet<NhaCungCap>();
            AspNetRoles = new HashSet<AspNetRole>();
        }
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(5)]
        public string MaNV { get; set; }

        [StringLength(9)]
        public string CMND { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        public bool? GioiTinh { get; set; }

        [StringLength(256)]
        public string DiaChi { get; set; }

        [Column(TypeName = "text")]
        public string Avatar { get; set; }

      
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }

        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }


        public virtual ICollection<BinhLuan> BinhLuans { get; set; }


        public virtual ICollection<DonHangKH> DonHangKHs { get; set; }


        public virtual ICollection<NhaCungCap> NhaCungCaps { get; set; }

 
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
