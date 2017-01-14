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
    public class NhanVienRepository : GenericRepository<NhanVien>, INhanVienRepository
    {
        public NhanVienRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Count record in Employee table to GenCode function
        /// </summary>
        /// <param name="_employeeRepo">EmployeeRepository</param>
        /// <returns>int</returns>
        public int CountEmployee(NhanVienRepository _nhanVienRepo)
        {
            return _nhanVienRepo.GetAll().Count();
        }
    }
}
