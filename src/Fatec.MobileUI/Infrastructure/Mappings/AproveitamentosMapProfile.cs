using AutoMapper;
using Fatec.Core.Domain;
using Fatec.MobileUI.ViewModels;

namespace Fatec.MobileUI.Infrastructure.Mappings
{
	public class AproveitamentosMapProfile : Profile
	{
		protected override void Configure()
		{
			var domainToModelMap = Mapper.CreateMap<StudiesAdvance, AproveitamentoModel>();
			domainToModelMap.ForAllMembers(x => x.Ignore());

			domainToModelMap
				.ForMember(x => x.Discipline, o => o.MapFrom(m => m.Discipline))
				.ForMember(x => x.TeacherDecision, o => o.MapFrom(m => m.TeacherDecision))
				.ForMember(x => x.Semester, o => o.MapFrom(m => m.Semester))
				.ForMember(x => x.Situation, o => o.MapFrom(m => m.Situation))
				.ForMember(x => x.Period, o => o.MapFrom(m => m.Period));
		}
	}
}