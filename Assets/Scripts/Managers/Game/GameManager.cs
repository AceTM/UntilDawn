using System;
using System.Collections.Generic;
using System.Collections;

public class GameManager{

	public event Action OnGameStart;
	
	public List<Delegate> Subscriptions = new List<Delegate>();
	
	public void StartGame()
	{
		foreach (Delegate subscription in Subscriptions) {
			subscription.DynamicInvoke();
		}
		OnGameStart();
	}
}
