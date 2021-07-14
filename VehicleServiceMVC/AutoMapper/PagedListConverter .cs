using AutoMapper;
using PagedList;
using System.Collections.Generic;

namespace VehicleServiceMVC.AutoMapper
{
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>>
    {
        public IPagedList<TDestination> Convert(IPagedList<TSource> source, IPagedList<TDestination> destination, ResolutionContext context)
        {
            //var models = (StaticPagedList<TSource>)source;
            //var viewModels = models.Select(context.Mapper.Map<TSource, TDestination>);

            var collection = context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
            return new StaticPagedList<TDestination>(collection, source.PageNumber, source.PageSize, source.TotalItemCount);
        }
    }
}




