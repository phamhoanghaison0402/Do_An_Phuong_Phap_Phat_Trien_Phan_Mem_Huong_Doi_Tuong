using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Base;
using Common.Models;
using Data.Interfaces;
using System.Data.Entity;

namespace Data.Implements
{
    public class PhanQuyenRepository : GenericRepository<PhanQuyen>, IPhanQuyenRepository
    {
        public PhanQuyenRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
