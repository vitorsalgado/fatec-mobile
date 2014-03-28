using AutoMapper;
using Fatec.Core.Domain;
using Fatec.MobileUI.ViewModels;

namespace Fatec.MobileUI.Infrastructure.Mappings
{
	public class RequirementsMapProfile : Profile
	{
		protected override void Configure()
		{
			var domainToModelMap = Mapper.CreateMap<Requirement, RequirementModel>();
			domainToModelMap.ForAllMembers(x => x.Ignore());

			domainToModelMap
				.ForMember(x => x.Category, o => o.MapFrom(m => m.Category))
				.ForMember(x => x.Comments, o => o.MapFrom(m => m.Comments))
				.ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
				.ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
				.ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
				.ForMember(x => x.Result, o => o.MapFrom(m => m.Result));
		}
	}
}