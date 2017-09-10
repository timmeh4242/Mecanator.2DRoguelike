using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AlphaECS;
using System;
using System.Linq;
using UniRx;

[CustomEditor(typeof(StateMachineHandler), true)]
public class StateMachineHandlerEditor : Editor
{
	private StateMachineHandler handler;

	private bool showActions = true;

	private readonly IEnumerable<Type> allComponentTypes = AppDomain.CurrentDomain.GetAssemblies()
						.SelectMany(s => s.GetTypes())
						.Where(p => typeof(StateMachineAction).IsAssignableFrom(p) && p.IsClass);

    //private SerializedProperty actions;

	void OnEnable()
	{
		//hideFlags = HideFlags.HideAndDontSave;

		if (handler == null)
		{ handler = (StateMachineHandler)target; }

        //if (actions == null && serializedObject.FindProperty("Actions") != null)
        //{ actions = serializedObject.FindProperty("Actions"); }
	}

	public override void OnInspectorGUI()
	{
		if (handler == null)
		{ handler = (StateMachineHandler)target; }

		//if (actions == null && serializedObject.FindProperty("Actions") != null)
		//{ actions = serializedObject.FindProperty("Actions"); }

		base.OnInspectorGUI();

		if (handler == null) { return; }
        //if (actions == null) { return; }

		serializedObject.Update();
		Undo.RecordObject(handler, "Added Action");

		DrawAddActions();
		DrawActions();
        PersistChanges();

        serializedObject.ApplyModifiedProperties();
	}

	private void DrawAddActions()
	{
		this.UseVerticalBoxLayout(() =>
		{
			var types = allComponentTypes.Select(x => string.Format("{0} [{1}]", x.Name, x.Namespace)).ToArray();
			var index = -1;
			index = EditorGUILayout.Popup("Add Action", index, types);

			if (index >= 0)
			{
                var action = (StateMachineAction)ScriptableObject.CreateInstance(allComponentTypes.ElementAt(index));
                AssetDatabase.AddObjectToAsset(action, handler);
				handler.Actions.Add(action);
			}
		});
	}

	private void DrawActions()
	{
		EditorGUILayout.BeginVertical(EditorExtensions.DefaultBoxStyle);
		int numberOfActions = handler.Actions.Count;

		this.WithHorizontalLayout(() =>
		{
			this.WithLabel("Actions (" + numberOfActions + ")");
			if (showActions)
			{
				if (this.WithIconButton("▾"))
				{
					showActions = false;
				}
			}
			else
			{
				if (this.WithIconButton("▸"))
				{
					showActions = true;
				}
			}
		});

		var actionsToRemove = new List<int>();
		if (showActions)
		{
            for (var i = 0; i < numberOfActions; i++)
			{
                if(handler.Actions[i] == null)
                {
					if (this.WithIconButton("-"))
					{
						actionsToRemove.Add(i);
					}
                    Debug.LogWarning("Action " + i + " is null!");
                    continue;
                }
				this.UseVerticalBoxLayout(() =>
				{
					var actionType = handler.Actions[i].GetType();

					var typeName = actionType == null ? "" : actionType.Name;
					var typeNamespace = actionType == null ? "" : actionType.Namespace;

					this.WithVerticalLayout(() =>
					{
						this.WithHorizontalLayout(() =>
						{
							if (this.WithIconButton("-"))
							{
								actionsToRemove.Add(i);
							}

							this.WithLabel(typeName);
						});

						EditorGUILayout.LabelField(typeNamespace);
						//EditorGUILayout.Space();
					});

					if (actionType == null)
					{
						if (GUILayout.Button("TYPE NOT FOUND. TRY TO CONVERT TO BEST MATCH?"))
						{
                            actionType = TypeUtilities.TryGetConvertedType(actionType.ToString());
                            if (actionType == null)
                            {
                              Debug.LogWarning("UNABLE TO CONVERT " + handler.Actions[i]);
                              return;
                            }
                            else
                            {
                                Debug.LogWarning("CONVERTED " + handler.Actions[i] + " to " + actionType.ToString());
                            }
						}
						else
						{
						    return;
						}
					}

                    var editor = Editor.CreateEditor(handler.Actions[i]);
                    editor.OnInspectorGUI();
				});
			}

			for (var i = 0; i < actionsToRemove.Count(); i++)
			{
				handler.Actions.RemoveAt(actionsToRemove[i]);
			}
		}

		EditorGUILayout.EndVertical();
	}

	private void PersistChanges()
	{
		if (GUI.changed && !Application.isPlaying)
		{
            this.SaveActiveSceneChanges();
        }
	}
}
