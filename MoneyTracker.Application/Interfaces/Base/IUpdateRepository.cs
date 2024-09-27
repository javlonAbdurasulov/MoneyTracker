using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Interfaces.Base
{
    public interface IUpdateRepository<T> where T : class
    {
        public Task<T> UpdateAsync(T obj);
    }
}
