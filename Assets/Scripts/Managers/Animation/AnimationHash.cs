using UnityEngine;
using System.Collections;

public class AnimationHash : MonoBehaviour {

	public int runningSlow;
	public int runningFast;
	public int runningFastest;
	
	public int idle;

	public int speed;			//Float type
	public int moving;			//Bool type
	
	void Awake () {
		runningSlow = Animator.StringToHash("Base Layer.Running Slow");
		runningFast = Animator.StringToHash("Base Layer.Running Fast");
		runningFastest = Animator.StringToHash("Base Layer.Running Fastest");
		
		idle = Animator.StringToHash("Base Layer.Idle");
		
		speed = Animator.StringToHash("Speed");
		
	}
}
