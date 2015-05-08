using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ConsoleLog : Singleton<MonoBehaviour> {

	public GameObject logObject;
	public Sprite assertIcon;
	public Sprite errorIcon;
	public Sprite exceptionIcon;
	public Sprite logIcon;
	public Sprite warningIcon;

	List<Log> logs = new List<Log>();

	private struct Log
	{
		public string message;
		public string stackTrace;
		public LogType type;
	}

//	static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>()
//	{
//		{ LogType.Assert, Color.white },
//		{ LogType.Error, Color.red },
//		{ LogType.Exception, Color.red },
//		{ LogType.Log, Color.white },
//		{ LogType.Warning, Color.yellow },
//	};
	
	static private ConsoleLog logMessageInstance = null;

	private GameObject logMessageWindow;
	private Text logMessageText;
	private Image logMessageIcon;
	private Button logMessageClose;
	private Button logMessageHistory;

	private void OnEnable ()
	{
		Application.logMessageReceived += HandleLog;
	}
	
	private void OnDisable ()
	{
		Application.logMessageReceived -= HandleLog;
	}

	private void HandleLog (string message, string stackTrace, LogType type)
	{
		logs.Add ( new Log() {
			message = message,
			stackTrace = stackTrace,
			type = type,
		});

		PrintLog(message, type);
	}

	private void Start ()
	{
		CheckInstanceExist ();
		SetupMessageComponents ();
	}

	private void Update () 
	{
		if (Input.GetKeyUp("a")) {
			Debug.Log ("Asserting some tests");
		}
		if (Input.GetKeyUp("c")) {
			Debug.LogError ("Critical error");
		}
		if (Input.GetKeyUp("l")) {
			Debug.Log ("I am awesome");
		}
		if (Input.GetKeyUp("w")) {
			Debug.LogWarning ("Might be careful");
		}
	}

	private void CheckInstanceExist ()
	{
		if (logMessageInstance != null) {
			Destroy(this);
		}
		logMessageInstance = this;
	}

	private void SetupMessageComponents ()
	{
		Transform canvasParent = GameObject.Find(ObjectConstants.OBJ_CANVAS).transform;
		if (canvasParent.GetComponent<Canvas>()) {
			logMessageWindow = GameObject.Instantiate(logObject);
			logMessageWindow.name = ObjectConstants.LOG_MES_WIN;
			logMessageWindow.transform.SetParent(canvasParent, false);

			Text[] logMessageTexts = logMessageWindow.GetComponentsInChildren<Text>();
			if (logMessageTexts.Length > Constants.ZERO) {
				foreach(Text text in logMessageTexts) {
					if (text.transform.name == ObjectConstants.LOG_MEX_TEX)
						logMessageText = text;
				}
			}

			Image[] logMessageImages = logMessageWindow.GetComponentsInChildren<Image>();
			if (logMessageImages.Length > Constants.ZERO) {
				foreach(Image image in logMessageImages) {
					if (image.transform.name == ObjectConstants.LOG_MES_ICO)
						logMessageIcon = image;
				}
			}

			Button[] logMessagebuttons = logMessageWindow.GetComponentsInChildren<Button>();
			if (logMessagebuttons.Length > Constants.ZERO) {
				foreach(Button button in logMessagebuttons) {
					if (button.transform.name == ObjectConstants.LOG_MES_CLO) {
						logMessageClose = button;
						logMessageClose.onClick.AddListener (() => OnClickCloseLogMessageWindow ());
					}
					else if (button.transform.name == ObjectConstants.LOG_MES_HIS) {
						logMessageHistory = button;
						logMessageHistory.onClick.AddListener (() => OnClickEnterHistory ());
					}
				}
			}
		}

		logMessageWindow.SetActive(false);
	}

	private void PrintLog (string message, LogType type) 
	{
		if (!logMessageWindow) {
			return;
		}

		if (!logMessageWindow.gameObject.activeSelf) {
			logMessageWindow.SetActive(true);
		}

		if (logMessageText) {
			logMessageText.text = message;
		}

		if (logMessageIcon) {
			switch (type) {
				case LogType.Assert: {
					logMessageIcon.sprite = assertIcon;
					break;
				}
				case LogType.Error: {
					logMessageIcon.sprite = errorIcon;
					break;
				}
				case LogType.Exception: {
					logMessageIcon.sprite = exceptionIcon;
					break;
				}
				case LogType.Log: {
					logMessageIcon.sprite = logIcon;
					break;
				}
				case LogType.Warning: {
					logMessageIcon.sprite = warningIcon;
					break;
				}
			}
		}
	}

	private void DestroyLogMessageWindow ()
	{
		if (logMessageWindow) {
			Destroy(logMessageWindow);
		}
	}

	public void OnClickCloseLogMessageWindow ()
	{
		logMessageWindow.SetActive(false);
	}

	public void OnClickEnterHistory ()
	{

	}
}
	