using balenciaga.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace balenciaga.DataLayer.Entities.Wallet
{
    public class Wallet
    {

        public Wallet()
        {
                
        }

        [Key]
        public int WalletId { get; set; }

        [Display(Name = "transaction Type")]
        [Required(ErrorMessage = "please enter ")]
        public int TypeId { get; set; }

        [Display(Name = " User ")]
        [Required(ErrorMessage = "please enter ")]
        public int UserId { get; set; }

        [Display(Name = " Price ")]
        [Required(ErrorMessage = "please enter ")]
        public int Amount { get; set; }

        [Display(Name = " Verify ")]
        public bool IsPay { get; set; }

        [Display(Name = "Description ")]
        public string Description { get; set; }

        [Display(Name = " Date ")]
        public DateTime CreateDate { get; set; }

        [ForeignKey("UserId")]

        public virtual User.User User { get; set; }


        [ForeignKey("TypeId")]
        public virtual WalletType WalletType { get; set; }

    }
}
