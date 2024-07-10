using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FSMExample))]
public class GameManagerEditor : Editor
{
    public bool ShowFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // O tipo aqui deve ser FSMExample, não GameManager
        FSMExample fsm = (FSMExample)target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("State Machine");

        if (fsm.stateMachine == null) return;

        if (fsm.stateMachine.CurrentState != null)
        {
            EditorGUILayout.LabelField("Current State", fsm.stateMachine.CurrentState.ToString());
        }

        ShowFoldout = EditorGUILayout.Foldout(ShowFoldout, "Available States");

        if (ShowFoldout)
        {
            if (fsm.stateMachine.dictionaryState != null)
            {
                var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
                var vals = fsm.stateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
                }
            }
        }
    }
}
