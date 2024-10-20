using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using MoneyTracker.Application.Interfaces.Repository;
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
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IUserService userService, ITransactionService transactionService, ICategoryRepository categoryRepository)
        {
            _userService = userService;
            _transactionService = transactionService;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UpdateMoneyAsync(UpdateTransactionDTO updateTransaction)
        {
            var transaction = await _transactionService.GetById(updateTransaction.Id);
            UpdateView updateView = new UpdateView()
            {
                Amount = transaction.Result.Amount,
                Category = transaction.Result.Category,
                Comment = transaction.Result.Comment,
                Date = transaction.Result.Date.ToUniversalTime(),
                Id = transaction.Result.Id,
                UserId = transaction.Result.UserId,
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
        public async Task<IActionResult> DeleteTransactions(UpdateTransactionDTO DeleteTransaction)
        {

            await _transactionService.Delete(DeleteTransaction.Id);
            
            IndexModel indexModel = new()
            {
                DefaultFilter = true,
                UserName = DeleteTransaction.UserName
            };
            return RedirectToAction("Dashboard", indexModel);
        }
        public IActionResult CreateMoney(PreCreateview preCreateview)
        {

            return View(preCreateview);
        }
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
        public async Task<IActionResult> Dashboard(IndexModel indexModel)
        {
            var responseUser = await _userService.LoginAsync(indexModel.UserName);
            if (responseUser.Result == null)
            {
                return View(new ResponseModel<DashboardModel>(responseUser.Error));
            }
            

            if (indexModel.DefaultFilter)
            {
                MoneyFilterDTO defaultFilterDTO = new MoneyFilterDTO()
                {
                    DateEnd = DateTime.MaxValue.ToUniversalTime(),
                    DateStart = DateTime.MinValue.ToUniversalTime(),
                    AmountStart = 0,
                    AmountEnd = decimal.MaxValue,
                    Category = new() { Name = "All", IsIncome = true },
                    OrderBy = 2,
                    UserId = responseUser.Result.Id
                };

                indexModel.MoneyFilter = defaultFilterDTO;
            }
            else
            {
                //addDays()
                indexModel.MoneyFilter.DateStart = indexModel.MoneyFilter.DateStart.ToUniversalTime().AddDays(1);

                if (indexModel.MoneyFilter.DateEnd == DateTime.MinValue)
                {
                    indexModel.MoneyFilter.DateEnd = DateTime.MaxValue.ToUniversalTime();
                }
                else
                {
                    indexModel.MoneyFilter.DateEnd = indexModel.MoneyFilter.DateEnd.ToUniversalTime().AddDays(1);
                }
                if (indexModel.MoneyFilter.AmountEnd == 0)
                {
                    indexModel.MoneyFilter.AmountEnd = decimal.MaxValue;
                }
                if (!indexModel.categoryIsVisibility)
                {
                    indexModel.MoneyFilter.Category = new() { Name = "All", IsIncome = true };
                }
                if (indexModel.MoneyFilter.Category.Id != 0)
                {
                    Category? id = await _categoryRepository.GetById(indexModel.MoneyFilter.Category.Id);
                    indexModel.MoneyFilter.Category.Name = id.Name;
                }else
                {
                    //indexModel.MoneyFilter.Category.Name = "All";
                }
            }

            ResponseModel<List<Transaction>> TransactionList = new ResponseModel<List<Transaction>>("");

            TransactionList = await _transactionService.ApplyFilter(indexModel.MoneyFilter);

            DashboardModel responseDashModel = new DashboardModel()
            {
                User = responseUser.Result,
                Transactions = TransactionList.Result == null ? new() : TransactionList.Result,
                Filter = indexModel.MoneyFilter,
                categoryIsVisible = indexModel.categoryIsVisibility
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
