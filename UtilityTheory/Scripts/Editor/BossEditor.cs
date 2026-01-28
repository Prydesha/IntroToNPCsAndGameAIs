using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Boss))]
public class BossEditor : Editor
{
	private Boss _boss;

	private void OnEnable()
	{
		_boss = target as Boss;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (Application.isPlaying)
		{
			GUI.enabled = false;
			EditorGUILayout.PrefixLabel("Utility Calculations");
			EditorGUILayout.ObjectField(new GUIContent("Current Action"), _boss.CurrentAction, typeof(Action), true);
			EditorGUILayout.Space();
			EditorGUILayout.PrefixLabel("Actions");
			foreach (var ac in _boss.Actions)
			{
				EditorGUILayout.FloatField(new GUIContent(ac.name), ac.lastCalculatedUtility);
			}
			GUI.enabled = true;
		
		}
	}
}