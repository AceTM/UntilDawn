using UnityEngine;
using System.Collections;

public class StateBase {

	public enum GameState {
		Default,
		Uncontrolable,
		Menu,
		Message,
		Gameplay,
		Pause,
		Entering,
		Exiting
	}

	public virtual string StateName { 
		get {
			return "Default"; 
		} 
	}

	protected GameState mCurrentState;

	protected SpriteRenderer mFadeInOutSprite;
	
	private float mAudioFadeoutSpeed; 

	private void Pause()
	{
		Time.timeScale = 0.0f;
		mFadeInOutSprite.gameObject.SetActive(true);
		mFadeInOutSprite.color = new Color( 1f, 1f, 1f, 1f );
	}

	private void UnPause()
	{
		Time.timeScale = 1.0f;
		mFadeInOutSprite.gameObject.SetActive(false);
		mFadeInOutSprite.color = new Color( 1f, 1f, 1f, 0f );
	}

	public virtual void OnUIAction( GameObject sender, Object data )
	{
		
	}

	public virtual void Awake()
	{
		SetAudioListenerVolume();
	}

	public virtual void Start()
	{
		mFadeInOutSprite = GameObject.Find("FadeInOutPlane").GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		mFadeInOutSprite.enabled = true;
		Debug.Log( "Start " + this );
	}

	public virtual void Update()
	{
		if ( State == GameState.Entering ) {	
			float alpha = Mathf.MoveTowards( mFadeInOutSprite.color.a, 0f, 0.15f * Time.deltaTime );
			mFadeInOutSprite.color = new Color( 1f, 1f, 1f, alpha );
			
			if ( mFadeInOutSprite.color.a == 0 ) {
				mFadeInOutSprite.gameObject.SetActive(false);
				State = GameState.Default;
			}
		}
		if ( State == GameState.Exiting ) {
			float alpha = Mathf.MoveTowards( mFadeInOutSprite.color.a, 1f, 0.3f * Time.deltaTime );
			AudioListener.volume = Mathf.MoveTowards( AudioListener.volume, 0f, mAudioFadeoutSpeed * Time.deltaTime );
			mFadeInOutSprite.color = new Color( 1f, 1f, 1f, alpha );
			
			if ( mFadeInOutSprite.color.a == 1f ) {
				GameRoot.Instance.LoadNextLevel();
			}
		}
		DoInput();
	}

	protected virtual void SetCursor()
	{
		if ( GameRoot.Instance.ControllerType == ControllerType.Controller ) {
			Cursor.visible = false;
		}
		else if ( GameRoot.Instance.ControllerType == ControllerType.MouseTouch ) {
			Cursor.visible = true;
		}
	}

	protected virtual void SetCursor(bool cursor)
	{
		Cursor.visible = cursor;
	}

	protected virtual void DoInput()
	{
		
	}

	public override string ToString ()
	{
		return string.Format ("[StateBase: Name={0}, State={1}]", StateName, State);
	}	

	protected void SetAudioListenerVolume()
	{
		AudioListener.volume = 1f;
	}

	#region properties
	public GameState State
	{
		get {
			return mCurrentState;
		}
		set {
			if ( value == GameState.Pause ) {
				Pause();
			}
			else if ( mCurrentState == GameState.Pause && value != GameState.Pause ) {
				UnPause();
			}
			else if ( value == GameState.Exiting ) {
				mFadeInOutSprite.gameObject.SetActive(true);
				SetCursor( false );
				mAudioFadeoutSpeed = 0.35f / ( 1f - mFadeInOutSprite.color.a ) / AudioListener.volume;
			}
			mCurrentState = value;
			Debug.Log( this );
		}
	}
	#endregion
}
