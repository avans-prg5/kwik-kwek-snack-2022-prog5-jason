using KwikKwekSnack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnackConsole.Models
{
	public class ConsoleLogic
	{
		public OrderViewModelConsole ConvertModelToViewModel(Order order)
		{
			OrderViewModelConsole orderViewModel = new OrderViewModelConsole();
			orderViewModel.Id = order.Id;
			orderViewModel.SetOrderNumber();
			orderViewModel.Status = order.Status.ToString();
			return orderViewModel;
		}



	}
}
