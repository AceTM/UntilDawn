using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class MessageManager : MonoBehaviour {

	public Text messageTextBox;

	[Range(1, 20)]
	public int messageSpeed;
	private int messageFrameSkipped;

	private bool allowMessageSkip;
	private bool messageFlowFinish;

	public List<DialogMessage> messagesList;
	private string currentMessage;
	private string displayMessage;
	private int messageIndex;
	private int messageCharacterIndex;

	public class DialogMessage {
		public int 		Id { get; set; }
		public string 	Character { get; set; }
		public bool 	Skipable { get; set; }
		public bool 	Voiced { get; set; }
		public bool 	Left { get; set; }
		public string 	JpText { get; set; }
		public string 	EnText { get; set; }
	}

	private static MessageManager messageInstance;
	
	public static MessageManager instance
	{
		get {
			if(messageInstance == null) {
				messageInstance = GameObject.FindObjectOfType<MessageManager>();
				DontDestroyOnLoad(messageInstance.gameObject);
			}
			return messageInstance;
		}
	}
	
	void Awake() 
	{
		if(messageInstance == null) {
			messageInstance = this;
			DontDestroyOnLoad(this);
		}
		else {
			if(this != messageInstance)
				Destroy(this.gameObject);
		}
	}

	private void OnEnable ()
	{
		CheckTextEnabled ();
		CreateList();
		ResetMessageFrameCounter ();
		ReadMessagesFromCSV (Constants.MF_A01);
	}

	private void Update ()
	{
		MessageFlow ();
		MessageDisplay ();
		OnMessageTap ();
	}

	private void CheckTextEnabled ()
	{
		if (messageTextBox == null) {
			if (GameObject.Find(Constants.MES_TEXT_BOX)) 
				messageTextBox = GameObject.Find(Constants.MES_TEXT_BOX).GetComponent<Text>();
			else
				Destroy(this.gameObject);
		}
	}

	private void CreateList () {
		messagesList = new List<DialogMessage>();
	}


	public void ReadMessagesFromCSV (string messageFileName) 
	{
		List<Dictionary<string,object>> data = CSVReader.Read (messageFileName);
		for (int i = Constants.ZERO; i < data.Count; i++) {
			if (Application.loadedLevelName == (string)data[i][Constants.MES_SCENE]) {
				messagesList.Add( new DialogMessage () {
					Id 			= (int)data[i][Constants.MES_ID],
					Character 	= (string)data[i][Constants.MES_CHARACTER],
					Skipable 	= bool.Parse((string)data[i][Constants.MES_SKIPABLE]),
					Voiced 		= bool.Parse((string)data[i][Constants.MES_VOICED]),
					Left 		= bool.Parse((string)data[i][Constants.MES_LEFT]),
					JpText 		= (string)data[i][Constants.MES_JPTEXT],
					EnText 		= (string)data[i][Constants.MES_ENTEXT]
				});
			}
		}
	}

	private void MessageDisplay () {
		messageTextBox.text = displayMessage;
	}

	private void ResetMessageFrameCounter ()
	{
		messageFrameSkipped = Constants.ZERO;
	}
	
	private void ResetMessageIndex () 
	{
		messageCharacterIndex = Constants.ZERO;
		messageFlowFinish = false;
	}

	public void DisplayFullMessage () {
		messageFlowFinish = true;	
		messageCharacterIndex = currentMessage.Length;
		displayMessage = currentMessage;
	}

	private void OnMessageTap ()
	{
		if (Input.touchCount > Constants.ZERO) {
			GetNextMessage ();
		}
		else if (Input.GetMouseButtonDown(Constants.ZERO)) {
			GetNextMessage ();
		}
	}

	private string MessageFlow () {
		if (!messageFlowFinish) {
			messageFrameSkipped += messageSpeed;
			if (messageFrameSkipped > Constants.MES_DISPLAY_COUNTER)
				messageFrameSkipped = Constants.MES_DISPLAY_COUNTER;
			
			if (messageFrameSkipped == Constants.MES_DISPLAY_COUNTER) {
				currentMessage = (string)messagesList[messageIndex].JpText;
				displayMessage = currentMessage.Substring(Constants.ZERO, messageCharacterIndex);
				if (messageCharacterIndex < currentMessage.Length)
					messageCharacterIndex++;
				ResetMessageFrameCounter ();
			}
		}
		return displayMessage;
	}

	public void GetNextMessage () {
		if (displayMessage.Length == currentMessage.Length) {
			if (messageIndex + Constants.ONE < messagesList.Count){
				if (messageIndex + Constants.ONE < messagesList.Count) 
					messageIndex++;
				ResetMessageFrameCounter ();
				ResetMessageIndex ();
				
			}
			else 
				Debug.Log ("Finished all messages");
		}
		else 
			DisplayFullMessage ();
	}
}
