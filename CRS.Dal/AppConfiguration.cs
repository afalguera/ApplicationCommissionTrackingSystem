using System.Configuration; 

namespace CRS.Dal
{
    
    public static class AppConfiguration
	{
		#region Public Properties

		/// <summary>Returns the connectionstring for the application.</summary>
		public static string ConnectionString
		{
			get
			{
				return ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
			}
		}
		/// <summary>Returns the name of the current connectionstring for the application.</summary>
		public static string ConnectionStringName
		{
			get
			{
				return ConfigurationManager.AppSettings["ConnectionStringName"];
			}
		}
		#endregion
	}
}