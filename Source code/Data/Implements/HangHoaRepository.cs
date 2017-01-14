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
    public class HangHoaRepository : GenericRepository<HangHoa>
    {
        public HangHoaRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
