using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static GameManager gameInstance;
	
	public static GameManager Instance
	{
		get {
			if(gameInstance == null) {
				gameInstance = GameObject.FindObjectOfType<GameManager>();				
				DontDestroyOnLoad(gameInstance.gameObject);
			}
			return gameInstance;
		}
	}

	void Awake() 
	{
		if(gameInstance == null) {
			gameInstance = this;
			DontDestroyOnLoad(this);
		}
		else {
			if(this != gameInstance)
				Destroy(this.gameObject);
		}
	}
}
