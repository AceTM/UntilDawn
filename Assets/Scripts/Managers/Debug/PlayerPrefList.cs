using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerPrefList : MonoBehaviour {
	[System.Serializable]
	public enum PlayerPrefType {
		Float, 
		Int, 
		String
	}

	[System.Serializable]
	public class PlayerPrefValue {
		public string name;
		public PlayerPrefType type;
	}

	public class PlayerPrefProperty {
		public string name;
		public string value;
		public PlayerPrefType type;
	}

	public List<PlayerPrefValue> prefValues = new List<PlayerPrefValue>();
	public List<PlayerPrefProperty> prefProperties = new List<PlayerPrefProperty>();
	public List<PlayerPrefProperty> initialPrefProperties = new List<PlayerPrefProperty>();

	public Transform rowObject;
	public Transform rowParent;

	private void OnEnable () 
	{

	}

	private void Start () 
	{
		ReadValues ();
		InstantiateRow ();
	}

	private void ReadValues () {
		for (int i = Constants.ZERO; i < prefValues.Count; i++) {
			PlayerPrefValue prefValue = prefValues[i];
			PlayerPrefProperty prefProperty = new PlayerPrefProperty();
			prefProperty = GetPlayerPrefValue(prefValue, prefProperty);
			prefProperties.Add(prefProperty);
		}
	}

	private PlayerPrefProperty GetPlayerPrefValue (PlayerPrefValue prefValue, PlayerPrefProperty prefProperty) {
		switch (prefValue.type) {
			case PlayerPrefType.Float: {
				prefProperty.name = prefValue.name;
				prefProperty.value = PlayerPrefs.GetFloat(prefValue.name).ToString();
				prefProperty.type = PlayerPrefType.Float;
				return prefProperty;
			}
			case PlayerPrefType.Int: {
				prefProperty.name = prefValue.name;
				prefProperty.value = PlayerPrefs.GetInt(prefValue.name).ToString();
				prefProperty.type = PlayerPrefType.Int;
				return prefProperty;
			}
			case PlayerPrefType.String: {
				prefProperty.name = prefValue.name;
				prefProperty.value = PlayerPrefs.GetString(prefValue.name);
				prefProperty.type = PlayerPrefType.String;
				return prefProperty;
			}
			default: {
				return null;
			}
		}
	}

	private void InstantiateRow ()
	{
		if (prefValues.Count == Constants.ZERO || 
		    prefProperties.Count == Constants.ZERO) 
			return;

		for (int i = Constants.ZERO; i < prefValues.Count; i++) {
			Transform prefRow = Instantiate(rowObject);
			prefRow.name = string.Format(ObjectConstants.PREF_ROW_OBJ + i);
			prefRow.SetParent(rowParent, false);

			var prefProperty = prefProperties[i];
			var prefValue = prefValues[i];

			AssignInitialProperties(prefProperties);

			RectTransform prefRowRect = (RectTransform) prefRow.GetComponent<RectTransform>();
			prefRowRect.position = new Vector3(prefRowRect.position.x, 
			                                   prefRowRect.position.y - (i * 70),
			                                   prefRowRect.position.z);

			Text prefText = prefRow.GetComponentInChildren<Text>();
			prefText.text = prefValue.name;

			InputField prefField = prefRow.GetComponentInChildren<InputField>();
			prefField.onValueChange.AddListener (delegate { OnPropertyChange(prefField.text, prefRow, prefProperty); } );
			prefField.text = prefProperty.value;

			ComboBox prefCombo =  prefRow.GetComponentInChildren<ComboBox>();
			int typeIndex = (int)Enum.Parse(typeof(PlayerPrefType), prefProperty.type.ToString());
			prefCombo.Interactable = false;
			prefCombo.SelectedIndex = typeIndex;

			Button prefButton = prefRow.GetComponentInChildren<Button>();
			prefButton.onClick.AddListener (() => OnClickSave(prefRow, prefProperty));
			prefButton.interactable = false;
		}

		AssignInitialProperties (prefProperties);
	}

	private void AssignInitialProperties (List<PlayerPrefProperty> properties)
	{
		foreach (PlayerPrefProperty property in properties) {
			initialPrefProperties.Add(property);
		}
	}

	private void ToggleSaveButton (Transform prefRow, bool toggle)
	{
		Button prefButton = prefRow.GetComponentInChildren<Button>();
		prefButton.interactable = true;
	}

	public void OnClickSave (Transform changedRow, PlayerPrefProperty prefProperty)
	{
		InputField prefInput = changedRow.GetComponentInChildren<InputField>();
		switch (prefProperty.type) {
			case PlayerPrefType.Float: {
				float floatValue;
				if(float.TryParse(prefInput.text, out floatValue))
					PlayerPrefs.SetFloat(prefProperty.name, floatValue);
				else return;
				break;
			}
			case PlayerPrefType.Int: {
				int intValue;
				if(int.TryParse(prefInput.text, out intValue)) {
					PlayerPrefs.SetInt(prefProperty.name, intValue);
				}
				else return;
				break;
			}
			case PlayerPrefType.String: {
				PlayerPrefs.SetString(prefProperty.name, prefInput.text);
				break;
			}
			default: {
				return;
			}
		}
		prefProperty.value = prefInput.text;
		changedRow.GetComponentInChildren<Button>().interactable = false;
	}

	public void OnPropertyChange (string changedString, Transform changedRow, PlayerPrefProperty prefProperty)
	{
		if (prefProperty.value != changedString)
			changedRow.GetComponentInChildren<Button>().interactable = true;
		else 
			changedRow.GetComponentInChildren<Button>().interactable = false;
	}
}
