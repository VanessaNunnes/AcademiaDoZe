using AcademiaDoZe.Application.Enums;
using AcademiaDoZe.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Application.Mappings
{
	public static class ColaboradorEnumMappings
	{
		public static EColaboradorTipo ToDomain(this EAppColaboradorTipo appTipo)
		{
			return (EColaboradorTipo)appTipo;
		}
		public static EAppColaboradorTipo ToApp(this EColaboradorTipo domainTipo)
		{
			return (EAppColaboradorTipo)domainTipo;
		}
		public static EColaboradorVinculo ToDomain(this EAppColaboradorVinculo appVinculo)
		{
			return (EColaboradorVinculo)appVinculo;
		}
		public static EAppColaboradorVinculo ToApp(this EColaboradorVinculo domainVinculo)
		{
			return (EAppColaboradorVinculo)domainVinculo;
		}
	}
}
