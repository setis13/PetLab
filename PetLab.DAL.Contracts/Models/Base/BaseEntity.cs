using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetLab.DAL.Contracts.Models.Base {
	public abstract class BaseEntity {
		public object[] GetKey() {
			var keys = new List<object>();
			foreach (var propertyInfo in GetType().GetProperties()) {
				var attrs = propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false);
				if (attrs.Length > 0) {
					keys.Add(propertyInfo.GetValue(this));
				}
			}
			return keys.ToArray();
		}
	}
}
