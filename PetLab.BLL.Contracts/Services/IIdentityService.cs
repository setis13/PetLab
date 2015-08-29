using System;
using System.Collections.Generic;
using System.Linq;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Common.Services.Results;
using PetLab.BLL.Contracts.Services.Base;

namespace PetLab.BLL.Contracts.Services {
	public interface IIdentityService : IService {

		/// <summary>
		/// получить всех пользователей
		/// </summary>
		ServiceResult<IEnumerable<UserDto>> LookupUser();

		/// <summary>
		/// получить все смены
		/// </summary>
		ServiceResult<IEnumerable<ShiftDto>> LookupShift();

		/// <summary>
		/// попробовать авторизоваться
		/// </summary>
		/// <param name="shift">смена</param>
		/// <param name="useId">id пользователя</param>
		/// <param name="password">пароль</param>
		LoginResult Login(byte shift, int useId, string password);

		/// <summary>
		/// создать новую смену
		/// </summary>
		ServiceResult CreateShift(int userId, byte shiftNumber, byte timeId);
	}
}
