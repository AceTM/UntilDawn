using UnityEngine;
using System;
using System.Collections;

public class Game : Singleton<MonoBehaviour> {

	public GameManager GameManager = new GameManager();
	
	void Awake()
	{
		// Standard events are faster
		GameManager.OnGameStart += OnStart;

		//raises events
		GameManager.StartGame();
		gameState = GameState.Uncontrolable;
	}

	void OnStart()
	{
		// should be called twice
		Debug.Log("Hello World");
	}



	public enum GameState {
		Uncontrolable,
		Menu,
		Message,
		Gameplay
	}
	public GameState gameState;
	
	private delegate void CallMode ();
	private CallMode currentMode;

	private void ModeHandler ()
	{
		currentMode ();
	}
	
	private void MessageMode ()
	{
		
	}
	
	private void UncontrolableMode ()
	{
		
	}
	
	private void MenuMode () 
	{
		
	}
	
	private void GameplayMode ()
	{
		
	}
	
	public void ToggleMode (GameState state)
	{
		switch (gameState) {
		case GameState.Uncontrolable:
			currentMode = UncontrolableMode;
			break;
		case GameState.Menu:
			currentMode = MenuMode;
			break;
		case GameState.Message:
			currentMode = MessageMode;
			break;
		case GameState.Gameplay:
			currentMode = GameplayMode;
			break;
		}
	}
}
