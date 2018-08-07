using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICompass : MonoBehaviour {

	public GameObject compass;
	public GameObject player1;
	public GameObject player2;

	public static float SignedAngleBetween(Vector3 a, Vector3 b)
	{
		float angle = Vector3.Angle(a, b);
		Vector3 n = new Vector3(0, 1, 0);
		float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));
		float signed_angle = angle * sign;
		return (signed_angle <= 0) ? 360 + signed_angle : signed_angle;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 a = new Vector3(0, 1, 0);
		Vector3 b = new Vector3(player1.transform.position.x - player2.transform.position.x, player1.transform.position.y - player2.transform.position.y, player1.transform.position.z - player2.transform.position.z);

		compass.transform.Rotate(0, 0, SignedAngleBetween(a, b));
	}
}
