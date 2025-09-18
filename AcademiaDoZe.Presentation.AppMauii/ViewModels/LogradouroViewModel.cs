using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Presentation.AppMauii.ViewModels
{
	public partial class LogradouroViewModel : BaseViewModel
	{
		// inicialmente só estamos incluindo o comando de cancelar

		[RelayCommand]
		private async Task CancelAsync()
		{
			await Shell.Current.GoToAsync("..");
		}
	}
}
