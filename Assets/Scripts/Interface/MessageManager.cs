﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MessageManager : MonoBehaviour {

	public Text messageTextBox;

	[Range(1, 20)]
	public int messageSpeed;
	private int messageFrameSkipped;

	private bool allowMessageSkip;
	private bool messageFlowFinish;

	private List<string> allMessages = new List<string>();
	private string currentMessage;
	private string displayMessage;
	private int messageIndex;
	private int messageCharacterIndex;

	private void Start ()
	{
		CheckTextEnabled ();
		ResetMessageFrameCounter ();
		ReadCSV ();
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

	private void OnMessageTap ()
	{
		if (Input.touchCount > Constants.ZERO) {
			GetNextMessage ();
		}
		else if (Input.GetMouseButtonDown(Constants.ZERO)) {
			GetNextMessage ();
		}
	}

	private void ReadCSV () 
	{
		List<Dictionary<string,object>> data = CSVReader.Read (Constants.EXAMPLE_CSV);
		for (int i = Constants.ZERO; i < data.Count; i++) {
			if (Application.loadedLevelName == (string)data[i][Constants.MES_SCENE])
				allMessages.Add((string)data[i][Constants.MES_JPTEXT]);
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

	private string MessageFlow () {
		if (!messageFlowFinish) {
			messageFrameSkipped += messageSpeed;
			if (messageFrameSkipped > Constants.MES_DISPLAY_COUNTER)
				messageFrameSkipped = Constants.MES_DISPLAY_COUNTER;
			
			if (messageFrameSkipped == Constants.MES_DISPLAY_COUNTER) {
				currentMessage = (string)allMessages[messageIndex];
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
			if (messageIndex + Constants.ONE < allMessages.Count){
				if (messageIndex + Constants.ONE < allMessages.Count) 
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