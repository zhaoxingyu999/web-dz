using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Database
{
	public class DBConfigure
	{
		public static string ConnStr = GetConnectionString("OfficialWebEntities");

        public static string GetConnectionString(string key, string defaultvalue = "")
		{
			if (ConfigurationManager.ConnectionStrings[key] != null)
			{
				return ConfigurationManager.ConnectionStrings[key].ConnectionString;
			}
			return "";
		}
	}
}
