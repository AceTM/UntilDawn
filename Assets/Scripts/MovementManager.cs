using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour {
	
	public float moveDistance;
	public float moveSpeed;
	public float turnSmoothing;
	public float speedDampTime = 0.1f;
	
//	private Animator animator;
	private AnimationHash hash;
	private LayerMask presentLayer;
	
	public GameObject mainCamera;
	
	private float xAxis;
	private float yAxis;

	private static MovementManager movementInstance;

	public static MovementManager Instance
	{
		get {
			if(movementInstance == null) {
				movementInstance = GameObject.FindObjectOfType<MovementManager>();				
				DontDestroyOnLoad(movementInstance.gameObject);
			}
			return movementInstance;
		}
	}
	
	void Awake() 
	{
		if(movementInstance == null) {
			movementInstance = this;
			DontDestroyOnLoad(this);
		}
		else {
			if(this != movementInstance)
				Destroy(this.gameObject);
		}
	}

	void Start () {
//		animator = GetComponent<Animator>();
	}

	public void MovementTransition (float horizontal, float vertical) {
		if(horizontal > 0.2f || vertical > 0.2f || horizontal < - 0.2f || vertical <  - 0.2f) {
			MovementRotation (horizontal, vertical);
		}
	}
	
	void MovementRotation (float horizontal, float vertical) {
//		if (animator.GetCurrentAnimatorStateInfo(0).nameHash == hash.runningSlow) {
		Vector3 targetDirection = mainCamera.transform.rotation * new Vector3(horizontal, 0f, vertical);
		targetDirection = new Vector3(targetDirection.x, 0f, targetDirection.z);
		targetDirection.Normalize();
		
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
		transform.rotation = newRotation;
//		}
	}


}
