using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCamera : MonoBehaviour {


	public Transform target;
	public float smoothMouv = 0.125f;
	public bool fallow = false;
	public Vector3 offset;
	void FixedUpdate()
	{
		if (fallow)
		{
			Vector3 goalPosition = target.position + offset;
			Vector3 smoothPos = Vector3.Lerp(transform.position, goalPosition, smoothMouv);
			transform.position = smoothPos;
			transform.LookAt(target);
		}
	}
}
