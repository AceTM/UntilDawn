using UnityEngine;
using System.Collections;

public class Delegates : Singleton<MonoBehaviour> {

//	public delegate void CommentMethod(string text);
//	
//	void Start() 
//	{
//		
//		CommentMethod cm = Say;
//
//		WriteText(cm, string.Empty); 
//		WriteText(cm, "First Comment");
//		WriteText(cm, "Second comment");
//	}
//	
//	public void Say(string comment) 
//	{
//		Debug.Log(comment);
//	}
//	
//	public void WriteText(CommentMethod method, string comment) 
//	{
//		Debug.Log("START");
//		method(comment);
//		Debug.Log("END");
//	}

	private delegate void UICall();
	private UICall currentUiCall;

	void Start () 
	{ 
		// start with the main menu GUI
		this.currentUiCall = MessageBox; 
	} 

	public void Update ()
	{
		this.currentUiCall ();
	}

	public void MessageBox () 
	{ 

	} 

	private void OptionsMenu()
	{

	}    

}
