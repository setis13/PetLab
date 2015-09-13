
using System;

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute("pickup", Namespace = "", IsNullable = false)]
public partial class pickupXml {

	private pickupColor colorField;

	private pickupSlip_meas[] slipField;

	private pickupWeight_meas[] weightField;

	private pickupThickness_meas[] thicknessField;

	private pickupDefect_meas[] defectField;

	private DateTime date_takeField;

	private DateTime date_beginField;

	private DateTime date_endField;

	private byte numberField;

	private string station_coolingField;

	private string equipmentField;

	private bool etalon_matchField;

	private string userField;

	/// <remarks/>
	public pickupColor color {
		get {
			return this.colorField;
		}
		set {
			this.colorField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlArrayItemAttribute("slip_meas", IsNullable = false)]
	public pickupSlip_meas[] slip {
		get {
			return this.slipField;
		}
		set {
			this.slipField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlArrayItemAttribute("weight_meas", IsNullable = false)]
	public pickupWeight_meas[] weight {
		get {
			return this.weightField;
		}
		set {
			this.weightField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlArrayItemAttribute("thickness_meas", IsNullable = false)]
	public pickupThickness_meas[] thickness {
		get {
			return this.thicknessField;
		}
		set {
			this.thicknessField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlArrayItemAttribute("defect_meas", IsNullable = false)]
	public pickupDefect_meas[] defect {
		get {
			return this.defectField;
		}
		set {
			this.defectField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public DateTime date_take {
		get {
			return this.date_takeField;
		}
		set {
			this.date_takeField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public DateTime date_begin {
		get {
			return this.date_beginField;
		}
		set {
			this.date_beginField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public DateTime date_end {
		get {
			return this.date_endField;
		}
		set {
			this.date_endField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte number {
		get {
			return this.numberField;
		}
		set {
			this.numberField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string station_cooling {
		get {
			return this.station_coolingField;
		}
		set {
			this.station_coolingField = value;
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

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public bool etalon_match {
		get {
			return this.etalon_matchField;
		}
		set {
			this.etalon_matchField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string user {
		get {
			return this.userField;
		}
		set {
			this.userField = value;
		}
	}
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class pickupSlip_meas {

	private decimal valueField;

	private ushort stepField;

	private decimal deviationField;

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public decimal value {
		get {
			return this.valueField;
		}
		set {
			this.valueField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public ushort step {
		get {
			return this.stepField;
		}
		set {
			this.stepField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public decimal deviation {
		get {
			return this.deviationField;
		}
		set {
			this.deviationField = value;
		}
	}
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class pickupWeight_meas {

	private byte socketField;

	private decimal valueField;

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte socket {
		get {
			return this.socketField;
		}
		set {
			this.socketField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public decimal value {
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
public partial class pickupThickness_meas {

	private byte socketField;

	private decimal valueField;

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte socket {
		get {
			return this.socketField;
		}
		set {
			this.socketField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public decimal value {
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
public partial class pickupDefect_meas {

	private byte socketField;

	private string defect_idField;

	private byte gradeField;

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte socket {
		get {
			return this.socketField;
		}
		set {
			this.socketField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string defect_id {
		get {
			return this.defect_idField;
		}
		set {
			this.defect_idField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public byte grade {
		get {
			return this.gradeField;
		}
		set {
			this.gradeField = value;
		}
	}
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class pickupColor {

	private pickupColorRange[] rangeField;

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("range")]
	public pickupColorRange[] range {
		get {
			return this.rangeField;
		}
		set {
			this.rangeField = value;
		}
	}
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class pickupColorRange {

	private string nameField;

	private decimal valueField;

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
	public decimal value {
		get {
			return this.valueField;
		}
		set {
			this.valueField = value;
		}
	}
}
