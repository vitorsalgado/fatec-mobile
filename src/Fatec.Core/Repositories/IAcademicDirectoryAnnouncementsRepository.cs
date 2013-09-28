using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Repositories
{
	public interface IAcademicDirectoryAnnouncementsRepository
	{
		Announcement Get(int id);
		ICollection<Announcement> GetAllValid();
	}
}
