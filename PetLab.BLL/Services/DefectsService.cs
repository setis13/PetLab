using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Common.Services.Results;
using PetLab.BLL.Contracts;
using PetLab.BLL.Contracts.Services;
using PetLab.BLL.Contracts.Services.Base;
using PetLab.BLL.Services.Base;
using PetLab.DAL;
using PetLab.DAL.Contracts;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;
using PetLab.DAL.Repositories;

namespace PetLab.BLL.Services {
	public class DefectsService : HostService<IDefectsService>, IDefectsService {

		public DefectsService(IServicesHost servicesHost, IUnitOfWork unitOfWork) 
			: base(servicesHost, unitOfWork) {
		}

		/// <summary>
		/// получить все дефекты
		/// </summary>
		public ServiceResult<IEnumerable<DefectDto>> LookupDefects() {
			try {
				var repository = UnitOfWork.GetRepository<defect>();
				var defects = repository.GetAll().ToList();
				return new ServiceResult<IEnumerable<DefectDto>>(AutoMapper.Mapper.Map<IEnumerable<DefectDto>>(defects));
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<IEnumerable<DefectDto>>>(exception);
			}
		}

		/// <summary>
		/// асинхронно получить дефекты из xml
		/// </summary>
		/// <returns></returns>
		public async Task<ServiceResult<IEnumerable<DefectXmlDto>>> LookupXmlDefectsAsync() {
			try {
				var repasitory = UnitOfWork.GetXmlRepository<XmlDefectsRepository>();
				defectsXml xmlDefects = await repasitory.GetAsync();
				var defects = AutoMapper.Mapper.Map<IEnumerable<DefectXmlDto>>(xmlDefects.defect);
				return new ServiceResult<IEnumerable<DefectXmlDto>>(defects);
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<IEnumerable<DefectXmlDto>>>(exception);
			}
		}

		/// <summary>
		/// синхранизировать дефекты
		/// </summary>
		public ServiceResult UpdateDefects(IEnumerable<DefectXmlDto> defectsDto) {
			try {
				var repasitory = UnitOfWork.GetRepository<defect>();
				var defects = AutoMapper.Mapper.Map<IEnumerable<defect>>(defectsDto);
				foreach (var defect in defects) {
					repasitory.Save(defect);
				}
				UnitOfWork.SaveChanges();
				return new ServiceResult();
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult>(exception);
			}
		}
	}
}
