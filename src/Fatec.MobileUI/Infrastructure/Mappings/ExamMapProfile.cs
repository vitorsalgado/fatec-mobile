using AutoMapper;
using Fatec.Core.Domain;
using Fatec.MobileUI.ViewModels;

namespace Fatec.MobileUI.Infrastructure.Mappings
{
	public class ExamMapProfile : Profile
	{
		protected override void Configure()
		{
			var domainToModelMap = Mapper.CreateMap<Exam, ExamModel>();
			domainToModelMap.ForAllMembers(x => x.Ignore());

			domainToModelMap
				.ForMember(x => x.DisciplineName, o => o.MapFrom(m => m.Discipline.Name))
				.ForMember(x => x.FirstExamDate, o => o.MapFrom(m => m.FirstExamDate))
				.ForMember(x => x.Period, o => o.MapFrom(m => m.Period))
				.ForMember(x => x.SecondExamDate, o => o.MapFrom(m => m.SecondExamDate))
				.ForMember(x => x.TeacherName, o => o.MapFrom(m => m.Professor));
		}
	}
}