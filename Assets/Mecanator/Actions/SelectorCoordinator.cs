using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
//using AlphaECS;

public class SelectorCoordinator : StateMachineAction
{
	public List<string> Parameters;
	private Dictionary<string, int> ParameterTable = new Dictionary<string, int> ();

	private List<string> PreviousParameters = new List<string>();

    private void OnEnable()
    {
		for (var i = 0; i < Parameters.Count; i++)
		{
			ParameterTable.Add(Parameters[i], 0);
		}
    }

    public override void Execute(StateMachineActionObject smao)
    {
        base.Execute(smao);

		foreach (var key in Parameters)
		{
            ParameterTable[key] = smao.Animator.GetInteger(key);
		}

		var scoreGrouping = ParameterTable.GroupBy(kvp => kvp.Value).OrderByDescending(grouping => grouping.Key);
		var highScores = scoreGrouping.First();
		foreach (var kvp in highScores)
		{
			if (PreviousParameters.Contains(kvp.Key))
			{
				//move on...
			}
		}
    }
}
