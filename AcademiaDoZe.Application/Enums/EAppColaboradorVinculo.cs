using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Application.Enums
{
	public enum EAppColaboradorVinculo
	{
		[Display(Name = "CLT")]
		CLT = 0,

		[Display(Name = "Estagiário")]
		Estagio = 1
	}
}
