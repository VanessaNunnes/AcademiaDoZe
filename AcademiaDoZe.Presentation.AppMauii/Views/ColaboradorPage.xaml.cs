using AcademiaDoZe.Presentation.AppMauii.ViewModels;

namespace AcademiaDoZe.Presentation.AppMauii.Views;

public partial class ColaboradorPage : ContentPage
{
	public ColaboradorPage(ColaboradorViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();

		if (BindingContext is ColaboradorViewModel viewModel)

		{
			await viewModel.InitializeAsync();
		}
	}
}