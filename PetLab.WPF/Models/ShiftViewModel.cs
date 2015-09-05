using System;

namespace PetLab.WPF.Models {
	public class ShiftViewModel {
		public int ShiftId { get; set; }
		public DateTime Datetime { get; set; }
		public byte ShiftNumber { get; set; }
		public byte TimeId { get; set; }
		public int UserId { get; set; }
	}
}
