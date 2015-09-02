using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Common.Services.Results;
using PetLab.BLL.Contracts;
using PetLab.BLL.Contracts.Services;
using PetLab.BLL.Services.Base;
using PetLab.DAL;
using PetLab.DAL.Contracts;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;
using PetLab.DAL.Repositories;

namespace PetLab.BLL.Services {
	public class PickupService : HostService<IPickupService>, IPickupService {
		public PickupService(IServicesHost servicesHost, IUnitOfWork unitOfWork)
			: base(servicesHost, unitOfWork) {
		}

		/// <summary>
		/// получить текущий заказ
		/// </summary>
		public async Task<ServiceResult<OrderDto>> LookupOrder(string equipmentId) {
			try {
				//используемые репазитории
				var repositoryOrder = UnitOfWork.GetRepository<order>();
				var repositoryOrderEtalonColor = UnitOfWork.GetRepository<order_etalon_color>();
				var repositoryOrderEtalonSlip = UnitOfWork.GetRepository<order_etalon_slip>();
				var repositoryOrderEtalonThickness = UnitOfWork.GetRepository<order_etalon_thickness>();
				var repositoryOrderEtalonWeight = UnitOfWork.GetRepository<order_etalon_weight>();
				var repositoryOrderEtalonColorRange = UnitOfWork.GetRepository<order_etalon_color_range>();
				var repositoryOrderEtalonColorRay = UnitOfWork.GetRepository<order_etalon_color_ray>();
				//нашли order в бд
				order order = repositoryOrder.SearchFor(o => o.equipment_id == equipmentId).FirstOrDefault();
				//надо в любом случае обновить заказ
				//if (order == null) {
				var repositoryXml = UnitOfWork.GetXmlRepository<XmlOrderRepository>();
				orderXml xmlOrder = await repositoryXml.GetAsync(equipmentId);
				if (xmlOrder != null) {
					//смапили всю xml
					order = AutoMapper.Mapper.Map<order>(xmlOrder);
					var orderEtalonColor = AutoMapper.Mapper.Map<order_etalon_color>(xmlOrder);
					var orderEtalonSlip = AutoMapper.Mapper.Map<order_etalon_slip>(xmlOrder);
					var orderEtalonThickness = AutoMapper.Mapper.Map<order_etalon_thickness>(xmlOrder);
					var orderEtalonWeight = AutoMapper.Mapper.Map<order_etalon_weight>(xmlOrder);
					var orderEtalonColorRange = AutoMapper.Mapper.Map<IEnumerable<order_etalon_color_range>>(xmlOrder);
					var orderEtalonColorRay = AutoMapper.Mapper.Map<IEnumerable<order_etalon_color_ray>>(xmlOrder);
					//сохраняем в репазиторий
					repositoryOrder.Save(order);
					if (orderEtalonColor != null) {
						repositoryOrderEtalonColor.Save(orderEtalonColor);
						foreach (var etalonColorRange in orderEtalonColorRange) {
							repositoryOrderEtalonColorRange.Save(etalonColorRange);
						}
						foreach (var etalonColorRay in orderEtalonColorRay) {
							repositoryOrderEtalonColorRay.Save(etalonColorRay);
						}
					}
					if (orderEtalonSlip != null)
						repositoryOrderEtalonSlip.Save(orderEtalonSlip);
					if (orderEtalonThickness != null)
						repositoryOrderEtalonThickness.Save(orderEtalonThickness);
					if (orderEtalonWeight != null)
						repositoryOrderEtalonWeight.Save(orderEtalonWeight);

					//сохраняем в бд
					UnitOfWork.SaveChanges();
				} else {
					throw new Exception("SAP не вернул xml");
				}
				//}
				return new ServiceResult<OrderDto>(AutoMapper.Mapper.Map<OrderDto>(order));
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<OrderDto>>(exception);
			}
		}

		/// <summary>
		/// получить все машины
		/// </summary>
		public ServiceResult<IEnumerable<EquipmentDto>> LookupEquipments() {
			try {
				var repository = UnitOfWork.GetRepository<equipment>();
				var users = repository.GetAll();
				return new ServiceResult<IEnumerable<EquipmentDto>>(AutoMapper.Mapper.Map<IEnumerable<EquipmentDto>>(users));
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<IEnumerable<EquipmentDto>>>(exception);
			}
		}

