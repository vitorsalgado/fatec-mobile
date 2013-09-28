using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Repositories;
using Fatec.Core.Services;
using System;
using System.Collections.Generic;

namespace Fatec.Services
{
	public class AnnouncementsServiceFacade : IAnnouncementService
	{
		private const string CACHE_AVISOS_HOME_ALL = "fatec.avisoshome.all";
		private const string CACHE_AVISO_HOME_ID = "fatec.avisohome.id-{0}";
		private const string CACHE_AVISOS_FATEC_ALL = "fatec.avisosfatec.all";
		private const string CACHE_AVISO_FATEC_ID = "fatec.avisofatec.id-{0}";
		private const string CACHE_ESTAGIO_ID = "fatec.estagio.id-{0}";
		private const string CACHE_ESTAGIO_ALL = "fatec.estagio.all";

		private const int CACHE_MIN_EXPIRATION_TIME = 10;
		private const int CACHE_MAX_EXPIRATION_TIME = 1440;

		private readonly IHomeAnnouncementsRepository _avisosHomeRepository;
		private readonly IAcademicDirectoryAnnouncementsRepository _avisosDiretorioAcademicoRepository;
		private readonly ITraineeRepository _avisosOportunidadesDeEstagioRepository;
		private readonly IFatecRepository _avisosFatecRepository;
		private readonly ITIAnnouncementsRepository _avisosDepartamentoDeInformaticaRepository;

		private readonly ICacheStrategy _cacheStrategy;

		public AnnouncementsServiceFacade(
			IHomeAnnouncementsRepository avisoHomeRepository,
			IAcademicDirectoryAnnouncementsRepository avisosDiretorioAcademicoRepository,
			ITraineeRepository oportunidadesDeEstagioRepository,
			IFatecRepository avisosFatecRepository,
			ITIAnnouncementsRepository avisosDepartamentoDeInformatica,
			ICacheStrategy cacheStrategy)
		{
			_avisosHomeRepository = avisoHomeRepository;
			_avisosDiretorioAcademicoRepository = avisosDiretorioAcademicoRepository;
			_avisosOportunidadesDeEstagioRepository = oportunidadesDeEstagioRepository;
			_avisosFatecRepository = avisosFatecRepository;
			_avisosDepartamentoDeInformaticaRepository = avisosDepartamentoDeInformatica;
			_cacheStrategy = cacheStrategy;
		}

		#region Home

		public Announcement GetHomeAnnouncementById(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "id must be greather than ZERO.");

			var cacheKey = string.Format(CACHE_AVISO_HOME_ID, id);
			return _cacheStrategy.Get(cacheKey, CACHE_MAX_EXPIRATION_TIME, () =>
			{
				return _avisosHomeRepository.Get(id);
			});
		}

		public ICollection<Announcement> GetHomeValidAnnouncements()
		{
			var cacheKey = CACHE_AVISOS_HOME_ALL;
			return _cacheStrategy.Get(cacheKey, CACHE_MIN_EXPIRATION_TIME, () =>
			{
				var avisosValidos = _avisosHomeRepository.GetAllValid();
				return avisosValidos;
			});
		}

		#endregion

		#region Fatec

		public Announcement GetFatecAnnouncementById(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "id must be greather than ZERO.");

			var cacheKey = string.Format(CACHE_AVISO_FATEC_ID, id);
			return _cacheStrategy.Get(cacheKey, CACHE_MAX_EXPIRATION_TIME, () =>
			{
				return _avisosFatecRepository.GetAnnouncementById(id);
			});
		}

		public ICollection<Announcement> GetFatecValidAnnouncements()
		{
			var cacheKey = CACHE_AVISOS_FATEC_ALL;
			return _cacheStrategy.Get(cacheKey, CACHE_MIN_EXPIRATION_TIME, () =>
			{
				var avisosValidos = _avisosFatecRepository.GetVigentAnnouncements();
				return avisosValidos;
			});
		}

		#endregion

		#region Diretorio Academico

		public Announcement GetAcademicDirectoryAnnouncementById(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "id must be greather than ZERO.");
			return _avisosDiretorioAcademicoRepository.Get(id);
		}

		public ICollection<Announcement> GetAcademicDirectoryValidAnnouncements()
		{
			return _avisosDiretorioAcademicoRepository.GetAllValid();
		}

		#endregion

		#region Estagios

		public Announcement GetInternshipOpportunityById(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "id must be greather than ZERO.");

			string cacheKey = string.Format(CACHE_ESTAGIO_ID, id);
			return _cacheStrategy.Get(cacheKey, CACHE_MIN_EXPIRATION_TIME, () =>
			{
				return _avisosOportunidadesDeEstagioRepository.Get(id);
			});
		}

		public ICollection<Announcement> GetValidIntershipOpportunities()
		{
			return _cacheStrategy.Get(CACHE_ESTAGIO_ALL, CACHE_MIN_EXPIRATION_TIME, () =>
			{
				return _avisosOportunidadesDeEstagioRepository.GetAllValid();
			});
		}

		#endregion

		#region Departamento de Informática

		public Announcement GetTIAnnouncementById(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "id must be greather than ZERO.");
			return _avisosDepartamentoDeInformaticaRepository.Get(id);
		}

		public ICollection<Announcement> GetTIValidAnnouncements()
		{
			return _avisosDepartamentoDeInformaticaRepository.GetAllValid();
		}

		#endregion
	}
}
