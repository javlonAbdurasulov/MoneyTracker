using MoneyTracker.Domain.Models;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Interfaces.Service
{
    public interface IUserService
    {
        public Task<ResponseModel<User>> LoginAsync(UserDTO loginUser);
        public Task<ResponseModel<User>> UpdateBalanceAsync(int userId, decimal amountMinus, decimal amountPlus);
    }
}
