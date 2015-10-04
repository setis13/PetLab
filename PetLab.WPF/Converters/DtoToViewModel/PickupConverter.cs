using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.WPF.Models;

namespace PetLab.WPF.Converters.DtoToViewModel {
	public class PickupConverter : TypeConverter<PickupDto, PickupViewModel> {
		protected override PickupViewModel ConvertCore(PickupDto source) {
			var result = new PickupViewModel();
			result.DatetimeClose = source.DatetimeClose;
			result.DatetimeCreate = source.DatetimeCreate;
			result.DatetimeTake = source.DatetimeTake;

			result.EtalonMatch = source.EtalonMatch;
			result.VisualMatch = source.VisualMatch;

			result.Export = source.Export;
			result.Number = source.Number;
			result.StatioName = source.StatioName;

			result.BoxId = source.BoxId;
			result.OrderId = source.OrderId;
			result.PickupId = source.PickupId;
			result.ShiftId = source.ShiftId;

			result.PickupDefects = new Dictionary<string, PickupDefectViewModel[]>();
			foreach (var defectDto in source.Defects) {
				var pickupDefectsViewModel = new PickupDefectViewModel[source.CountSockets];
				for (byte i = 0; i < source.CountSockets; i++) {
					var pickupDefectDto = source.PickupDefects
						.FirstOrDefault(p => p.DefectId == defectDto.DefectId && p.Socket == i);
					if (pickupDefectDto != null) {
						pickupDefectsViewModel[i] = Mapper.Map<PickupDefectViewModel>(pickupDefectDto);
						pickupDefectsViewModel[i].CountSockets = source.CountSockets;
					} else {
						pickupDefectsViewModel[i] = new PickupDefectViewModel() {
							Socket = i,
							DefectId = defectDto.DefectId,
							PickupId = source.PickupId,
							CountSockets = source.CountSockets
						};
					}
				}
				result.PickupDefects.Add(defectDto.DefectId, pickupDefectsViewModel);
			}

			return result;
		}
	}
}
