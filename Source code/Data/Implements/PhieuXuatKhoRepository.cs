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
    public class PhieuXuatKhoRepository : GenericRepository<PhieuXuatKho>
    {
        public PhieuXuatKhoRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public int CountPhieuXuatKho(PhieuXuatKhoRepository _phieuXuatKhoRepo)
        {
            return _phieuXuatKhoRepo.GetAll().Count();
        }
    }
}
