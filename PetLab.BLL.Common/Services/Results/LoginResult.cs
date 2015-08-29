namespace PetLab.BLL.Common.Services.Results {
	public class LoginResult : ServiceResult {
		public bool Result { get; set; }
		public bool CheckNewShift { get; set; }
		public byte? ShiftNumber { get; set; }
	}
}
