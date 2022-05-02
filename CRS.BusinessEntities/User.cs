using System;
using System.ComponentModel.DataAnnotations;


namespace CRS.BusinessEntities
{
    public class User : ICRSBase
    {
        public int ID  { get; set; }

        [Required(ErrorMessage = "Login name is required.")]
        [Display(Name = "User Name")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength=5)]
        //[System.Web.Mvc.Remote("IsUserNameValid", "UserManagement", HttpMethod = "GET", ErrorMessage = "The login name is unavailable")]
        public string UserName{ get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength=5, ErrorMessage="5 to 20 letters")]
        public string Password{ get; set; }

        [Required(ErrorMessage = "Password must be verified.")]
        [Display(Name = "Verify Password")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 5,ErrorMessage="5 to 20 letters")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
   
        [Display(Name = "New Password")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 5)]
        public string NewPassword { get; set; }
        
        [Display(Name = "Verify Password")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 5)]
        [Compare("NewPassword")]
        public string NewPasswordConfirm { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Login Attempts")]
        public int LoginAttempts{ get;set;}
        
        [Display(Name = "Is Locked")]
        public Boolean IsLocked { get; set; }

        [Display(Name = "Role")]
        public int Role { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }

        public string Code { get; set; }

        public string Name{get;set;}

        [Display(Name = "Channel")]
        public string Channel { get; set; }

        [Display(Name = "Channel")]
        public string ChannelName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        [RegularExpression(@"([a-zA-Z]{1,50}\s*)+", ErrorMessage = "Please enter a valid first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        [RegularExpression(@"([a-zA-Z]{1,50}\s*)+", ErrorMessage = "Please enter a valid last name.")]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        [RegularExpression(@"([a-zA-Z]{0,50}\s*)+", ErrorMessage = "Please enter a valid middle name.")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(30)]
        [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Display(Name = "Region")]
        public string RegionCode { get; set; }

        [Display(Name = "Region")]
        public string RegionName { get; set; }

        [Display(Name = "Area")]
        public string AreaCode { get; set; }

        [Display(Name = "Area")]
        public string AreaName { get; set; }

        [Display(Name = "District")]
        public string DistrictCode { get; set; }

        [Display(Name = "District")]
        public string DistrictName { get; set; }

        [Display(Name = "Branch")]
        public string BranchCode { get; set; }

        [Display(Name = "Branch")]
        public string BranchName { get; set; }

        [Display(Name = "Referror Code")]
        //public int ReferrorCode { get; set; }
        public string ReferrorCode { get; set; }

        [Display(Name = "Referror Name")]
        public string ReferrorName { get; set; }
        
        public DateTime DateModified{get;set;}
        
        public DateTime DateCreated{get;set;}
        
        public string ModifiedBy{get;set;}
        
        public string CreatedBy{get;set;}

        [Display(Name = "Redemption Points")]
        public int RedemptionPoints { get; set; }

        public string FullName
        {
           
            get
            {
                string lName = !string.IsNullOrEmpty(this.LastName) ? (this.LastName + ", ") : string.Empty;
                string fName = !string.IsNullOrEmpty(this.FirstName) ? (this.FirstName + " ") : string.Empty;
                string mName = !string.IsNullOrEmpty(this.MiddleName) ? (this.MiddleName) : string.Empty;
                return lName + fName + mName;
              
            }
        }


        public string Actions {
            get
            {
                return string.Empty;
            }
        
        }
                 
        public bool Validate()
        {
            return true;
        }
    }
}
