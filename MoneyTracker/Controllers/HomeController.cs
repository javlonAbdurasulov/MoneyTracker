using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using MoneyTracker.Application.Interfaces.Service;
using MoneyTracker.Domain.Models;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using MoneyTracker.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace MoneyTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;

        public HomeController(IUserService userService, ITransactionService transactionService)
        {
            _userService = userService;
            _transactionService = transactionService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UpdateMoneyAsync(UpdateTransactionDTO updateTransaction)
        {
            var income = await _transactionService.GetById(updateTransaction.Id);
            UpdateView updateView = new UpdateView()
            {
                Amount = income.Result.Amount,
                Category = income.Result.Category,
                Comment = income.Result.Comment,
                Date = income.Result.Date.ToUniversalTime().AddDays(1),
                Id = income.Result.Id,
                UserId = income.Result.UserId,
                UserName = updateTransaction.UserName
            };
            return View(updateView);
        }
        public async Task<IActionResult> UpdateTransactions(UpdateView updateView)
        {

            Transaction transaction = new()
            {
                Amount = updateView.Amount,
                CategoryId = updateView.Category.Id,
                Comment = updateView.Comment,
                Date = updateView.Date.ToUniversalTime(),
                Id = updateView.Id,
                UserId = updateView.UserId
            };
            var res =await _transactionService.Update(transaction);

            IndexModel indexModel = new()
            {
                DefaultFilter = true,
                UserName = updateView.UserName
            };
            return RedirectToAction("Dashboard",indexModel);
        }
        //public async Task<IActionResult> DeleteTransactions(UpdateTransactionDTO DeleteTransaction)
        //{
        //    if (DeleteTransaction.Category == "Income")
        //    {
        //        await _incomeService.Delete(DeleteTransaction.Id);
        //    }
        //    else
        //    {
        //        await _expenseService.Delete(DeleteTransaction.Id);

        //    }
        //    IndexModel indexModel = new()
        //    {
        //        DefaultFilter = true,
        //        UserName = DeleteTransaction.UserName
        //    };
        //    return RedirectToAction("Dashboard",indexModel);
        //}
        //public IActionResult CreateMoney(PreCreateview preCreateview)
        //{

        //    return View(preCreateview);
        //}
        public async Task<IActionResult> AddTransactions(CreateView Createview)
        {
            MoneyDTO money = new MoneyDTO()
            {
                Amount = Createview.Amount,
                Category = new() { Id= Createview.CategoryId },
                Comment = Createview.Comment,
                Date = Createview.Date,
                UserId = Createview.UserId
            };

            var res = await _transactionService.Create(money);
            
            IndexModel indexModel = new()
            {
                DefaultFilter = true,
                UserName = Createview.UserName
            };
            return RedirectToAction("Dashboard",indexModel);
        }
        //public async Task<IActionResult> Dashboard(IndexModel indexModel)
        //{
        //    var responseUser = await _userService.LoginAsync(indexModel.UserName);
        //    if(responseUser.Result==null) { 
        //        return View(new ResponseModel<DashboardModel>(responseUser.Error));
        //    }
             

        //    if (indexModel.DefaultFilter)
        //    {
        //        MoneyFilterDTO defaultFilterDTO = new MoneyFilterDTO()
        //        {
        //            DateEnd = DateTime.MaxValue.ToUniversalTime(),
        //            DateStart = DateTime.MinValue.ToUniversalTime(),
        //            AmountStart = decimal.MinValue,
        //            AmountEnd = decimal.MaxValue,
        //            Category = new() { Name="All"},
        //            OrderBy = 2,
        //            UserId =responseUser.Result.Id
        //        };

        //        indexModel.MoneyFilter = defaultFilterDTO;
        //    }
        //    else
        //    {
        //        //addDays()
        //        indexModel.MoneyFilter.DateStart = indexModel.MoneyFilter.DateStart.ToUniversalTime();

        //        if (indexModel.MoneyFilter.DateEnd == DateTime.MinValue)
        //        {
        //            indexModel.MoneyFilter.DateEnd = DateTime.MaxValue.ToUniversalTime();
        //        }
        //        else
        //        {
        //            indexModel.MoneyFilter.DateEnd = indexModel.MoneyFilter.DateEnd.ToUniversalTime();
        //        }
        //        if (indexModel.MoneyFilter.AmountEnd == 0)
        //        {
        //            indexModel.MoneyFilter.AmountEnd = decimal.MaxValue;
        //        }

        //    }
        //    ResponseModel<List<Transaction>> TransactionList = new ResponseModel<List<Transaction>>("");

        //    TransactionList = await _transactionService.ApplyFilter(indexModel.MoneyFilter);
            
        //    DashboardModel responseDashModel = new DashboardModel()
        //    {
        //        User = responseUser.Result,
        //        Incomes = IncomeList.Result==null ? new() : IncomeList.Result,
        //        Expenses = ExpenseList.Result == null ? new() : ExpenseList.Result,
        //        BaseTransactions = BaseTransactionList == null ? new() : BaseTransactionList.Result,
        //        Filter = indexModel.MoneyFilter
        //    };

        //    return View(new ResponseModel<DashboardModel>(responseDashModel));
        //}

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
