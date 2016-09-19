using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public Vector2 vel;

	void Update () {
		float dt = Time.deltaTime;
		if (Input.GetKey(KeyCode.RightArrow))
			transform.position += vel.x * Vector3.right * dt;
		else if (Input.GetKey(KeyCode.LeftArrow))
			transform.position += vel.x * Vector3.left * dt;

		if (Input.GetKey(KeyCode.UpArrow))
			transform.position += vel.y * Vector3.up * dt;
		else if (Input.GetKey(KeyCode.DownArrow))
			transform.position += vel.y * Vector3.down * dt;
	}

	//public

}
