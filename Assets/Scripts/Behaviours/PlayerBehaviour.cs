using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerStates {
	Idle = 0, 
	MoveForward = 1, 
	Turn = 2, 
	MoveDown = 3, 
	MoveUp = 4, 
	Dead = 6, 
	StartPickingUp = 5, 
	PushOffWall = 9, 
	PushOffGround = 8
}

public class PlayerBehaviour : GameBehaviour {

	#region variables	

	#endregion
	
	#region implementation
	/// <summary>Start this instance.</summary>
	protected override void Start ()
	{
		base.Start();
	}
	
	/// <summary>Games the update.</summary>
	protected override void GameUpdate ()
	{
		ProcessMove();
	}
	
	/// <summary>Processes the move.</summary>
	private void ProcessMove()
	{

	}

	
	/// <summary>Raises the collistion enter event.</summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter( Collision collision )
	{

	}
	
	/// <summary>Takes the object.</summary>
	/// <param name="go">Go.</param>
	public void ProcessTakingObject ()
	{

	}
	
	/// <summary>Die this instance.</summary>
	protected virtual void Die ()
	{

	}
	
	#endregion
	
	#region properties		
	/// <summary>Gets or sets the state of the move.</summary>
	/// <value>The state of the move.</value>

	#endregion
}
