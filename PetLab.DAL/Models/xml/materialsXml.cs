using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetLab.DAL.Models.xml {
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
	public partial class materialsXml {

		private materialsmaterial[] materialField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("material")]
		public materialsmaterial[] material {
			get {
				return this.materialField;
			}
			set {
				this.materialField = value;
			}
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class materialsmaterial {

		private string idField;

		private string textField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string id {
			get {
				return this.idField;
			}
			set {
				this.idField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string text {
			get {
				return this.textField;
			}
			set {
				this.textField = value;
			}
		}
	}
}
