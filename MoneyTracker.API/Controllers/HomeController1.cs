using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Interfaces.Service;
using MoneyTracker.Domain.Models;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using System.Diagnostics.Metrics;

namespace MoneyTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController1 : Controller
    {
        public IUserService _userService;

        public HomeController1(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<ResponseModel<User>> LoginUser(UserDTO userDTO)
        {
            var letterResponseModel = await _userService.LoginAsync(userDTO);
            return letterResponseModel;
        }
        //[HttpDelete]
        //public async Task<bool> DeleteLetter(int Id)
        //{
        //    var DeleteLetterResponseModel = await _letterService.Delete(Id);
        //    return DeleteLetterResponseModel;
        //}
        //[HttpPut]
        //public async Task<bool> UpdateLetter(Letter letter)
        //{
        //    //kak ona rabotayet
        //    // polzovatel viberayet zayavku i najimayet UPDATE
        //    // togda v servere s frontend prosit snachalo GetById i etotje Letter kotoriy vernulsya zapolnyayet ix formu avtomatom
        //    // nujniye mesta izmenyayet i vozvrashayet nam i tut uje srabativayet UPDATE

        //    var UpdateLetterResponseModel = await _letterService.Update(letter);
        //    return UpdateLetterResponseModel;
        //}
        //[HttpPost]
        //public async Task<ResponseModel<Letter>> GetByIdLetter(int Id)
        //{
        //    var letterResponseModel = await _letterService.GetById(Id);
        //    return letterResponseModel;
        //}

    }
}
