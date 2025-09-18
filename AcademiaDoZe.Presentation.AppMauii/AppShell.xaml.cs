﻿using AcademiaDoZe.Presentation.AppMauii.Views;

namespace AcademiaDoZe.Presentation.AppMauii
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			RegisterRoutes();
		}
		// O Routing.RegisterRoute é necessário para que o Shell do MAUI reconheça e permita a navegação
		// para páginas que não estão diretamente no TabBar ou Flyout,

		// como páginas de detalhe, edição ou cadastro.
		private static void RegisterRoutes()

		{
			Routing.RegisterRoute("logradouro", typeof(LogradouroPage));
		}
	}
}
