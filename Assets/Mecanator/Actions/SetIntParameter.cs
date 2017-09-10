using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[System.Serializable]
public class SetIntParameter : StateMachineAction
{
	public string ParameterName;
	public int Value;
    public IntReactiveProperty blah = new IntReactiveProperty();

	private int ParameterHash;

    private void OnEnable()
    {
       ParameterHash = Animator.StringToHash(ParameterName); 
    }

    public override void Execute (StateMachineActionObject smao)
	{
		smao.Animator.SetInteger (ParameterHash, Value);
	}
}
