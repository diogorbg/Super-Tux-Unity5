using UnityEngine;
using System.Collections;

public class Bloco : MonoBehaviour {

	public void anim_onExit () {
		GetComponent<Animator>().enabled = false;
	}

}
