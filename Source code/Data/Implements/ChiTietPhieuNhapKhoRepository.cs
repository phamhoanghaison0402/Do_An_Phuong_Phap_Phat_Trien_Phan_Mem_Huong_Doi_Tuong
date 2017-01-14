using Common.Models;
using Data.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implements
{
    public class ChiTietPhieuNhapKhoRepository : GenericRepository<ChiTietPhieuNhap>
    {
        public ChiTietPhieuNhapKhoRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