		/// <summary>
		/// получить все дефекты
		/// </summary>
		public ServiceResult<IEnumerable<DefectDto>> LookupDefects() {
			try {
				var repository = UnitOfWork.GetRepository<defect>();
				var defects = repository.GetAll();
				return new ServiceResult<IEnumerable<DefectDto>>(AutoMapper.Mapper.Map<IEnumerable<DefectDto>>(defects));
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<IEnumerable<DefectDto>>>(exception);
			}
		}

		/// <summary>
		/// получить все съёмы по указанной машине. Можно выбрать с учётом экспорта в SAP
		/// </summary>
		public ServiceResult<IEnumerable<PickupDto>> LookupPickups(int shiftId, string equipmentId, bool? export = null) {
			try {
				var repository = UnitOfWork.GetRepository<pickup>();
				var pickups = repository.SearchFor(p => p.shift_id == shiftId && p.order.equipment_id == equipmentId && (export == null || p.export == export)).Include("pickup_station_cooling");
				return new ServiceResult<IEnumerable<PickupDto>>(AutoMapper.Mapper.Map<IEnumerable<PickupDto>>(pickups));
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<IEnumerable<PickupDto>>>(exception);
			}
		}

		/// <summary>
		/// найти открытый съём
		/// </summary>
		public ServiceResult<PickupDto> LookupOpenPickup(string equipmentId) {
			try {
				var repository = UnitOfWork.GetRepository<equipment>();
				var pickup = repository.GetById(equipmentId).pickup;
				return new ServiceResult<PickupDto>(pickup != null ? AutoMapper.Mapper.Map<PickupDto>(pickup) : null);
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<PickupDto>>(exception);
			}
		}

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
		public ServiceResult<PickupDto> OpenPickup(string orderId, string boxId, int shiftId, byte number, DateTime take, byte stationId) {
			try {
				var pickupRepository = UnitOfWork.GetRepository<pickup>();
				var orderRepository = UnitOfWork.GetRepository<order>();
				var equipmentRepository = UnitOfWork.GetRepository<equipment>();
				//получили указанный заказ
				var order = orderRepository.SearchFor(o => o.order_id == orderId).First();
				//проверили на открытые съёмы
				if (order.equipment.pickup != null) {
					throw new Exception("Имеется незакрытый съём");
				}
				//создали съём
				var pickup = new pickup();
				pickup.order_id = orderId;
				pickup.box_id = boxId;
				pickup.shift_id = shiftId;
				pickup.number = number;
				pickup.datetime_take = take;
				pickup.station_id = stationId;
				pickup.datetime_create = DateTime.Now;
				//всё созранили
				pickupRepository.Save(pickup);
				UnitOfWork.SaveChanges();
				order.equipment.pickup = pickup;
				equipmentRepository.Save(order.equipment);
				UnitOfWork.SaveChanges();
				pickupRepository.ReferenceLoad(pickup, p => p.pickup_station_cooling);
				return new ServiceResult<PickupDto>(AutoMapper.Mapper.Map<PickupDto>(pickup));
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<PickupDto>>(exception);
			}
		}

		/// <summary>
		/// закрыть съём
		/// </summary>
		public ServiceResult ClosePickup(int pickupId) {
			try {
				var repository = UnitOfWork.GetRepository<pickup>();
				var pickup = repository.GetById(pickupId);
				if (pickup == null) {
					throw new Exception("съём не найдем");
				}
				foreach (var equipment in pickup.equipments) {
					equipment.pickup_id = null;
				}
				pickup.datetime_close = DateTime.Now;
				repository.Save(pickup);
				UnitOfWork.SaveChanges();
				return new ServiceResult();
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult>(exception);
			}
		}

		/// <summary>
		/// получить отмеченных дефектов к съему
		/// </summary>
		/// <returns></returns>
		public ServiceResult<IEnumerable<PickupDefectDto>> LookupPickupDefect(int pickupId) {
			try {
				var repository = UnitOfWork.GetRepository<pickup>();
				var pickup = repository.GetById(pickupId);
				if (pickup == null) {
					throw new Exception("съём не найдем");
				}
				return new ServiceResult<IEnumerable<PickupDefectDto>>(AutoMapper.Mapper.Map<IEnumerable<PickupDefectDto>>(pickup.pickup_defects));
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<IEnumerable<PickupDefectDto>>>(exception);
			}
		}

