using AutoMapper;
using Fatec.Core.Domain;
using Fatec.MobileUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fatec.MobileUI.Infrastructure.Mappings
{
	public class EnrollmentsMapProfile : Profile
	{
		protected override void Configure()
		{
			var domainToModelMap = Mapper.CreateMap<EnrolledDiscipline, EnrollmentModel>();
			domainToModelMap.ForAllMembers(x => x.Ignore());

			domainToModelMap
				.ForMember(x => x.Concept, o => o.MapFrom(m => m.Concept))
				.ForMember(x => x.Discipline, o => o.MapFrom(m => m.Discipline.Name))
				.ForMember(x => x.AbsencesB1, o => o.MapFrom(m => m.AbsencesFirstTwoMonths))
				.ForMember(x => x.AbsencesB2, o => o.MapFrom(m => m.AbsencesSecondTwoMonths))
				.ForMember(x => x.Grade, o => o.MapFrom(m => m.Grade))
				.ForMember(x => x.GradeP1, o => o.MapFrom(m => m.GradeP1))
				.ForMember(x => x.GradeP2, o => o.MapFrom(m => m.GradeP2));
		}
	}
}