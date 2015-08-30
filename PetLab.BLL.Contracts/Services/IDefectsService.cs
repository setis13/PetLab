using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Common.Services.Results;
using PetLab.BLL.Contracts.Services.Base;

namespace PetLab.BLL.Contracts.Services {
	public interface IDefectsService : IService {
		/// <summary>
		/// получить все дефекты
		/// </summary>
		ServiceResult<IEnumerable<DefectDto>> LookupDefects();
		/// <summary>
		/// асинхронно получить все дефекты из xml сервиса
		/// </summary>
		Task<ServiceResult<IEnumerable<DefectXmlDto>>> LookupXmlDefectsAsync();
		/// <summary>
		/// синхранизировать дефекты
		/// </summary>
		ServiceResult UpdateDefects(IEnumerable<DefectXmlDto> defects);
	}
}
