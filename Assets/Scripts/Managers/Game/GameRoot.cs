using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ControllerType { 
	MouseTouch, 
	Controller 
};

public partial class GameRoot : Singleton<GameRoot>  {

//	private Hero mHero;
//	private Actors mActors;	
//	private Thresure mThresures; 

	//States
	private List<StateBase> mStates;
	private StateBase mCurrentState;
	private float mStartExitingTime;
	private string mLoadingLevel;

	public void LevelComplete()
	{
		LevelToLoad = GetNextLevelName();
		
		int progress = PlayerPrefs.GetInt( Constants.LEVELS_PASSED, 0 );
		
		int nextLevel = GetLevel() + 1;
		
		if( nextLevel > progress ) {
			PlayerPrefs.SetInt( Constants.LEVELS_PASSED, nextLevel );		
		}
		
		mCurrentState.State = StateBase.GameState.Exiting;
	}

	/// <summary> Raises the user interface action event.</summary>
	/// <param name="sender">Sender.</param>
	/// <param name="data">Data.</param>
	public void OnUIAction( GameObject sender, Object data )
	{
		mCurrentState.OnUIAction( sender, data );
	}

	void Update () 
	{
		DoInput();
		mCurrentState.Update();
	}

	private void DoInput()
	{
		
	}

	void Awake()
	{
		mStates = new List<StateBase>();
//		mStates.Add( new StateMenu() );
//		mStates.Add( new StateEnd() );

		// Change this logic, definitely
		foreach ( StateBase sb in mStates ) {
			if ( sb.StateName == Application.loadedLevelName ) {
				mCurrentState = sb;
			}
		}
		
		if ( mCurrentState == null ) {
			mCurrentState = new StateLevel();
		}
		
		mCurrentState.Awake();
	}
	
	/// <summary>Gets the level.</summary>
	/// <returns>The level.</returns>
	public static int GetLevel()
	{
		foreach( KeyValuePair<int, string[]> kvp in Constants.LEVELS ) {
			foreach ( string s in kvp.Value ) {
				if ( Application.loadedLevelName == s ) {
					return kvp.Key;
				}
			}		
		}
		return -1;
	}
	
	/// <summary>Gets the random name of the level./summary>
	/// <returns>The random level name.</returns>
	/// <param name="level">Level.</param>
	public string GetRandomLevelName(int level)
	{
		string[] levels = Constants.LEVELS[ level ];
		int rnd = UnityEngine.Random.Range( 0, levels.Length );
		return levels[rnd];
	}
	
	/// <summary>Gets the name of the next level./summary>
	/// <returns>The next level name.</returns>
	public string GetNextLevelName()
	{
		int currentLevel = GetLevel();
		int nextLevel = currentLevel + 1;
		
		return GetRandomLevelName( nextLevel );
	}
	
	/// <summary>Games the over.</summary>
	public void GameOver()
	{
		LevelToLoad = GetRandomLevelName( GetLevel() );
		mCurrentState.State = StateBase.GameState.Exiting;
	}
	
	/// <summary>Loads the level.</summary>
	/// <param name="level">Level.</param>
	public void LoadLevel( int level )
	{
		Application.LoadLevel( level );
	}
	
	/// <summary>Loads the level.</summary>
	/// <param name="level">Level.</param>
	public void LoadLevel( string levelName )
	{
		LevelToLoad = levelName;
		Application.LoadLevel(levelName);
	}
	
	/// <summary>Loads the next level.</summary>
	public void LoadNextLevel()
	{
		LoadLevel( LevelToLoad );
	}


	#region properties
	/// <summary>Gets the type of the controller.</summary>
	/// <value>The type of the controller.</value>
	public ControllerType ControllerType
	{
		get {
			return (ControllerType) PlayerPrefs.GetInt( Constants.CONTROLLER, 0 );
		}
	}
	
	/// <summary>Gets or sets the level to load.</summary>
	/// <value>The level to load.</value>
	public string LevelToLoad 
	{
		get {
			return mLoadingLevel;
		}
		set {
			mLoadingLevel = value;
		}
	}
	
	/// <summary>Gets or sets the main hero.</summary>
	/// <value>The main hero.</value>
//	public Hero MainHero
//	{
//		get {
//			return mHero;
//		}
//		set {
//			mHero = value;
//		}
//	}
	
	/// <summary>Gets or sets the main hero.</summary>
	/// <value>The main hero.</value>
//	public Actors SceneActors
//	{
//		get {
//			return mActors;
//		}
//		set {
//			mActors = value;
//		}
//	}
	
	/// <summary>Gets or sets the scene robot interests.</summary>
	/// <value>The scene robot interests.</value>
//	public Thresure SceneThresures
//	{
//		get {
//			return mThresures;
//		}
//		set {
//			mThresures = value;
//		}
//	}
	
	/// <summary>Sets the state of the scene.</summary>
	/// <value>The state of the scene.</value>
	public StateBase SceneState
	{
		get {
			return mCurrentState;
		}
	}
	#endregion
}
