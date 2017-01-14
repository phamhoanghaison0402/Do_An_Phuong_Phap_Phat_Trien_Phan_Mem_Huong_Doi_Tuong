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
    public class PhieuKiemKhoRepository : GenericRepository<PhieuKiemKho>
    {
        public PhieuKiemKhoRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public int CountPhieuKiemKho(PhieuKiemKhoRepository _phieuKiemKhoRepo)
        {
            return _phieuKiemKhoRepo.GetAll().Count();
        }
    }
}
