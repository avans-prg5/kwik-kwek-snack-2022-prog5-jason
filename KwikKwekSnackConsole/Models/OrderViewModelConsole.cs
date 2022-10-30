using KwikKwekSnack.Domain.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KwikKwekSnackConsole.Models
{
	public class OrderViewModelConsole
	{
		private string? orderNumber;
		public int Id { get; set; }
		public string? Status { get; set; }

		public void SetOrderNumber()
		{
			string id = Id.ToString();
			orderNumber = Regex.Match(id, @"(\d{1,3})$").ToString();
		}

		public string GetOrderNumber()
		{
			if(orderNumber == null)
			{
				return Id.ToString();
			}
			return orderNumber;
		}

	}
}
