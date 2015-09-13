using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetLab.DAL.Models.xml {
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	[System.Xml.Serialization.XmlRootAttribute("defects", Namespace = "", IsNullable = false)]
	public partial class defectsXml {

		private defectsDefect[] defectField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("defect")]
		public defectsDefect[] defect {
			get {
				return this.defectField;
			}
			set {
				this.defectField = value;
			}
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class defectsDefect {

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
