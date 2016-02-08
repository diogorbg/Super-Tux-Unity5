using UnityEngine;
using System.Collections;

public class Ground : Tiled {

	public Vector2 boxSize;
	public Vector2 boxOffset;

	private BoxCollider2D box;

	void Start () {
		box = gameObject.AddComponent<BoxCollider2D>();
		boxSize.x /= transform.localScale.x;
		boxSize.y /= transform.localScale.y;
		box.size += boxSize;
		boxOffset.x /= transform.localScale.x;
		boxOffset.y /= transform.localScale.y;
		box.offset += boxOffset;
	}

}
