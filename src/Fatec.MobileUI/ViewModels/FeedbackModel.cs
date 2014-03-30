using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fatec.MobileUI.ViewModels
{
	public class FeedbackModel
	{
		[Required(ErrorMessage = "Por favor, informe uma nota")]
		[DisplayName("Avaliação")]
		public Rating Rate { get; set; }

		[DisplayName("Comentários")]
		[DataType(DataType.Text)]
		public string Comments { get; set; }

		public string GetRatingStringValue(Rating rating)
		{
			switch (rating)
			{
				case Rating.Awful:
					return "Péssimo";
				case Rating.Poor:
					return "Ruim";
				case Rating.Regular:
					return "Regular";
				case Rating.Good:
					return "Bom";
				case Rating.Excellent:
					return "Excelente";
				default:
					return string.Empty;
			}
		}
	}
}
