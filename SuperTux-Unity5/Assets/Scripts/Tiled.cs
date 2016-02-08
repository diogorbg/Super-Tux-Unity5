using UnityEngine;
using System.Collections;

public class Tiled : MonoBehaviour {

	[System.Serializable]
	public struct FillOptions {
		public bool left;
		public bool rigth;
		public bool bottom;
		public bool top;

		public FillOptions (bool lf=true, bool rt=true, bool bt=true, bool tp=true) {
			left = lf;
			rigth = rt;
			bottom = bt;
			top = tp;
		}
	}
	//[HideInInspector, SerializeField]
	public FillOptions fillOptions = new FillOptions(true, true, true, true);

	[HideInInspector] public Vector4 borda = Vector4.zero;
	[HideInInspector] public Vector2 size = Vector2.zero;
	[HideInInspector] public Vector2 min, max;

	private SpriteRenderer _render;
	public SpriteRenderer render {
		get {
			if (_render==null) {
				_render = GetComponent<SpriteRenderer>();
			}
			return _render;
		}
	}

	private MaterialPropertyBlock _mat;
	public MaterialPropertyBlock mat {
		get {
			if (_mat==null) {
				_mat = new MaterialPropertyBlock();
				render.GetPropertyBlock(_mat);
			}
			return _mat;
		}
	}

	public Sprite sprite {
		get {
			return render.sprite;
		}
	}

	void Awake () {
		calcVetores();

		Vector4 borda = this.borda;
		borda.x /= sprite.bounds.size.x;
		borda.y /= sprite.bounds.size.y;
		borda.z /= sprite.bounds.size.x;
		borda.w /= sprite.bounds.size.y;
		mat.SetVector("_border", borda);

		Vector4 minMax = new Vector4(min.x/size.x, min.y/size.y, max.x/size.x, max.y/size.y);
		mat.SetVector("_minMax", minMax);

		Vector4 escala = new Vector4(transform.localScale.x, transform.localScale.y, 0f, 0f);
		mat.SetVector("_scale", escala);

		render.SetPropertyBlock(mat);
	}

#region UNITY_EDITOR ----------------------------------------------------------------------------------------------
#if UNITY_EDITOR
	Sprite oldSprite = null;
	void OnValidate () {
		if (oldSprite==null || sprite!=oldSprite) {
			_mat = null;
		}
		Awake();
		oldSprite = sprite;
	}

	void calcVetores () {
		borda = sprite.border / sprite.pixelsPerUnit;
		Vector4 aux = borda;
		borda.z = sprite.bounds.size.x - borda.z;
		borda.w = sprite.bounds.size.y - borda.w;

		size = sprite.bounds.size;
		size.Scale(transform.localScale);

		min = new Vector2(borda.x, borda.y);
		max = new Vector2(size.x-aux.z, size.y-aux.w);
		if (!fillOptions.left)
			min.x = 0f;
		if (!fillOptions.rigth)
			max.x = size.x;
		if (!fillOptions.bottom)
			min.y = 0f;
		if (!fillOptions.top)
			max.y = size.y;
	}

	void OnDrawGizmosSelected () {
		OnValidate();

		Vector3 p1, p2;
		Gizmos.color = Color.gray;
		p1 = p2 = transform.position;
		p2.y += size.y;
		if (fillOptions.left) {
			p1.x = p2.x = min.x + transform.position.x;
			Gizmos.DrawLine(p1, p2);
		}
		if (fillOptions.rigth) {
			p1.x = p2.x = max.x + transform.position.x;
			Gizmos.DrawLine(p1, p2);
		}

		p1 = p2 = transform.position;
		p2.x += size.x;
		if (fillOptions.bottom) {
			p1.y = p2.y = min.y + transform.position.y;
			Gizmos.DrawLine(p1, p2);
		}
		if (fillOptions.top) {
			p1.y = p2.y = max.y + transform.position.y;
			Gizmos.DrawLine(p1, p2);
		}
	}
#endif
#endregion

}
