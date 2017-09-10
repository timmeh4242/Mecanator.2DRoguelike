using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetIntParameter), true)]
public class SetIntParameterEditor : StateMachineActionEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
	}
}
