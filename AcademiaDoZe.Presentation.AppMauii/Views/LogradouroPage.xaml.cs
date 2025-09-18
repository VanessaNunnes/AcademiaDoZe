using AcademiaDoZe.Presentation.AppMauii.ViewModels;

namespace AcademiaDoZe.Presentation.AppMauii.Views;

public partial class LogradouroPage : ContentPage
{
	public LogradouroPage(LogradouroViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override async void OnAppearing()
	{
		/* implementar depois */
	}
}