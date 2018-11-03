using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheMinx.Tatts.Data.Responses
{
    public class TABQResponse
    {
        public bool Success { get; set; }

        public string AuthenticationToken { get; set; }

        public int TotalTransactions { get; set; }
        public int GoodTransactions { get; set; }
        public int BadTransactions { get; set; }
        
        public decimal BatchCost { get; set; }
        public decimal Balance { get; set; }

        public string Message { get; set; }

        public string Account { get; set; }
        public string Password { get; set; }
        public string SecurityCode { get; set; }

        public TABQResponse()
        {
            this.Success = true;

            this.AuthenticationToken = string.Empty;

            this.TotalTransactions = 0;
            this.GoodTransactions = 0;
            this.BadTransactions = 0;

            this.BatchCost = 0;
            this.Balance = 0;

            this.Message = string.Empty;

            this.Account = string.Empty;
            this.Password = string.Empty;
            this.SecurityCode = string.Empty;
        }
    }
}