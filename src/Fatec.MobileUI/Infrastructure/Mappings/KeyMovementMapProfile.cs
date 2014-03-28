using AutoMapper;
using Fatec.Core.Domain;
using Fatec.MobileUI.ViewModels;

namespace Fatec.MobileUI.Infrastructure.Mappings
{
	public class KeyMovementMapProfile : Profile
	{
		protected override void Configure()
		{
			var domainToModelMap = Mapper.CreateMap<KeyMovement, KeyMovementModel>();
			domainToModelMap.ForAllMembers(x => x.Ignore());

			domainToModelMap
				.ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
				.ForMember(x => x.Key, o => o.MapFrom(m => m.Key))
				.ForMember(x => x.Requester, o => o.MapFrom(m => m.Requester));
		}
	}
}