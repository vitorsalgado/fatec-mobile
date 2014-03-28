using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fatec.MobileUI.ViewModels
{
	public class LoginModel
	{
		[Required(ErrorMessage= "Informe seu usuário")]
		[DisplayName("Usuário")]
		[DataType(DataType.Text)]
		public string Username { get; set; }

		[Required(ErrorMessage="Informe sua senha")]
		[DisplayName("Senha")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DisplayName("Mantenha-me conectado")]
		public bool Remember { get; set; }
	}
}
