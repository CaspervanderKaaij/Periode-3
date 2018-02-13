using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{

	public Vector3 rotateV3;
	public bool local = false;

	void Update ()
	{
		if (local == false) {
			transform.eulerAngles += rotateV3 * Time.deltaTime;
		} else {
			transform.Rotate(rotateV3 * Time.unscaledDeltaTime);
		}
	}
}
