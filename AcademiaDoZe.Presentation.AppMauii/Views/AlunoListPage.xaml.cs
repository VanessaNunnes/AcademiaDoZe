using AcademiaDoZe.Application.DTOs;
using AcademiaDoZe.Presentation.AppMauii.ViewModels;

namespace AcademiaDoZe.Presentation.AppMauii.Views;

public partial class AlunoListPage : ContentPage
{
	public AlunoListPage(AlunoListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is AlunoListViewModel viewModel)
		{
			await viewModel.LoadAlunosCommand.ExecuteAsync(null);
		}
	}
	private async void OnEditButtonClicked(object sender, EventArgs e)
	{
		try
		{
			if (sender is Button button && button.BindingContext is AlunoDTO colaborador && BindingContext is AlunoListViewModel viewModel)
			{
				await viewModel.EditAlunoCommand.ExecuteAsync(colaborador);
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Erro", $"Erro ao editar colaborador: {ex.Message}", "OK");
		}
	}
	private async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		try
		{
			if (sender is Button button && button.BindingContext is AlunoDTO colaborador && BindingContext is AlunoListViewModel viewModel)
			{
				await viewModel.DeleteAlunoCommand.ExecuteAsync(colaborador);
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Erro", $"Erro ao excluir colaborador: {ex.Message}", "OK");
		}
	}
}