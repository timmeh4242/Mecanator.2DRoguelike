using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetIntParameter), true)]
public class StateMachineActionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

		EditorGUI.BeginChangeCheck();

		this.serializedObject.Update();
		SerializedProperty Iterator = this.serializedObject.GetIterator();
		Iterator.NextVisible(true);

		while (Iterator.NextVisible(false))
		{
			EditorGUILayout.PropertyField(Iterator, true);
		}

		this.serializedObject.ApplyModifiedProperties();

		EditorGUI.EndChangeCheck();
	}
}
