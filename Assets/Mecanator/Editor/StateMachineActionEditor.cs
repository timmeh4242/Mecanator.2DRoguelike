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
        var iterator = this.serializedObject.GetIterator();
		iterator.NextVisible(true);

		while (iterator.NextVisible(false))
		{
			EditorGUILayout.PropertyField(iterator, true);
		}

		this.serializedObject.ApplyModifiedProperties();

		EditorGUI.EndChangeCheck();
	}
}
