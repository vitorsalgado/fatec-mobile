using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Repositories
{
	public interface IFatecRepository
	{
		Announcement GetAnnouncementById(int id);
		ICollection<Announcement> GetVigentAnnouncements();
		ICollection<TeacherAbsence> GetTeacherAbsences();
		ICollection<ClassReplacement> GetVigentClassReplacements();
	}
}
