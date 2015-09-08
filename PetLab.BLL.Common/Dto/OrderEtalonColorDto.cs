using System.Collections.Generic;

namespace PetLab.BLL.Common.Dto {
	public class OrderEtalonColorDto {
		public string Name { get; set; }
		public byte SocketNumber { get; set; }
		public byte PickupMode { get; set; }
		public IEnumerable<OrderEtalonColorRangeDto> Ranges { get; set; }
		public IEnumerable<OrderEtalonColorRayDto> Rays {get; set; }
    }
}