using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetLab.DAL.Models.xml {

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
	public partial class orderXml {

		private orderEtalons etalonsField;

		private byte shiftField;

		private string material_idField;

		private string dye_nameField;

		private string color_shadeField;

		private string batch_idField;

		private string order_idField;

		private byte count_socketsField;

		private string equipmentField;

		/// <remarks/>
		public orderEtalons etalons {
			get {
				return this.etalonsField;
			}
			set {
				this.etalonsField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public byte shift {
			get {
				return this.shiftField;
			}
			set {
				this.shiftField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string material_id {
			get {
				return this.material_idField;
			}
			set {
				this.material_idField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string dye_name {
			get {
				return this.dye_nameField;
			}
			set {
				this.dye_nameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string color_shade {
			get {
				return this.color_shadeField;
			}
			set {
				this.color_shadeField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string batch_id {
			get {
				return this.batch_idField;
			}
			set {
				this.batch_idField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string order_id {
			get {
				return this.order_idField;
			}
			set {
				this.order_idField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public byte count_sockets {
			get {
				return this.count_socketsField;
			}
			set {
				this.count_socketsField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string equipment {
			get {
				return this.equipmentField;
			}
			set {
				this.equipmentField = value;
			}
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class orderEtalons {

		private orderEtalonsColor colorField;

		private orderEtalonsWeight weightField;

		private orderEtalonsSlip slipField;

		private orderEtalonsThickness thicknessField;

		/// <remarks/>
		public orderEtalonsColor color {
			get {
				return this.colorField;
			}
			set {
				this.colorField = value;
			}
		}

		/// <remarks/>
		public orderEtalonsWeight weight {
			get {
				return this.weightField;
			}
			set {
				this.weightField = value;
			}
		}

		/// <remarks/>
		public orderEtalonsSlip slip {
			get {
				return this.slipField;
			}
			set {
				this.slipField = value;
			}
		}

		/// <remarks/>
		public orderEtalonsThickness thickness {
			get {
				return this.thicknessField;
			}
			set {
				this.thicknessField = value;
			}
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class orderEtalonsColor {

		private orderEtalonsColorRange[] rangeField;

		private orderEtalonsColorRay[] pickup_raysField;

		private string nameField;

		private byte socket_numberField;

		private byte pickup_modeField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("range")]
		public orderEtalonsColorRange[] range {
			get {
				return this.rangeField;
			}
			set {
				this.rangeField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("ray", IsNullable = false)]
		public orderEtalonsColorRay[] pickup_rays {
			get {
				return this.pickup_raysField;
			}
			set {
				this.pickup_raysField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name {
			get {
				return this.nameField;
			}
			set {
				this.nameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public byte socket_number {
			get {
				return this.socket_numberField;
			}
			set {
				this.socket_numberField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public byte pickup_mode {
			get {
				return this.pickup_modeField;
			}
			set {
				this.pickup_modeField = value;
			}
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class orderEtalonsColorRange {

		private string nameField;

		private decimal lim1Field;

		private decimal lim2Field;

		private decimal lim3Field;

		private decimal lim4Field;

		private decimal lim5Field;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name {
			get {
				return this.nameField;
			}
			set {
				this.nameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim1 {
			get {
				return this.lim1Field;
			}
			set {
				this.lim1Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim2 {
			get {
				return this.lim2Field;
			}
			set {
				this.lim2Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim3 {
			get {
				return this.lim3Field;
			}
			set {
				this.lim3Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim4 {
			get {
				return this.lim4Field;
			}
			set {
				this.lim4Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim5 {
			get {
				return this.lim5Field;
			}
			set {
				this.lim5Field = value;
			}
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class orderEtalonsColorRay {

		private string idField;

		private byte valueField;

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
		public byte value {
			get {
				return this.valueField;
			}
			set {
				this.valueField = value;
			}
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class orderEtalonsWeight {

		private decimal nameField;

		private decimal lim1Field;

		private decimal lim2Field;

		private decimal lim3Field;

		private decimal lim4Field;

		private decimal lim5Field;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal name {
			get {
				return this.nameField;
			}
			set {
				this.nameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim1 {
			get {
				return this.lim1Field;
			}
			set {
				this.lim1Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim2 {
			get {
				return this.lim2Field;
			}
			set {
				this.lim2Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim3 {
			get {
				return this.lim3Field;
			}
			set {
				this.lim3Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim4 {
			get {
				return this.lim4Field;
			}
			set {
				this.lim4Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim5 {
			get {
				return this.lim5Field;
			}
			set {
				this.lim5Field = value;
			}
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class orderEtalonsSlip {

		private string nameField;

		private decimal lim1Field;

		private decimal lim2Field;

		private decimal lim3Field;

		private decimal lim4Field;

		private decimal lim5Field;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name {
			get {
				return this.nameField;
			}
			set {
				this.nameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim1 {
			get {
				return this.lim1Field;
			}
			set {
				this.lim1Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim2 {
			get {
				return this.lim2Field;
			}
			set {
				this.lim2Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim3 {
			get {
				return this.lim3Field;
			}
			set {
				this.lim3Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim4 {
			get {
				return this.lim4Field;
			}
			set {
				this.lim4Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim5 {
			get {
				return this.lim5Field;
			}
			set {
				this.lim5Field = value;
			}
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class orderEtalonsThickness {

		private string nameField;

		private decimal lim3Field;

		private decimal lim4Field;

		private decimal lim5Field;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name {
			get {
				return this.nameField;
			}
			set {
				this.nameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim3 {
			get {
				return this.lim3Field;
			}
			set {
				this.lim3Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim4 {
			get {
				return this.lim4Field;
			}
			set {
				this.lim4Field = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal lim5 {
			get {
				return this.lim5Field;
			}
			set {
				this.lim5Field = value;
			}
		}
	}
}
