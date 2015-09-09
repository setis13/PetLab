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
		/// получить все станции охлаждения
		/// </summary>
		ServiceResult<IEnumerable<CoolingStationDto>> LookupCoolingStations();

		/// <summary>
		/// получить текущий заказ
		/// </summary>
		Task<ServiceResult<OrderDto>> LookupOrder(string equipmentId);

		/// <summary>
		/// получить все съёмы по указанной машине. Можно выбрать с учётом экспорта в SAP
		/// </summary>
		ServiceResult<IEnumerable<PickupDto>> LookupPickups(int shiftId, string equipmentId, bool? export = null);

		/// <summary>
		/// найти открытый съём
		/// </summary>
		ServiceResult<PickupDto> LookupOpenPickup(string equipmentId);

		/// <summary>
		/// открыть съём
		/// </summary>
		/// <param name="orderId">номер заказа</param>
		/// <param name="boxId">номер короба, если есть</param>
		/// <param name="shiftId">смена</param>
		/// <param name="number">номер съёма</param>
		/// <param name="take">когда взят съём</param>
		/// <param name="stationId">станция охлаждения</param>
		/// <returns>PickupDto</returns>
		ServiceResult<PickupDto> OpenPickup(string orderId, string boxId, int shiftId,  DateTime take, byte stationId);

		/// <summary>
		/// закрыть съём
		/// </summary>
		ServiceResult ClosePickup(int pickupId);

		/// <summary>
		/// получить отмеченных дефектов к съему
		/// </summary>
		/// <returns></returns>
		ServiceResult<IEnumerable<PickupDefectDto>> LookupPickupDefect(int pickupId);

		/// <summary>
		/// задать/удалить дефект. Если grade не задан, то удалить дефект
		/// </summary>
		ServiceResult<PickupDefectDto> SetPickupDefect(int pickupId, string defectId, byte socket, byte? grade = null);

		/// <summary>
		/// получить пределы измерений цвета для заказа
		/// </summary>
		ServiceResult<IEnumerable<OrderEtalonColorRangeDto>> LookupEtalonColorRanges(string orderId);

		/// <summary>
		/// задать значение цвета
		/// </summary>
		ServiceResult SetColor(PickupEtalonColorRangeDto rangeDto);

		/// <summary>
		/// задать, эталон соответствует
		/// </summary>
		ServiceResult SetEtalonMatch(int pickupId, bool value);

		/// <summary>
		/// задать, визуальное сравнение
		/// </summary>
		ServiceResult SetVisualMatch(int pickupId, bool value);
	}
}
