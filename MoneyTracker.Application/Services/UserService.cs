using MoneyTracker.Application.Interfaces.Repository;
using MoneyTracker.Application.Interfaces.Service;
using MoneyTracker.Domain.Models;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseModel<User>> LoginAsync(string UserName)
        {
            User? UsernameIsHave = await _userRepository.GetByUsernameAsync(UserName);
            if (UsernameIsHave != null)
            {
                return new(UsernameIsHave);
            }
            User user = new User(UserName);
            user = await _userRepository.CreateAsync(user);
            
            return new(user);
        }

        public async Task<ResponseModel<User>> UpdateBalanceAsync(int userId,decimal amountMinus,decimal amountPlus)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                return new("Пользователь с таким Id не существует!");
            }
            user.Balance -= amountMinus;
            user.Balance += amountPlus;
            var updatedUser = await _userRepository.UpdateAsync(user);
            return new(updatedUser);
        }
    }
}
