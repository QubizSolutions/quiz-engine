using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qubiz.QuizEngine.Areas.M.Controllers
{
    public class AdminCheckController : Controller
    {
		private static string Name = "";

        public static string getUserName( string httpUser )
        {
			if ((httpUser == System.String.Empty) || !Authorizer.IsAdmin(httpUser))
			{
				Name = "Guest";
			}
			else
			{
				Name = httpUser.Substring(6);
			}
			return Name;
		}
    }
}