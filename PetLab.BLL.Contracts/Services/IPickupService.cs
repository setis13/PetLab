using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Common.Services.Results;
using PetLab.BLL.Contracts.Services.Base;

namespace PetLab.BLL.Contracts.Services {
	public interface IPickupService : IService {
		/// <summary>
		/// получить все машины
		/// </summary>
		ServiceResult<IEnumerable<EquipmentDto>> LookupEquipments();
		/// <summary>
		/// получить все дефекты
		/// </summary>
		ServiceResult<IEnumerable<DefectDto>> LookupDefects();
		/// <summary>
		/// получить текущий заказ
		/// </summary>
		Task<ServiceResult<OrderDto>> LookupOrder(string equipmentId);
	}
}
