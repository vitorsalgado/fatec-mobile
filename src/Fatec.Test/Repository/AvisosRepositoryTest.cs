using Fatec.Core.Repositories;
using NUnit.Framework;
using System;
using System.Linq;

namespace Fatec.Test.Repository
{
	[TestFixture]
	public class AvisosRepositoryTest
	{
		private IHomeAnnouncementsRepository _avisosHomeRepository;

		[TestFixtureSetUp]
		public void SetUp()
		{
			TestEngine.Start();
			_avisosHomeRepository = TestEngine.Resolve<IHomeAnnouncementsRepository>();
		}

		[Test]
		public void Obter_Todos_Avisos_Home_Deve_Retornar_Apenas_Avisos_Validos()
		{
			var avisosValidos = _avisosHomeRepository.GetAllValid();

			Assert.IsNotEmpty(avisosValidos);
			Assert.IsTrue(avisosValidos.All(x => x.DueDate.CompareTo(DateTime.Now) >= 0));
		}
	}
}
