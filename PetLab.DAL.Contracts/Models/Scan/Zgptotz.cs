using System;
using System.Globalization;

namespace PetLab.DAL.Contracts.Models.Scan {
	public class Zgptotz {
		public Zgptotz(string content) {
			var parts = content.Split(';');
			EquipmentId = byte.Parse(parts[0]);
			Begin = DateTime.ParseExact(parts[1], "yyyyMMddHHmm", CultureInfo.InvariantCulture);
			End = DateTime.ParseExact(parts[2], "yyyyMMddHHmm", CultureInfo.InvariantCulture);
		}

		public byte EquipmentId { get; set; }
		public DateTime Begin { get; set; }
		public DateTime End { get; set; }
	}
}
