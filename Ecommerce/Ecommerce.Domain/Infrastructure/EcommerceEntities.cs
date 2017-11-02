
namespace Ecommerce.Domain.Infrastructure
{
    using Model;
    using System.Data.Entity;

    public partial class EcommerceModel_DbContext : DbContext
    {
        public EcommerceModel_DbContext(): base("name=SellOnline")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<EcommerceModel_DbContext>());
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<ConfigAPI> ConfigAPIs { get; set; }
        public virtual DbSet<DanhsachdangkisanphamNCC> DanhsachdangkisanphamNCCs { get; set; }
        public virtual DbSet<DonHangKH> DonHangKHs { get; set; }
        public virtual DbSet<Display> Displays { get; set; }
        public virtual DbSet<HangSanXuat> HangSanXuats { get; set; }
        public virtual DbSet<HopDongNCC> HopDongNCCs { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<LoaiSP> LoaiSPs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<Oauth> Oauths { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Sanphamcanmua> Sanphamcanmuas { get; set; }
        public virtual DbSet<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
        public virtual DbSet<ThongSoKyThuat> ThongSoKyThuats { get; set; }
        public virtual DbSet<Trackingaction> Trackingactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.BinhLuans)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.MaKH)
                .WillCascadeOnDelete();

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.DonHangKHs)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.MaKH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.NhaCungCaps)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.Net_user)
                .WillCascadeOnDelete();

            

           

           

            

           
            modelBuilder.Entity<HangSanXuat>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.HangSanXuat)
                .WillCascadeOnDelete(false);

          
          

           

           

            modelBuilder.Entity<LoaiSP>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.LoaiSP1)
                .HasForeignKey(e => e.LoaiSP)
                .WillCascadeOnDelete(false);

           

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.HopDongNCCs)
                .WithOptional(e => e.NhaCungCap)
                .WillCascadeOnDelete();

           

           

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.BinhLuans)
                .WithOptional(e => e.SanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.HopDongNCCs)
                .WithOptional(e => e.SanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.Sanphamcanmuas)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sanphamcanmua>()
                .Property(e => e.MaSP)
                .IsFixedLength();

            modelBuilder.Entity<Sanphamcanmua>()
                .HasMany(e => e.DanhsachdangkisanphamNCCs)
                .WithOptional(e => e.Sanphamcanmua)
                .HasForeignKey(e => e.MaSPCanMua)
                .WillCascadeOnDelete();

          
        }
    }
}
