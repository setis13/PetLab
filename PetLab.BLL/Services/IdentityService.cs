using System;
using System.Collections.Generic;
using System.Linq;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Common.Services.Results;
using PetLab.BLL.Contracts;
using PetLab.BLL.Contracts.Services;
using PetLab.BLL.Services.Base;
using PetLab.DAL.Contracts;
using PetLab.DAL.Models;

namespace PetLab.BLL.Services {
	/// <summary>
	/// сервис авторизации
	/// </summary>
	public class IdentityService : HostService<IIdentityService>, IIdentityService {
		public IdentityService(IServicesHost servicesHost, IUnitOfWork unitOfWork)
			: base(servicesHost, unitOfWork) {
		}

		/// <summary>
		/// получить всех пользователей
		/// </summary>
		public ServiceResult<IEnumerable<UserDto>> LookupUser() {
			try {
				var repository = UnitOfWork.GetRepository<user>();
				var users = repository.GetAll();
				return new ServiceResult<IEnumerable<UserDto>>(AutoMapper.Mapper.Map<IEnumerable<UserDto>>(users));
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<IEnumerable<UserDto>>>(exception);
			}
		}

		/// <summary>
		/// получить все смены
		/// </summary>
		public ServiceResult<IEnumerable<ShiftDto>> LookupShift() {
			try {
				var repository = UnitOfWork.GetRepository<shift>();
				var shifts = repository.GetAll();
				return new ServiceResult<IEnumerable<ShiftDto>>(AutoMapper.Mapper.Map<IEnumerable<ShiftDto>>(shifts));
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult<IEnumerable<ShiftDto>>>(exception);
			}
		}

		/// <summary>
		/// попробовать авторизоваться
		/// </summary>
		/// <param name="shift">смена</param>
		/// <param name="useId">id пользователя</param>
		/// <param name="password">пароль</param>
		public LoginResult Login(byte shift, int useId, string password) {
			try {
				if (password == shift.ToString()) {

					var repository = UnitOfWork.GetRepository<shift>();
					var currentShift = repository.GetAll().OrderByDescending(o => o.shift_id).FirstOrDefault();

					var result = new LoginResult() { Result = true, ShiftNumber = currentShift?.shift_number };
#if DEBUG
					result.CheckNewShift = false;
					//#else
					result.CheckNewShift = currentShift == null ||
						DateTime.Now.Hour < currentShift.shift_time.begin.Hours ||
						DateTime.Now.Hour >= currentShift.shift_time.end.Hours;
#endif
					return result;
				} else {
					return ServiceResult.ExceptionFactory<LoginResult>(new Exception("Пароль неверный"));
				}
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<LoginResult>(exception);
			}
		}

		/// <summary>
		/// создать новую смену
		/// </summary>
		public ServiceResult CreateShift(int userId, byte shiftNumber, byte timeId) {
			try {
				var shift = new shift() {
					shift_number = shiftNumber,
					time_id = timeId,
					user_id = userId,
					datetime = DateTime.Now
				};
				var repository = UnitOfWork.GetRepository<shift>();
				repository.Insert(shift);
				UnitOfWork.SaveChanges();
				return new ServiceResult();
			} catch (Exception exception) {
				return ServiceResult.ExceptionFactory<ServiceResult>(exception);
			}
		}
	}
}
