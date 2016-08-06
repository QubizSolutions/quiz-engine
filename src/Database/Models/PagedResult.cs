using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qubiz.QuizEngine.Database.Models
{
	public class PagedResult <T>
	{
		public int TotalCount { get; set; }
		public T[] Items { get; set; }
	}
}