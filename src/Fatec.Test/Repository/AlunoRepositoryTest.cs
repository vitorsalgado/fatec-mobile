using Fatec.Core.Repositories;
using NUnit.Framework;

namespace Fatec.Test.Repository
{
	[TestFixture]
	public class AlunoRepositoryTest
	{
		private IStudentRepository _alunoRepository;

		[TestFixtureSetUp]
		public void SetUp()
		{
			TestEngine.Start();
			_alunoRepository = TestEngine.Resolve<IStudentRepository>();
		}

		[Test]
		public void Obter_Curso_Com_Id_16_Deve_Retornar_GE()
		{
			//int cursoId = 16; //GE
			//var curso = _alunoRepository.GetCursoById(cursoId);

			//Assert.IsNotNull(curso);
			//Assert.AreEqual(16, curso.Id);
		}

		[Test]
		public void Obter_Aluno_Por_Matricula_Valida()
		{
			string matricula = "1290371413034";

			var aluno = _alunoRepository.GetByEnrollment(matricula);

			Assert.IsNotNull(aluno);
			Assert.AreEqual(matricula, aluno.Enrollment);
		}

		[Test]
		public void Obter_Aluno_Por_Matricula_Invalida_Deve_Retornar_Nulo()
		{
			string matricula = "ndksajndksja";

			var aluno = _alunoRepository.GetByEnrollment(matricula);

			Assert.IsNull(aluno);
		}

		[Test]
		public void Obter_Disciplinas_De_Um_Aluno_Valido_Deve_Retornar_Ao_Menos_1_Disciplina()
		{
			string matricula = "1290371313006";

			var disciplinasDoAluno = _alunoRepository.GetEnrolledDisciplinesByEnrollment(matricula);

			Assert.IsNotNull(disciplinasDoAluno);
			Assert.IsNotEmpty(disciplinasDoAluno);
			Assert.GreaterOrEqual(disciplinasDoAluno.Count, 1);
		}

		[Test]
		public void Obter_Aproveitamentos_De_Um_Aluno_Valido_Deve_Retornar_Ao_Menos_1_Disciplina()
		{
			string matricula = "";

			var aproveitamentosDoAluno = _alunoRepository.GetStudiesAdvanceByEnrollment(matricula);

			Assert.IsNotNull(aproveitamentosDoAluno);
			Assert.IsNotEmpty(aproveitamentosDoAluno);
			Assert.GreaterOrEqual(aproveitamentosDoAluno.Count, 1);
		}
	}
}
