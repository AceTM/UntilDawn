using UnityEngine;
using System.Collections;

public class ControlManager : Singleton<MonoBehaviour> {
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
	
}
