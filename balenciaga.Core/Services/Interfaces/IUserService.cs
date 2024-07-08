using balenciaga.Core.DTOs;
using balenciaga.DataLayer.Entities.User;
using balenciaga.DataLayer.Entities.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace balenciaga.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region Genereal
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        int AddUser(User user);
        void UpdateUser(User user);
        User GetUserName(string username);
        User GetUserById(int userId);
        User LoginUser(loginViewModel login);
        bool ActiveAccount(string ActiveCode);
        int GetUserIdByUserName(string userName);
        #endregion

        #region UserPanel

        InformationUserViewModel GetUserInformation(string UserName);
        SideBarUserPanelViewModel GetSideBarUserPanelData(string UserName);
        EditProfileViewModel GetDataForEditProfileUser(string UserName);
        void EditProfile(string UserName , EditProfileViewModel profile);
        #endregion

        #region wallet
        int BalanceUserWallet(string UserName);
        List<WalletViewModel> GetWalletUser(string UserName);
        int ChargeWallet(string UserName , int Amount,string Description, bool IsPay = false);
        int AddWallet(Wallet wallet);
        Wallet GetWalletByWalletId(int WalletId);
        void UpdateWallet(Wallet wallet);
        #endregion

        #region Admin Panel
        UsersForAdminViewModel GetUser(int PageId = 1, string filterEmail = "", string filterUserName = "");
        UsersForAdminViewModel GetDeleteUser(int PageId = 1, string filterEmail = "", string filterUserName = "");
        int AddUserFromAdmin(CreateUserViewModel user);
        EditUserViewModel GetUserForShowInEditMode(int  userId);
        void EditUserFromAdmin(EditUserViewModel edituser);
        #endregion
    }
}
