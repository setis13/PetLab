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
	public class MaterialsService : HostService<IMaterialsService>, IMaterialsService {

		public MaterialsService(IServicesHost servicesHost, IUnitOfWork unitOfWork)
			: base(servicesHost, unitOfWork) {
		}

		/// <summary>
		/// получить все материалы
		/// </summary>
		public ServiceResult<IEnumerable<MaterialDto>> LookupMaterials() {
			try {
				var repository = UnitOfWork.GetRepository<material>();
				var materials = repository.GetAll().ToList();
				return new ServiceResult<IEnumerable<MaterialDto>>(AutoMapper.Mapper.Map<IEnumerable<MaterialDto>>(materials));
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<IEnumerable<MaterialDto>>>(exception);
			}
		}

		public async Task<ServiceResult<IEnumerable<MaterialXmlDto>>> LookupXmlMaterialsAsync() {
			try {
				var repasitory = UnitOfWork.GetXmlRepository<XmlMaterialsRepository>();
				materialsXml xmlMaterials = await repasitory.GetAsync();
				var materials = AutoMapper.Mapper.Map<IEnumerable<MaterialXmlDto>>(xmlMaterials.material);
				return new ServiceResult<IEnumerable<MaterialXmlDto>>(materials);
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<IEnumerable<MaterialXmlDto>>>(exception);
			}
		}

		/// <summary>
		/// синхранизировать материалы
		/// </summary>
		public ServiceResult UpdateMaterials(IEnumerable<MaterialXmlDto> materialsDto) {
			try {
				var repasitory = UnitOfWork.GetRepository<material>();
				var materials = AutoMapper.Mapper.Map<IEnumerable<material>>(materialsDto);
				foreach (var material in materials) {
					repasitory.Save(material);
				}
				UnitOfWork.SaveChanges();
				return new ServiceResult();
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult>(exception);
			}
		}
	}
}