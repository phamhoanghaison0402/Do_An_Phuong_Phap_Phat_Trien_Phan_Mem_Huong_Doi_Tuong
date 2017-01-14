namespace Common.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SMSEntities : DbContext
    {
        public SMSEntities()
            : base("name=SMSEntities")
        {
        }

        public virtual DbSet<BaoCaoTonKho> BaoCaoTonKhoes { get; set; }
        public virtual DbSet<ChiTietPhieuBanHang> ChiTietPhieuBanHangs { get; set; }
        public virtual DbSet<ChiTietPhieuBaoHanh> ChiTietPhieuBaoHanhs { get; set; }
        public virtual DbSet<ChiTietPhieuDatHang> ChiTietPhieuDatHangs { get; set; }
        public virtual DbSet<ChiTietPhieuKiemKho> ChiTietPhieuKiemKhoes { get; set; }
        public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public virtual DbSet<ChiTietPhieuXuatKho> ChiTietPhieuXuatKhoes { get; set; }
        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<HangHoa> HangHoas { get; set; }
        public virtual DbSet<LoaiHangHoa> LoaiHangHoas { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NhanVien_Quyen> NhanVien_Quyen { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<PhieuBanHang> PhieuBanHangs { get; set; }
        public virtual DbSet<PhieuBaoHanh> PhieuBaoHanhs { get; set; }
        public virtual DbSet<PhieuChi> PhieuChis { get; set; }
        public virtual DbSet<PhieuDatHang> PhieuDatHangs { get; set; }
        public virtual DbSet<PhieuKiemKho> PhieuKiemKhoes { get; set; }
        public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public virtual DbSet<PhieuXuatKho> PhieuXuatKhoes { get; set; }
        public virtual DbSet<ThamSo> ThamSoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ChiTietPhieuBanHang>()
                .Property(e => e.Gia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietPhieuBanHang>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietPhieuBaoHanh>()
                .Property(e => e.Gia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietPhieuBaoHanh>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietPhieuDatHang>()
                .Property(e => e.Gia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietPhieuDatHang>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietPhieuNhap>()
                .Property(e => e.GiaNhap)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietPhieuNhap>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietPhieuXuatKho>()
                .Property(e => e.Gia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietPhieuXuatKho>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

          
            modelBuilder.Entity<HangHoa>()
                .Property(e => e.GiaBan)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HangHoa>()
                .Property(e => e.GiamGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.CMND)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien_Quyen>()
                .Property(e => e.MaQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<PhanQuyen>()
                .Property(e => e.MaQuyen)
                .IsUnicode(false);

           
            modelBuilder.Entity<PhieuBanHang>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuBanHang>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

          
            modelBuilder.Entity<PhieuBaoHanh>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuBaoHanh>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

         

            modelBuilder.Entity<PhieuChi>()
                .Property(e => e.TongTienChi)
                .HasPrecision(18, 0);

           
            modelBuilder.Entity<PhieuDatHang>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuDatHang>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

         
            modelBuilder.Entity<PhieuNhap>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

       
            modelBuilder.Entity<PhieuXuatKho>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);
        }
    }
}
