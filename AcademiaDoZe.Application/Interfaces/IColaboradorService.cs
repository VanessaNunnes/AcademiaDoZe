﻿using AcademiaDoZe.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Application.Interfaces
{
	public interface IColaboradorService
	{
		Task<ColaboradorDTO> ObterPorIdAsync(int id);
		Task<IEnumerable<ColaboradorDTO>> ObterTodosAsync();
		Task<ColaboradorDTO> AdicionarAsync(ColaboradorDTO colaboradorDto);
		Task<ColaboradorDTO> AtualizarAsync(ColaboradorDTO colaboradorDto);
		Task<bool> RemoverAsync(int id);
		Task<ColaboradorDTO> ObterPorCpfAsync(string cpf);
		Task<bool> CpfJaExisteAsync(string cpf, int? id = null);
		Task<bool> TrocarSenhaAsync(int id, string novaSenha);
	}
}
