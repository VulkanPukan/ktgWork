using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Web.Helpers
{
	public interface IMapperService
	{
		TDestination Map<TSource, TDestination>(TSource source);

		TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
	}
}
