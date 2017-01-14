namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [Key]
        public int MaNhanVien { get; set; }

        [StringLength(50)]
        public string TenNhanvien { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        [StringLength(15)]
        public string SoDienThoai { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(10)]
        public string CMND { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string PassWord { get; set; }

        public bool TrangThai { get; set; }

        public int MaChucVu { get; set; }

        public string Avatar { get; set; }
    }
}
