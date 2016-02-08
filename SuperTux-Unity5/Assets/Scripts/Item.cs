using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public void anim_onExit () {
		GetComponent<Animator>().enabled = false;
	}

}
