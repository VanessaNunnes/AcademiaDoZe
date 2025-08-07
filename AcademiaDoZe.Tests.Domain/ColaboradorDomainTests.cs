﻿//Vanessa Furtado Nunes
using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Domain.Exceptions;
using AcademiaDoZe.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Tests.Domain
{
	public class ColaboradorDomainTests
	{
		[Fact]
		public void CriarColaborador_Valido_NaoDeveLancarExcecao()
		{
			var logradouro = Logradouro.Criar("Rua A", "12345678", "Brasil", "SP", "SP", "Centro");

			var fotoFake = new Arquivo(new byte[] { 1, 2, 3, 4 });

			var colaborador = Colaborador.Criar(new DateOnly(2020, 1, 1), EColaboradorTipo.Instrutor, EColaboradorVinculo.CLT,
												"12345678901", "Maria", new DateOnly(1990, 5, 20),
												"maria@email.com", "48988888888", "senha123",
												fotoFake, logradouro, "50", "Sala 2");

			Assert.NotNull(colaborador);
		}

		[Fact]
		public void CriarColaborador_DataAdmissaoFutura_DeveLancarExcecao()
		{
			var logradouro = Logradouro.Criar("Rua A", "12345678", "Brasil", "SP", "SP", "Centro");

			var fotoFake = new Arquivo(new byte[] { 1, 2, 3, 4 });

			Assert.Throws<DomainException>(() =>
				Colaborador.Criar(DateOnly.FromDateTime(DateTime.Now.AddDays(1)), EColaboradorTipo.Instrutor, EColaboradorVinculo.CLT,
								  "12345678901", "Maria", new DateOnly(1990, 5, 20),
								  "maria@email.com", "48988888888", "senha123",
								  fotoFake, logradouro, "50", "Sala 2")
			);
		}
	}
}
