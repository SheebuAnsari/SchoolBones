using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Focus8W.BL
{
    public class PrintInvoice
    {
        public int iRegistrationId { get; set; }
        public int iStdId { get; set; }
        public string UserName { get; set; }
        public int iCurrentClass { get; set; }
        public int iDate { get; set; }
        public int iFeeAmount { get; set; }
        public int iPerMonthFee { get; set; }
        public int iAdvanceFee { get; set; }
        public int iForMonth { get; set; }
        public int iMonthlyFeeId { get; set; }
    }

    
}