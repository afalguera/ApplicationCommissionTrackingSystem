using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRS.BusinessEntities {

    public class RolePageAccess : ICRSBase
    {

        public string Code
        {
            get;
            set;

        }

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

        public int RoleId
        {
            get;
            set;

        }

        [Display(Name = "Page")]
        public int PageId
        {
            get;
            set;

        }
        public int ID
        {
            get;
            set;

        }

        [Display(Name = "Can View")]
        public bool CanView{get;set;}

        [Display(Name = "Can Edit")]
        public bool CanEdit{get;set;}

        [Display(Name = "Can Delete")]
        public bool CanDelete{get;set;}

        [Display(Name = "Can Print")]
        public bool CanPrint{get;set;}

        [Display(Name = "Can Add")]
        public bool CanAdd { get; set; }
        
        
        [Display(Name = "Page")]
        public string PageTitle { get; set; }
		

        public bool Validate()
        {
            return true;
        }
        
    }
}
