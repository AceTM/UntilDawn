using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class MessageManager : Singleton<MonoBehaviour> {

	public Text messageTextBox;
	public bool messageOver;

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



	private void OnEnable ()
	{
		MessageStart ();
		CheckTextEnabled ();
		CreateList();
		ResetMessageFrameCounter ();
		ReadMessagesFromCSV (Constants.MF_A01);
	}

	private void OnDisable ()
	{
		DisableMessage ();
	}

	private void Update ()
	{
		MessageFlow ();
		MessageDisplay ();
		OnMessageTap ();
	}

	private void MessageStart ()
	{
		messageOver = false;
	}

	private void MessageEnd ()
	{
		messageOver = true;
	}

	private void CheckTextEnabled ()
	{
		if (!messageTextBox) {
			if (GameObject.Find(Constants.MES_TEXT_BOX)) 
				messageTextBox = GameObject.Find(Constants.MES_TEXT_BOX).GetComponent<Text>();
			else
				Destroy(this.gameObject);
		}
	}

	private void CreateList () {
		messagesList = new List<DialogMessage>();
	}


	private void ReadMessagesFromCSV (string messageFileName) 
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

	private void DisplayFullMessage () {
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

	private void GetNextMessage () {
		if (displayMessage.Length == currentMessage.Length) {
			if (messageIndex + Constants.ONE < messagesList.Count){
				if (messageIndex + Constants.ONE < messagesList.Count) 
					messageIndex++;
				ResetMessageFrameCounter ();
				ResetMessageIndex ();
			}
			else {
				MessageEnd ();
				Debug.Log ("Finished all messages");
			}
		}
		else 
			DisplayFullMessage ();
	}

	private void DisableMessage () 
	{
		
	}
}
