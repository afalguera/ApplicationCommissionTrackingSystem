using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace CRS.BusinessEntities
{
    public interface ICRSPerson
    {
        string FirstName
        {
            get;
            set;
        }

        string LastName
        {
            get;
            set;
        }

        string MiddleName
        {
            get;
            set;
        }

        DateTime DateOfBirth
        {
            get;
            set;
        }

        char Gender
        {
            get;
            set;
        }

        CivilStatus CivilStatus
        {
            get;
            set;
        }

        string Address
        {
            get;
            set;
        }

        string ZipCode
        {
            get;
            set;
        }

        string ContactNumber
        {
            get;
            set;
        }

       
    }
}
