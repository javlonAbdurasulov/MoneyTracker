﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Domain.Models.Entity
{
    public class User
    {
        public User()
        {
            
        }
        public User(string username)
        {
            UserName = username;
            Balance = 0;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public decimal Balance{ get; set; }
        public List<Income> Incomes { get; set; }=new List<Income>();
        public List<Expense> Expenses { get; set; } = new List<Expense>();

    }
}
