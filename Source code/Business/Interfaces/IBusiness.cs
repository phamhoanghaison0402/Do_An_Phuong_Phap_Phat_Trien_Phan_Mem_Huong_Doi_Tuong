using Common.Models;
using Data.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IBusiness 
    {
        Task<Object> Find(String ID);

        Task Create(Object model);

        Task Update(Object inputModel, Object editModel);
    }
}
