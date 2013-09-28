using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Services
{
	public interface IAnnouncementService
	{
		Announcement GetTIAnnouncementById(int id);
		Announcement GetAcademicDirectoryAnnouncementById(int id);
		Announcement GetInternshipOpportunityById(int id);
		Announcement GetFatecAnnouncementById(int id);
		Announcement GetHomeAnnouncementById(int id);
		ICollection<Announcement> GetTIValidAnnouncements();
		ICollection<Announcement> GetAcademicDirectoryValidAnnouncements();
		ICollection<Announcement> GetValidIntershipOpportunities();
		ICollection<Announcement> GetFatecValidAnnouncements();
		ICollection<Announcement> GetHomeValidAnnouncements();
	}
}
