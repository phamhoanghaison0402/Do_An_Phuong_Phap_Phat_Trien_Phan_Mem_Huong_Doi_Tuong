namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuBaoHanh")]
    public partial class ChiTietPhieuBaoHanh
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoPhieuBaoHanh { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHangHoa { get; set; }

        public int SoLuong { get; set; }

        public decimal Gia { get; set; }

        public decimal ThanhTien { get; set; }

        [StringLength(200)]
        public string NoiDungBaoHanh { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }
    }
}
