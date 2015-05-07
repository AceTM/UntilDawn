using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(PlayerPrefList))]
public class PlayerPrefInspector : Editor {
	private SerializedObject serialObject;

	public void OnEnable()
	{
		serialObject = new SerializedObject(target);
	}

	public override void OnInspectorGUI () 
	{
		serialObject.Update();

		EditorGUILayout.LabelField(UIConstants.PREF_ARRAY, EditorStyles.boldLabel);

		SerializedProperty prefValues = serialObject.FindProperty(VariableConstants.PREF_VALUES);
		EditorGUILayout.PropertyField(prefValues, true);

		EditorGUILayout.LabelField(UIConstants.PREF_OBJECTS, EditorStyles.boldLabel);

		SerializedProperty rowObject = serialObject.FindProperty(VariableConstants.ROW_OBJECT);
		EditorGUILayout.PropertyField(rowObject, false);

		SerializedProperty rowParent = serialObject.FindProperty(VariableConstants.ROW_PARENT);
		EditorGUILayout.PropertyField(rowParent, false);

		serialObject.ApplyModifiedProperties();
	}
}
