using UnityEngine;
using System.Collections;

public class PlayerPreference {
	public enum PlayerPrefType {
		Float, 
		Int, 
		String
	}

	public class PlayerPrefValue {
		public PlayerPrefType type;
		public string stringValue;
		public int intValue;
		public float floatValue;
	}

	public string name;
	public bool isMarkedForDelete;
	public PlayerPrefValue value;
	public PlayerPrefValue initial;

	public string StringType 
	{
		get {
			if (this.value.type == PlayerPrefType.Float) return Constants.REAL; 
			if (this.value.type == PlayerPrefType.Int) return Constants.INTEGER; 
			return Constants.STRING; 
		}
	}

	public string StringValue 
	{
		get {
			if (this.value.type == PlayerPrefType.Float) return this.value.floatValue.ToString(); 
			if (this.value.type == PlayerPrefType.Int) return this.value.intValue.ToString (); 
			return this.value.stringValue; 
		}
	}

	public bool Changed 
	{
		get {
			if (initial.type != value.type) return true;
			switch (value.type) {
			case PlayerPrefType.Float:
				if (value.floatValue != initial.floatValue) return true;
				break;
			case PlayerPrefType.Int:
				if (value.intValue != initial.intValue) return true;
				break;
			case PlayerPrefType.String:
				if (value.stringValue != initial.stringValue) return true;
				break;
			}
			return false;
		}
	}

	public PlayerPreference (string name, string prefType, string valueTxt) 
	{
		this.name = name;
		value = new PlayerPrefValue();
		initial = new PlayerPrefValue();
		switch (prefType) {
		case Constants.INTEGER:
			value.intValue = initial.intValue = int.Parse(valueTxt);
			value.type = initial.type = PlayerPrefType.Int;
			break;
		case Constants.REAL:
			value.floatValue = initial.floatValue = float.Parse(valueTxt);
			value.type = initial.type = PlayerPrefType.Float;
			break;
		case Constants.STRING:
			value.stringValue = initial.stringValue = valueTxt;
			value.type = initial.type = PlayerPrefType.String;
			break;
		}
	}

	public void Reset() 
	{
		value.intValue = initial.intValue;
		value.stringValue= initial.stringValue;
		value.floatValue = initial.floatValue;
		value.type = initial.type;
	}
	
	public void Save() 
	{
		initial.intValue = value.intValue;
		initial.stringValue = value.stringValue;
		initial.floatValue = value.floatValue;
		initial.type = value.type;
	}
}
