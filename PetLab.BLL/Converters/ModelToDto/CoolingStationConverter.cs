using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToDto {
	class CoolingStationConverter : TypeConverter<pickup_station_cooling, CoolingStationDto> {
		protected override CoolingStationDto ConvertCore(pickup_station_cooling source) {
			var result = new CoolingStationDto();
			result.Name = source.name;
			result.StationId = source.station_id;
			return result;
		}
	}
}
