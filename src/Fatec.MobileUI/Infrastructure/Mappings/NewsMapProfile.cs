using AutoMapper;
using Fatec.Core.Domain;
using Fatec.MobileUI.ViewModels;

namespace Fatec.MobileUI.Infrastructure.Mappings
{
	public class NewsMapProfile : Profile
	{
		protected override void Configure()
		{
			var domainToModelMap = Mapper.CreateMap<News, NewsModel>();
			domainToModelMap.ForAllMembers(x => x.Ignore());

			domainToModelMap
				.ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
				.ForMember(x => x.Body, o => o.MapFrom(m => m.Body))
				.ForMember(x => x.CreatedOn, o => o.MapFrom(m => m.CreatedOn))
				.ForMember(x => x.CreatedBy, o => o.MapFrom(m => m.CreatedBy))
				.ForMember(x => x.Subject, o => o.MapFrom(m => m.Subject))
				.ForMember(x => x.Title, o => o.MapFrom(m => m.Title));
		}
	}
}