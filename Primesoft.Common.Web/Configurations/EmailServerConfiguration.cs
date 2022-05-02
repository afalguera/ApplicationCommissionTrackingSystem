using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primesoft.Common.Web.Configurations
{
    public class EmailServerConfiguration
    {
        private String _server;
        private Int32 _port;
        private Boolean _requiresAuthentication;
        private String _userName;
        private String _password;
        private Boolean _useSsl;

        public EmailServerConfiguration(String server, Int32 port, Boolean requiresAuthentication, String userName, String password, Boolean useSsl)
        {
            _server = server;
            _port = port;
            _requiresAuthentication = requiresAuthentication;
            _userName = userName;
            _password = password;
            _useSsl = useSsl;
        }

        public String Server 
        {
            get { return _server; } 
        }

        public Int32 Port        
        {
            get { return _port; }
        }

        public Boolean RequiresAuthentication
        {
            get { return _requiresAuthentication; }
        }

        public String UserName
        {
            get { return _userName; }
        }

        public String Password
        {
            get { return _password; }
        }

        public Boolean UseSsl
        {
            get { return _useSsl; }
        }
    }
}
