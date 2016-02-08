using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tiled), true)]
public class TiledEditor : Editor {

	Tiled obj;

	public override void OnInspectorGUI () {

		obj = target as Tiled;
		base.OnInspectorGUI();

		/*GUILayout.BeginHorizontal();
		obj.fillOptions.left = GUILayout.Toggle(obj.fillOptions.left, "Left");
		obj.fillOptions.rigth = GUILayout.Toggle(obj.fillOptions.rigth, "Rigth");
		obj.fillOptions.bottom = GUILayout.Toggle(obj.fillOptions.bottom, "Bottom");
		obj.fillOptions.top = GUILayout.Toggle(obj.fillOptions.top, "Top");
		GUILayout.EndHorizontal();*/

		GUILayout.BeginHorizontal();
		/*if (GUILayout.Button("Fix All")) {
			fixGrig();
		}*/
		if (GUILayout.Button("Fix Grig")) {
			fixGrig();
		}
		/*if (GUILayout.Button("Fix Size")) {
		}*/
		GUILayout.EndHorizontal();

		SceneView.RepaintAll();
	}

	void fixGrig () {
		Vector3 pos = obj.transform.position;
		pos.x = Mathf.Round(pos.x);
		pos.y = Mathf.Round(pos.y);
		obj.transform.position = pos;
	}

}