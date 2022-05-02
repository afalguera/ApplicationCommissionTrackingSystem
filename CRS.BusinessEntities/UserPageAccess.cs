using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace CRS.BusinessEntities
{
    public class UserPageAccess
    {
        public int PageId
        {
            get;
            set;
        }

        public int ModuleId
        {
            get;
            set;
        }

        public string PageName
        {
            get;
            set;

        }

        public string PageTitle
        {
            get;
            set;

        }

        public Boolean CanView
        {
            get;
            set;

        }

        public Boolean CanAdd
        {
            get;
            set;

        }

        public Boolean CanDelete
        {
            get;
            set;
            
        }

        public Boolean CanEdit
        {
            get;
            set;

        }

        public Boolean CanPrint
        {
            get;
            set;

        }

        public string PageType
        {
            get;
            set;

        }
    }
}
