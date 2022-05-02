using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace CRS.BusinessEntities
{
    public interface ICRSBase
    {
        string Code
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }

        DateTime DateModified
        {
            get;
            set;
        }

        DateTime DateCreated
        {
            get;
            set;
        }

        string ModifiedBy
        {
            get;
            set;
        }

        string CreatedBy
        {
            get;
            set;
        }

        int ID
        {
            get;
            set;
        }

        bool Validate();
    }
}
