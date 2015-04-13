using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour {
	enum ControlTypes {
		PointAndClick,
		ThirdPerson,
		FirstPerson
	}
	ControlTypes controlType;

	enum ControllerDeviceTypes {
		XboxController,
		SonyController,
		Keyboard,
		MobileTouch,
	}
	ControllerDeviceTypes controllerType;

	private static ControlManager controlInstance;
	
	public static ControlManager Instance
	{
		get {
			if(controlInstance == null) {
				controlInstance = GameObject.FindObjectOfType<ControlManager>();				
				DontDestroyOnLoad(controlInstance.gameObject);
			}
			return controlInstance;
		}
	}
	
	void Awake() 
	{
		if(controlInstance == null) {
			controlInstance = this;
			DontDestroyOnLoad(this);
		}
		else {
			if(this != controlInstance)
				Destroy(this.gameObject);
		}
	}


}
