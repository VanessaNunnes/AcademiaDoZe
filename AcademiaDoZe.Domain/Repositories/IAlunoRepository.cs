﻿//Vanessa Furtado Nunes
using AcademiaDoZe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Domain.Repositories
{
	public interface IAlunoRepository : IRepository<Aluno>
	{
		// Métodos específicos do domínio
		Task<Aluno?> ObterPorCpf(string cpf);

		Task<bool> CpfJaExiste(string cpf, int? id = null);
		Task<bool> TrocarSenha(int id, string novaSenha);
	}
}
