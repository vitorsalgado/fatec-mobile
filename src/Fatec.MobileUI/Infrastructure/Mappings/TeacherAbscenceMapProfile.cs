using AutoMapper;
using Fatec.Core.Domain;
using Fatec.MobileUI.ViewModels;

namespace Fatec.MobileUI.Infrastructure.Mappings
{
	public class TeacherAbscenceMapProfile : Profile
	{
		protected override void Configure()
		{
			var domainToModelMap = Mapper.CreateMap<TeacherAbsence, TeacherAbsenceModel>();
			domainToModelMap.ForAllMembers(x => x.Ignore());

			domainToModelMap
				.ForMember(x => x.Date, o => o.MapFrom(m => m.Date))
				.ForMember(x => x.Discipline, o => o.MapFrom(m => m.Discipline.Name))
				.ForMember(x => x.Reason, o => o.MapFrom(m => m.Reason))
				.ForMember(x => x.Details, o => o.MapFrom(m => m.Observations))
				.ForMember(x => x.Teacher, o => o.MapFrom(m => m.TeacherName))
				.ForMember(x => x.Semester, o => o.MapFrom(m => m.Semester))
				.ForMember(x => x.Periods, o => o.MapFrom(m => m.Periods));

		}
	}
}