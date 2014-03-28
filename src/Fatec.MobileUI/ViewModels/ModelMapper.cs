using Fatec.Core;
using Fatec.Core.Domain;
using System;

namespace Fatec.MobileUI.ViewModels
{
	public static class ModelMapper
	{
		public static NewsModel ToModel(this News entity)
		{
			return new NewsModel()
			{
				Id = entity.Id,
				Titulo = entity.Title,
				Corpo = entity.Body,
				CriadoEm = (entity.CreatedOn ?? DateTime.MinValue).ToString("dd/MM/yyyy HH:mm"),
				CriadoPor = entity.CreatedBy
			};
		}

		public static EnrollmentModel ToModel(this EnrolledDiscipline entity)
		{
			return new EnrollmentModel()
			{
				Disciplina = CommonHelper.ShrinkText(entity.Discipline.Name, 25, 23),
				Conceito = entity.Concept,
				FaltasPrimeiroBimestre = entity.AbsencesFirstTwoMonths,
				FaltasSegundoBimestre = entity.AbsencesSecondTwoMonths,
				Media = entity.Grade,
				NotaPrimeiroBimestre = entity.GradeP1,
				NotaSegundoBimestre = entity.GradeP2
			};
		}

		public static CourseModel ToModel(this Course entity)
		{
			return new CourseModel()
			{
				CargaHorario = entity.WorkLoad,
				DuracaoEmMeses = entity.DurationInMonths,
				Id = entity.Id,
				NivelDeEnsino = entity.EducationLevel,
				Nome = entity.Name,
				PerfilDeEnsino = entity.TeachingProfile,
				Turnos = entity.Periods
			};
		}

		public static TeacherAbsenceModel ToModel(this TeacherAbsence entity)
		{
			return new TeacherAbsenceModel()
			{
				Data = entity.Date,
				Disciplina = entity.Discipline.Name,
				Motivo = entity.Reason,
				Observacoes = entity.Observations,
				Professor = entity.TeacherName ?? "<SEM NOME>",
				Semestre = entity.Semester,
				Turnos = entity.Periods
			};
		}

		public static ClassReplacementModel ToModel(this ClassReplacement entity)
		{
			ClassReplacementModel model = new ClassReplacementModel();
			model.Data = entity.Date;
			model.Disciplina = entity.Discipline.Name;
			model.Professor = entity.TeacherName;
			model.Turnos = entity.Periods;
			return model;
		}
	}
}