using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Base;
using Common.Models;
using Data.Interfaces;
using System.Data.Entity;

namespace Data.Implements
{
    public class ChucVuRepository : GenericRepository<ChucVu>, IChucVuRepository
    {
        public ChucVuRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
