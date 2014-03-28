using AutoMapper;
using Fatec.MobileUI.Infrastructure.Mappings;

namespace Fatec.MobileUI
{
	public static class AutoMapperConfig
	{
		public static void SetUp()
		{
			Mapper.Initialize(x =>
			{
				x.AddProfile<NewsMapProfile>();
				x.AddProfile<EnrollmentsMapProfile>();
				x.AddProfile<RequirementsMapProfile>();
				x.AddProfile<ExamMapProfile>();
				x.AddProfile<AproveitamentosMapProfile>();
				x.AddProfile<CourseMapProfile>();
				x.AddProfile<TeacherAbscenceMapProfile>();
				x.AddProfile<ReplacementsMapProfile>();
				x.AddProfile<KeyMovementMapProfile>();
			});
		}
	}
}