using System;
using System.Collections.Generic;
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

	}
}
