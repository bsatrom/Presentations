using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using StackOverflow;

namespace RazorHelpers.StackOverflow
{
	public static class Flair
	{
		public static IHtmlString Create(string username, FlairTheme theme)
		{
			string userId = GetUserId(username);
			string flairFrame = string.Format("<iframe src=\"http://stackoverflow.com/users/flair/{0}.html?theme={1}\" " + 
				"marginwidth=\"0\" marginheight=\"0\" frameborder=\"0\" scrolling=\"no\" width=\"210\" height=\"60\"></iframe>", 
				userId, theme.ToString());
		
			return new HtmlString(flairFrame);
		}

		public static string GetUserId(string username)
		{
			var apiEndpoint = string.Format("http://stack2xml.quickmediasolutions.com/stackoverflow/users?filter={0}", username);
			var webRequest = WebRequest.Create(apiEndpoint) as HttpWebRequest;
			var webResponse = webRequest.GetResponse() as HttpWebResponse;

			if (webResponse != null)
			{
				string userDataStream;
				using (var userStream = new StreamReader(webResponse.GetResponseStream()))
				{
					userDataStream = userStream.ReadToEnd();
				}

				return GetUserIdFromXmlResult(userDataStream);
			}

			return "22656"; // If no user is found, return Jon Skeet
		}

		private static string GetUserIdFromXmlResult(string userString)
		{
			XElement userXml = XElement.Parse(userString);
			var users = userXml.Descendants("users").ToList();

			if (users[0] != null)
				return users[0].Element("user").Element("user_id").Value;

			return "22656"; // If no user is found, return Jon Skeet
		}
	}
}
