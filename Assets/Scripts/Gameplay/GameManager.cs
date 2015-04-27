using UnityEngine;
using System.Collections;

public class GameManager : Singleton<MonoBehaviour> {
	public enum GameState {
		Uncontrolable,
		Menu,
		Message,
		Gameplay
	}
	public GameState gameState;

	private delegate void CallMode ();
	private CallMode currentMode;

	private void Awake ()
	{
		gameState = GameState.Uncontrolable;
	}

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
