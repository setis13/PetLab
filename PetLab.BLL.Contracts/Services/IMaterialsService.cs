using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Common.Services.Results;
using PetLab.BLL.Contracts.Services.Base;

namespace PetLab.BLL.Contracts.Services {
	public interface IMaterialsService : IService {
		/// <summary>
		/// получить все материалы
		/// </summary>
		ServiceResult<IEnumerable<MaterialDto>> LookupMaterials();
		/// <summary>
		/// получить все материалы из xml сервиса
		/// </summary>
		Task<ServiceResult<IEnumerable<MaterialXmlDto>>> LookupXmlMaterialsAsync();
		/// <summary>
		/// синхранизировать материалы
		/// </summary>
		ServiceResult UpdateMaterials(IEnumerable<MaterialXmlDto> materials);
	}
}
