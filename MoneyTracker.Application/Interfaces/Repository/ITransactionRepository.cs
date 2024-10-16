using MoneyTracker.Application.Interfaces.Base;
using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Interfaces.Repository
{
    public interface ITransactionRepository: ICreateRepository<Transaction>, IGetAllRepository<Transaction>,
                                      IGetByIdRepository<Transaction>, IDeleteRepository,
                                      IUpdateRepository<Transaction>, IGetQueryable<Transaction>
    {
    }
}
