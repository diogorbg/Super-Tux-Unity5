using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public Vector2 vel;

	void Update () {
		moverTeclado ();
		moverMouse ();
	}

	public void moverTeclado () {
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

	public void moverMouse () {
		float dt = Time.deltaTime;
	
		if (!Input.GetKey (KeyCode.Mouse0))
			return;
	
		Vector3 p = Input.mousePosition;
		p.x -= Screen.width / 2;
		p.y -= Screen.height / 2;
		transform.position +=
			Vector3.right * Mathf.Cos(Mathf.Atan2(p.y, p.x)) * vel.x * dt +
			Vector3.up * Mathf.Sin(Mathf.Atan2(p.y, p.x)) * vel.y * dt;
	}

}
