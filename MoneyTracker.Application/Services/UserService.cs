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

        public async Task<ResponseModel<User>> LoginAsync(UserDTO loginUser)
        {
            User? UsernameIsHave = await _userRepository.GetByUsernameAsync(loginUser.UserName);
            if (UsernameIsHave != null)
            {
                return new(UsernameIsHave);
            }
            User user = new User(loginUser.UserName);
            user = await _userRepository.CreateAsync(user);
            
            return new(user);

        }
    }
}
