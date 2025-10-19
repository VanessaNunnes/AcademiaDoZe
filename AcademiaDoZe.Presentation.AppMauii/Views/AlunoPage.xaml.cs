using AcademiaDoZe.Presentation.AppMauii.ViewModels;

namespace AcademiaDoZe.Presentation.AppMauii.Views;

public partial class AlunoPage : ContentPage
{
	public AlunoPage(AlunoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();

		if (BindingContext is AlunoViewModel viewModel)

		{
			await viewModel.InitializeAsync();
		}
	}
}