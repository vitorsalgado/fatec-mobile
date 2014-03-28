using Fatec.Core.Repositories;
using NUnit.Framework;
using System;
using System.Linq;

namespace Fatec.Test.Repository
{
	[TestFixture]
	public class AvisosRepositoryTest
	{

		[TestFixtureSetUp]
		public void SetUp()
		{
			TestEngine.Start();
		}

		[Test]
		public void Obter_Todos_Avisos_Home_Deve_Retornar_Apenas_Avisos_Validos()
		{

		}
	}
}
