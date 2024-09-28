using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Interfaces.Service;
using MoneyTracker.Domain.Models;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Models;
using System.Diagnostics;

namespace MoneyTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IIncomeService _incomeService;

        public HomeController(IUserService userService, IIncomeService incomeService)
        {
            _userService = userService;
            _incomeService = incomeService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Dashboard(UserDTO userDTO)
        {
            var responseUser = await _userService.LoginAsync(userDTO);
            
            if(responseUser.Result==null) { 
                //return View("/Views/Home/Index.cshtml");
                return View(new ResponseModel<DashboardModel>(responseUser.Error));
            }
            //var responseIncomes = await _incomeService;
            DashboardModel responseDashModel = new DashboardModel()
            {
                User = responseUser.Result,

            };

            return View(new ResponseModel<DashboardModel>(responseDashModel));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
