using UnityEngine;
using System.Collections;

public class TransformHandler {

	public static void SetX(Transform transform, float x)
	{
		Vector3 newPosition = new Vector3(x, transform.position.y, transform.position.z);
		transform.position = newPosition;
	}
}
