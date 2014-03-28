using Fatec.Core.Repositories;
using NUnit.Framework;
using Fatec.Test.FatecApi;
using System;

namespace Fatec.Test.Repository
{
	[TestFixture]
	public class ApiTest
	{
		private IStudentRepository _alunoRepository;

		[TestFixtureSetUp]
		public void SetUp()
		{
		}

		[Test]
		public void Test()
		{
			FatecApiClient client = new FatecApiClient();
			var response = client.Login(new LoginRequest());

			Assert.IsNotNullOrEmpty(response.Message);
			Console.WriteLine(response.Message);
		}
	}
}
