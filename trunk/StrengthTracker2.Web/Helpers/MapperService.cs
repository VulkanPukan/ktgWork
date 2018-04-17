using AutoMapper;

namespace StrengthTracker2.Web.Helpers
{
	public class MapperService : IMapperService
	{
        private IMapper _mapper { get; set; }
		public MapperService()
		{
			var config = AutoMapperConfiguration.Configure();
			_mapper = config.CreateMapper();
		}
		public TDestination Map<TSource, TDestination>(TSource source)
		{
			return _mapper.Map<TSource, TDestination>(source);
		}

		public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
		{
			return _mapper.Map<TSource, TDestination>(source, destination);
		}
	}
}