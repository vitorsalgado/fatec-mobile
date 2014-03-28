using Fatec.Core.Domain;
using Fatec.Repositories.Mapping;
using System;
using System.Xml.Linq;

namespace Fatec.Repositories.SharePoint.Mapping
{
	public class WarehouseMap : BaseMapper
	{
		public static Func<XElement, KeyMovement> Map = xElement =>
		{
			var keyMovement = new KeyMovement();
			keyMovement.Id = xElement.GetAttrValue<int>("ows_ID");
			keyMovement.Key = xElement.GetAttrValue<string>("ows_Chave").Split('#')[1];
			keyMovement.Requester = xElement.GetAttrValue<string>("ows_Requisitante").Split('#')[1];
			keyMovement.WithdrawalDate = xElement.GetAttrValue<DateTime>("ows_Data_x0020_de_x0020_Retirada");

			FillDefaultFields(keyMovement, xElement);

			return keyMovement;
		};
	}
}
