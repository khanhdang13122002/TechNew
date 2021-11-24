using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Tech_News.Models.DAO;
namespace Tech_News.Models
{
    public class IndexViewModel:LeftContentViewModel
    {   
        public async Task<bool> GetData()
        {
            try
            {
                await GetDataL();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel:LeftContentViewModel
    {
        public async Task<bool> GetData()
        {
            try
            {
                await GetDataL();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel:LeftContentViewModel
    {
        public async Task<bool> GetData()
        {
            try
            {
                await GetDataL();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel:LeftContentViewModel
    {
        public async Task<bool> GetData()
        {
            try
            {
                await GetDataL();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel:LeftContentViewModel
    {
        public async Task<bool> GetData()
        {
            try
            {
                await GetDataL();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel:LeftContentViewModel
    {
        public async Task<bool> GetData()
        {
            try
            {
                await GetDataL();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel:LeftContentViewModel
    {
        public async Task<bool> GetData()
        {
            try
            {
                await GetDataL();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel:LeftContentViewModel
    {
        public async Task<bool> GetData()
        {
            try
            {
                await GetDataL();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}