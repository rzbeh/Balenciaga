using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace balenciaga.Core.DTOs
{
    public class ChargeWalletViewModel
    {
        [Display(Name = "amount")]
        [Required(ErrorMessage = "please enter {0}")]
        public int Amount { get; set; }
    }

    public class WalletViewModel
    {
        public int Amount { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
