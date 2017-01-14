using Common.Models;
using Data.Base;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implements
{
    public class NhanVien_QuyenRepository : GenericRepository<NhanVien_Quyen>, INhanVien_QuyenRepository
    {
        public NhanVien_QuyenRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
