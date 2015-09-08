namespace PetLab.BLL.Common.Dto {
	public class OrderDto {
		public string OrderId { get; set; }
		public string BatchId { get; set; }
		public string MaterialName { get; set; }
		public int ShiftId { get; set; }
		public string DyeName { get; set; }
		public string ColorShade { get; set; }
		public short CountSocket { get; set; }
		public string EquipmentId { get; set; }
		public OrderEtalonColorDto EtalonColor { get; set; }
	}
}
