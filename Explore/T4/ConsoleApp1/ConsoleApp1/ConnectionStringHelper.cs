
using System.Configuration;

public static class ConnectionStrings
{
	public static string local
	{
		get { return ConfigurationManager.ConnectionStrings["local"].ConnectionString; }
	}
}

