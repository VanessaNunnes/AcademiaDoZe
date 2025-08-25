﻿using AcademiaDoZe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Domain.Repositories
{
	public interface IColaboradorRepository : IRepository<Colaborador>
	{
		// Métodos específicos do domínio

		Task<Colaborador?> ObterPorCpf(string cpf);

		Task<bool> CpfJaExiste(string cpf, int? id = null);
		Task<bool> TrocarSenha(int id, string novaSenha);
	}
}
