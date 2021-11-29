using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ModelEF.EF
{
    public partial class WebsiteDUTDbContext : DbContext
    {
        public WebsiteDUTDbContext()
            : base("name=WebsiteDUTDbContext")
        {
        }

        public virtual DbSet<ChuyenMuc> ChuyenMucs { get; set; }
        public virtual DbSet<DonVi> DonVis { get; set; }
        public virtual DbSet<LienKet> LienKets { get; set; }
        public virtual DbSet<LoaiChuyenMuc> LoaiChuyenMucs { get; set; }
        public virtual DbSet<LoaiDonVi> LoaiDonVis { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<QuangCao> QuangCaos { get; set; }
        public virtual DbSet<QuyenNguoiDung> QuyenNguoiDungs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TagCould> TagCoulds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChuyenMuc>()
                .Property(e => e.MaChuyenMuc)
                .IsUnicode(false);

            modelBuilder.Entity<ChuyenMuc>()
                .Property(e => e.MaLoaiChuyenMuc)
                .IsUnicode(false);

            modelBuilder.Entity<ChuyenMuc>()
                .Property(e => e.MaNguoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<ChuyenMuc>()
                .HasMany(e => e.DonVis)
                .WithMany(e => e.ChuyenMucs)
                .Map(m => m.ToTable("ChuyenMuc_DonVi").MapLeftKey("MaChuyenMuc").MapRightKey("MaDonVi"));

            modelBuilder.Entity<ChuyenMuc>()
                .HasMany(e => e.TagCoulds)
                .WithMany(e => e.ChuyenMucs)
                .Map(m => m.ToTable("ChuyenMuc_TagCould").MapLeftKey("MaChuyenMuc").MapRightKey("MaTagCould"));

            modelBuilder.Entity<DonVi>()
                .Property(e => e.MaDonVi)
                .IsUnicode(false);

            modelBuilder.Entity<DonVi>()
                .Property(e => e.MaLoaiDonVi)
                .IsUnicode(false);

            modelBuilder.Entity<LienKet>()
                .Property(e => e.MaLienKet)
                .IsUnicode(false);

            modelBuilder.Entity<LienKet>()
                .Property(e => e.Duongdan)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiChuyenMuc>()
                .Property(e => e.MaLoaiChuyenMuc)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiChuyenMuc>()
                .Property(e => e.DuongDan)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiChuyenMuc>()
                .HasMany(e => e.ChuyenMucs)
                .WithRequired(e => e.LoaiChuyenMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiDonVi>()
                .Property(e => e.MaloaiDonVi)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiDonVi>()
                .HasMany(e => e.DonVis)
                .WithRequired(e => e.LoaiDonVi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MaNguoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.TenTruycap)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.ChuyenMucs)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.QuyenNguoiDungs)
                .WithMany(e => e.NguoiDungs)
                .Map(m => m.ToTable("NguoiDung_QuyenNguoiDung").MapLeftKey("MaNguoiDung").MapRightKey("MaQuyen"));

            modelBuilder.Entity<QuangCao>()
                .Property(e => e.MaQuangCao)
                .IsUnicode(false);

            modelBuilder.Entity<QuangCao>()
                .Property(e => e.DuongDan)
                .IsUnicode(false);

            modelBuilder.Entity<QuyenNguoiDung>()
                .Property(e => e.MaQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<TagCould>()
                .Property(e => e.MaTagCould)
                .IsUnicode(false);
        }
    }
}
