using UnityEngine;
using System.Collections;

public class StateLevel : StateBase {
	
	public override string StateName { 
		get {
			return "Level"; 
		}
	}

	public override void Awake ()
	{
		base.Awake ();
		
//		SceneRoot.Instance.MainHero = new Hero();
//		SceneRoot.Instance.SceneThresures = new Thresure();
//		SceneRoot.Instance.SceneActors = new Actors();
//		
//		SceneRoot.Instance.MainHero.Awake();
//		SceneRoot.Instance.SceneThresures.Awake();
//		SceneRoot.Instance.SceneActors.Awake();
		
		SetCursor();
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	public override void Update ()
	{
		base.Update ();
		
//		SceneRoot.Instance.MainHero.rigidbody.AddForce( Random.insideUnitSphere * floatingForce );
//		
//		Camera.main.transform.position = Vector3.Lerp( Camera.main.transform.position, 
//		                                              new Vector3( SceneRoot.Instance.MainHero.position.x, Camera.main.transform.position.y, SceneRoot.Instance.MainHero.position.z ),
//		                                              Constants.CameraFoollowHeroSpeed * Time.deltaTime );
	}
	
	/// <summary>
	/// Dos the input.
	/// </summary>
	protected override void DoInput ()
	{
		base.DoInput ();
		
//		if ( Input.GetButtonDown("Exit")) {
//			Time.timeScale = 1f;
//			Application.LoadLevel(0);							
//		}
//		else if ( Input.GetButtonDown( "Pause" )) {
//			if ( State == ESubState.Default ) {
//				State = ESubState.Pause;
//			}
//			else if ( State == ESubState.Pause ) {
//				State = ESubState.Default;
//			}
//		}
//		
//		//
//		if ( SceneRoot.Instance.ControllerType == EControllerType.MouseTouch ) {
//			if ( Input.GetMouseButton(0)) {
//				Vector3 dir = Camera.main.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y ));
//				
//				dir.y = SceneRoot.Instance.MainHero.position.y;
//				dir = dir - SceneRoot.Instance.MainHero.position;
//				
//				SceneRoot.Instance.Iteract(IteractionArgs.EIteractions.Sweem, dir.normalized );
//			}
//		}
//		else if ( SceneRoot.Instance.ControllerType == EControllerType.Joystic ) {
//			//Move
//			Vector3 dir = new Vector3( Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical") ).normalized;
//			
//			if ( dir.magnitude > 0f ) {
//				SceneRoot.Instance.Iteract(IteractionArgs.EIteractions.Sweem, dir.normalized );
//			}
//		}
		
	}
}