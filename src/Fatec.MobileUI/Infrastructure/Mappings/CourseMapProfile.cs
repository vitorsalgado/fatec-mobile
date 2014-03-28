using AutoMapper;
using Fatec.Core.Domain;
using Fatec.MobileUI.ViewModels;

namespace Fatec.MobileUI.Infrastructure.Mappings
{
	public class CourseMapProfile : Profile
	{
		protected override void Configure()
		{
			var domainToModelMap = Mapper.CreateMap<Course, CourseModel>();
			domainToModelMap.ForAllMembers(x => x.Ignore());

			domainToModelMap
				.ForMember(x => x.Workload, o => o.MapFrom(m => m.WorkLoad))
				.ForMember(x => x.DurationInMonths, o => o.MapFrom(m => m.DurationInMonths))
				.ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
				.ForMember(x => x.TeachingLevel, o => o.MapFrom(m => m.EducationLevel))
				.ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
				.ForMember(x => x.TeachingProfile, o => o.MapFrom(m => m.TeachingProfile))
				.ForMember(x => x.Periods, o => o.MapFrom(m => m.Periods));
		}
	}
}