﻿using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToDto {
	public class OrderConverter : TypeConverter<order, OrderDto> {
		protected override OrderDto ConvertCore(order source) {
			var result = new OrderDto();
			result.BatchId = source.batch_id;
			result.ShiftId = source.shift_id;
			result.OrderId = source.order_id;
			result.EquipmentId = source.equipment_id;
			result.ColorShade = source.color_shade;
			result.CountSocket = source.count_socket;
			result.DyeName = source.dye_name;
			if (source.material != null) {
				result.MaterialName = source.material.name;
			} else {
				result.MaterialName = "НЕОПРЕДЕЛЕННЫЙ МАТЕРИАЛ";
			}
            return result;
		}
	}
}