		/// <summary>
		/// задать/удалить дефект. Если grade не задан, то удалить дефект
		/// </summary>
		public ServiceResult<PickupDefectDto> SetPickupDefect(int pickupId, string defectId, byte socket, byte? grade = null) {
			try {
				var repository = UnitOfWork.GetRepository<pickup_defects>();
				var pickupDefect = repository.GetById(socket, defectId, pickupId);
				if (grade != null) {
					if (pickupDefect != null) {
						pickupDefect.grade = grade.Value;
					} else {
						pickupDefect = new pickup_defects() {
							defect_id = defectId,
							grade = grade.Value,
							pickup_id = pickupId,
							socket = socket
						};
					}
					repository.Save(pickupDefect);
				} else {
					if (pickupDefect != null) {
						repository.Delete(pickupDefect);
					}
				}
				UnitOfWork.SaveChanges();
				return new ServiceResult<PickupDefectDto>(pickupDefect != null ? AutoMapper.Mapper.Map<PickupDefectDto>(pickupDefect) : null);
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<PickupDefectDto>>(exception);
			}
		}

		/// <summary>
		/// получить пределы измерений цвета для заказа
		/// </summary>
		public ServiceResult<IEnumerable<OrderEtalonColorRangeDto>> LookupEtalonColorRanges(string orderId) {
			try {
				var repository = UnitOfWork.GetRepository<order>();
				var ranges = repository.GetById(orderId).order_etalon_color_range.ToList();
				return new ServiceResult<IEnumerable<OrderEtalonColorRangeDto>>(
					AutoMapper.Mapper.Map<IEnumerable<OrderEtalonColorRangeDto>>(ranges));
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<IEnumerable<OrderEtalonColorRangeDto>>>(exception);
			}
		}

		/// <summary>
		/// задать/удалить значение цвета
		/// </summary>
		public ServiceResult<PickupEtalonColorRangeDto> SetColor(int pickupId, string orderId, string rangeName, decimal? value = null) {
			try {
				var repository = UnitOfWork.GetRepository<pickup_etalon_color_range>();
				var pickupEtalonColorRange = repository.GetById(pickupId, orderId, rangeName);
				if (value != null) {
					if (pickupEtalonColorRange != null) {
						pickupEtalonColorRange.value = value.Value;
					} else {
						pickupEtalonColorRange = new pickup_etalon_color_range() {
							range_name = rangeName,
							value = value.Value,
							pickup_id = pickupId,
							order_id = orderId
						};
					}
					repository.Save(pickupEtalonColorRange);
				} else {
					if (pickupEtalonColorRange != null) {
						repository.Delete(pickupEtalonColorRange);
					}
				}
				UnitOfWork.SaveChanges();
				return new ServiceResult<PickupEtalonColorRangeDto>(pickupEtalonColorRange != null ? AutoMapper.Mapper.Map<PickupEtalonColorRangeDto>(pickupEtalonColorRange) : null);
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<PickupEtalonColorRangeDto>>(exception);
			}
		}

		/// <summary>
		/// задать, эталон соответствует
		/// </summary>
		public ServiceResult SetEtalonMatch(int pickupId, bool value) {
			try {
				var repository = UnitOfWork.GetRepository<pickup>();
				var pickup = repository.GetById(pickupId);
				if (pickup == null) {
					throw new Exception("съём не найдем");
				}
				pickup.etalon_match = value;
				repository.Save(pickup);
				UnitOfWork.SaveChanges();
				return new ServiceResult();
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult>(exception);
			}
		}

		/// <summary>
		/// задать, визуальное сравнение
		/// </summary>
		public ServiceResult SetVisualMatch(int pickupId, bool value) {
			try {
				var repository = UnitOfWork.GetRepository<pickup>();
				var pickup = repository.GetById(pickupId);
				if (pickup == null) {
					throw new Exception("съём не найдем");
				}
				pickup.visual_match = value;
				repository.Save(pickup);
				UnitOfWork.SaveChanges();
				return new ServiceResult();
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult>(exception);
			}
		}

	}
}
