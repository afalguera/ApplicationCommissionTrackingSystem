using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRS.BusinessEntities
{
    public class Role : ICRSBase
    {

        public string Code
        {
            get;
            set;
            
        }
        [Required(ErrorMessage = "Role name is required.")]
        [Display(Name = "Role Name")]
        [DataType(DataType.Text)]
        public string Name
        {
            get;
            set;

        }

        public DateTime DateModified
        {
            get;
            set;

        }

        public DateTime DateCreated
        {
            get;
            set;
        }

        public string ModifiedBy
        {
            get;
            set;
            
        }

        public string CreatedBy
        {
            get;
            set;
            
        }

        public int ID
        {
            get;
            set;
            
        }

        [Required]
        [Display(Name = "Channel")]
        public Channel Channel
        {
            get;
            set;
            
        }


        public bool Validate()
        {
            return true;
        }
    }
}
