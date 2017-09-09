using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineAction : ScriptableObject
{
    public virtual void Execute (StateMachineActionObject stateMachineActionObject){}

    private void OnEnable()
    {
		//hideFlags = HideFlags.HideAndDontSave;
	}
}
