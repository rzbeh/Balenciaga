using balenciaga.Core.DTOs;
using balenciaga.Core.Services.Interfaces;
using balenciaga.DataLayer.Context;
using balenciaga.DataLayer.Entities.User;
using balenciaga.DataLayer.Entities.Wallet;
using BF_Core.Generator;
using BF_Core.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace balenciaga.Core.Services
{
    public class UserService : IUserService
    {
        private BalenciagaContext _context;

        public UserService(BalenciagaContext context)
        {
            _context = context;
        }


        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public User LoginUser(loginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixEmail(login.Email);
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == hashPassword);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == username);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public bool ActiveAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ActiveCode = NameGenarator.GenerateUniqCode();
            _context.SaveChanges();

            return true;
        }

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName).UserId;
        }

        public InformationUserViewModel GetUserInformation(string username)
        {
            var user = GetUserByUserName(username);
            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.Wallet = BalanceUserWallet(username);

            return information;

        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
        {
            return _context.Users.Where(u => u.UserName == username).Select(u => new SideBarUserPanelViewModel()
            {
                UserName = u.UserName,
                ImageName = u.UserAvatar,
                RegisterDate = u.RegisterDate
            }).Single();
        }

        public EditProfileViewModel GetDataForEditProfileUser(string username)
        {
            return _context.Users.Where(u => u.UserName == username).Select(u => new EditProfileViewModel()
            {
                AvatarName = u.UserAvatar,
                Email = u.Email,
                UserName = u.UserName

            }).Single();
        }

        public void EditProfile(string username, EditProfileViewModel profile)
        {
            if (profile.UserAvatar != null)
            {
                string imagePath = "";
                if (profile.AvatarName != "Defult.jpg")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                profile.AvatarName = NameGenarator.GenerateUniqCode() + Path.GetExtension(profile.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    profile.UserAvatar.CopyTo(stream);
                }

            }

            var user = GetUserByUserName(username);
            user.UserName = profile.UserName;
            user.Email = profile.Email;
            user.UserAvatar = profile.AvatarName;

            UpdateUser(user);

        }

        public bool CompareOldPassword(string oldPassword, string username)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.Users.Any(u => u.UserName == username && u.Password == hashOldPassword);
        }

        public void ChangeUserPassword(string userName, string newPassword)
        {
            var user = GetUserByUserName(userName);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }

        public int BalanceUserWallet(string userName)
        {

            int userId = GetUserIdByUserName(userName);

            var enter = _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 1 && w.IsPay)
                .Select(w => w.Amount).ToList();

            var exit = _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 2)
                .Select(w => w.Amount).ToList();

            return (enter.Sum() - exit.Sum());


        }

        public List<WalletViewModel> GetWalletUser(string userName)
        {
            int userId = GetUserIdByUserName(userName);

            return _context.Wallets
                .Where(w => w.IsPay && w.UserId == userId)
                .Select(w => new WalletViewModel()
                {
                    Amount = w.Amount,
                    DateTime = w.CreateDate,
                    Description = w.Description,
                    Type = w.TypeId
                })
                .ToList();
        }

        public int ChargeWallet(string userName, int amount, string description, bool isPay = false)
        {
            Wallet wallet = new Wallet()
            {
                Amount = amount,
                CreateDate = DateTime.Now,
                Description = description,
                IsPay = isPay,
                TypeId = 1,
                UserId = GetUserIdByUserName(userName)
            };
           return AddWallet(wallet);
        }

        public int AddWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
            return wallet.WalletId;
        }

        public User GetUserName(string username)
        {
            return null;
        }

        public Wallet GetWalletByWalletId(int WalletId)
        {
            return _context.Wallets.Find(WalletId);
        }

        public void UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            _context.SaveChanges();
        }

        public UsersForAdminViewModel GetUser(int PageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> resault = _context.Users;

            if (!string.IsNullOrEmpty(filterEmail))
            {
                resault = resault.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                resault = resault.Where(u => u.Email.Contains(filterUserName));
            }

            // show item in page

            int take = 20;
            int skip = (PageId - 1) * take;

            UsersForAdminViewModel list = new UsersForAdminViewModel();
            list.CurrentPage = PageId;
            list.PageCount = resault.Count() / take ;
            list.Users = resault.OrderBy( u => u.RegisterDate ).Take(take).ToList();

            return list;
        }

        public int AddUserFromAdmin(CreateUserViewModel user)
        {
            User addUser = new User();
            addUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            addUser.Email = user.Email;
            addUser.ActiveCode = NameGenarator.GenerateUniqCode();
            addUser.IsActive = true;
            addUser.RegisterDate = DateTime.Now;
            addUser.UserName = user.UserName;

            #region Save Avatar 

            if (user.UserAvatar != null)
            {
                string uniqueFileName = NameGenarator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
                addUser.UserAvatar = uniqueFileName;

                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", addUser.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }
            }
            #endregion

            return AddUser(addUser);

        }

        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId)
                 .Select(u => new EditUserViewModel()
                 {
                     UserId = u.UserId,
                     AvatarName = u.UserAvatar,
                     Email = u.Email,
                     UserName = u.UserName,
                     UserRoles = u.UserRoles.Select(r => r.Role_Id).ToList()
                 }).Single();
        }

        public void EditUserFromAdmin(EditUserViewModel edituser)
        {
            User user = GetUserById(edituser.UserId);
            user.Email = edituser.Email;

            if (!string.IsNullOrEmpty(edituser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(edituser.Password);    
            }

            if (edituser.UserAvatar != null)
            {
                if (edituser.AvatarName != "Avatar.jpg")
                {
                    // Delete old Image
                    string Deletepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", edituser.AvatarName);

                    if (File.Exists(Deletepath))
                    {
                        File.Delete(Deletepath);
                    }

                }


                string uniqueFileName = NameGenarator.GenerateUniqCode() + Path.GetExtension(edituser.UserAvatar.FileName);
                user.UserAvatar = uniqueFileName;

                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    edituser.UserAvatar.CopyTo(stream);
                }

            }
            _context.Users.Update(user);
            _context.SaveChanges(); 
           
              
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public UsersForAdminViewModel GetDeleteUser(int PageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> resault = _context.Users.IgnoreQueryFilters();

            if (!string.IsNullOrEmpty(filterEmail))
            {
                resault = resault.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                resault = resault.Where(u => u.Email.Contains(filterUserName));
            }

            // show item in page

            int take = 20;
            int skip = (PageId - 1) * take;

            UsersForAdminViewModel list = new UsersForAdminViewModel();
            list.CurrentPage = PageId;
            list.PageCount = resault.Count() / take;
            list.Users = resault.OrderBy(u => u.RegisterDate).Take(take).ToList();

            return list;
        }
    }
}


