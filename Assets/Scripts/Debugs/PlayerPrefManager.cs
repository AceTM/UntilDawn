using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.Win32;
using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;

public class PlayerPrefManager : MonoBehaviour {

	Resolution[] resolutions;

	public List<PlayerPreference> playerPrefs;

	private PlayerPreference newPref;
	private bool isCreatingNew;

	private void Awake () 
	{
		resolutions = Screen.resolutions;
		for (int i = Constants.ZERO; i < resolutions.Length; i++) {

		}
		playerPrefs = new List<PlayerPreference>();
		GetPrefKeysWindows ();
	}

	private string ResolutionToString (Resolution res) 
	{
		return res.width + Constants.RESO_TIME + res.height;
	}


	private void SaveAll() {
		for (int i = playerPrefs.Count - 1; i >= 0; i--) {
			PlayerPreference pref = playerPrefs[i];
			if (pref.isMarkedForDelete) {
				PlayerPrefs.DeleteKey(pref.name);
				playerPrefs.RemoveAt(i);
				continue;
			}
			switch (pref.value.type) {
			case PlayerPreference.PlayerPrefType.Int:
				PlayerPrefs.SetInt(pref.name, pref.value.intValue);
				break;
			case PlayerPreference.PlayerPrefType.Float:
				PlayerPrefs.SetFloat(pref.name, pref.value.floatValue);
				break;
			case PlayerPreference.PlayerPrefType.String:
				PlayerPrefs.SetString(pref.name, pref.value.stringValue);
				break;
			}
			pref.Save();
		}
	}

	private void RefreshPlayerPrefs() {
		if (playerPrefs != null) playerPrefs.Clear();
		playerPrefs = new List<PlayerPreference>();
		if (Application.platform == RuntimePlatform.WindowsPlayer) {
			GetPrefKeysWindows();
		} 
		else {
			GetPrefKeysMac();
		}
	}

	private void GetPrefKeysWindows () {
		// Unity stores prefs in the registry on Windows. 
		string regKey = Constants.WIN_REG + PlayerSettings.companyName + Constants.WIN_SLASH + PlayerSettings.productName;
		RegistryKey key = Registry.CurrentUser.OpenSubKey(regKey);
		foreach (string subkeyName in key.GetValueNames()) {
			string keyName = subkeyName.Substring (Constants.ZERO, subkeyName.LastIndexOf(Constants.UNDERSCORE));
			string val = key.GetValue(subkeyName).ToString();
			// getting the type of the key is not supported in Mono with registry yet :(
			// Have to infer type and guess...
			int testInt = - Constants.ONE;
			string newType = Constants.EMPTY;
			bool couldBeInt = int.TryParse(val, out testInt);
			if (!float.IsNaN (PlayerPrefs.GetFloat (keyName, float.NaN))) {
				val = PlayerPrefs.GetFloat (keyName).ToString();
				newType = Constants.REAL;
			} 
			else if (couldBeInt && (PlayerPrefs.GetInt (keyName, testInt - Constants.TEN) == testInt)) {		
				newType = Constants.INTEGER;		
			} 
			else {
				newType = Constants.STRING;
			}
			PlayerPreference pref = new PlayerPreference(keyName, newType, val);
			playerPrefs.Add (pref);
			Debug.Log (pref.name);
		}
	}
	
	private void GetPrefKeysMac() {
		string homePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		string pListPath = homePath + "/Library/Preferences/unity." + PlayerSettings.companyName + "." +
			PlayerSettings.productName + ".plist";
		// Convert from binary plist to xml.
		Process p = new Process();
		ProcessStartInfo psi = new ProcessStartInfo("plutil", "-convert xml1 \"" + pListPath + "\"");
		p.StartInfo = psi;
		p.Start();
		p.WaitForExit();
		
		StreamReader sr = new StreamReader(pListPath);
		string pListData = sr.ReadToEnd();
		
		XmlDocument xml = new XmlDocument();
		xml.LoadXml(pListData);
		
		XmlElement plist = xml["plist"];
		if (plist == null) return;
		XmlNode node = plist["dict"].FirstChild;
		while (node != null) {
			string name = node.InnerText;
			node = node.NextSibling;
			PlayerPreference pref = new PlayerPreference(name, node.Name, node.InnerText);
			node = node.NextSibling;
			playerPrefs.Add(pref);
		}
		// Convert plist back to binary
		Process.Start("plutil", " -convert binary1 \"" + pListPath + "\"");
	}
}
