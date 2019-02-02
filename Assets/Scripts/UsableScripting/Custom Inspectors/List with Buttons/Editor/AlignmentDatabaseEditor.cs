using UnityEditor;
using UnityEngine;

/// <summary>
/// Source: BurgZerg Arcade - Unity Custom Inspector: https://www.youtube.com/watch?v=zE4PYusy1YY&list=PL_eGgISVYZkdeYj10fsn7gqzyEez-lnf-&index=20
/// </summary>
[CustomEditor(typeof(AlignmentDatabase))]
public class AlignmentDatabaseEditor : Editor
{
	private AlignmentDatabase db;

	private void OnEnable()
	{
		db = (AlignmentDatabase)target;
	}

	public override void OnInspectorGUI()
	{
		//Update the serializedProperty - always do this in the beginning of OnInspectorGUI!
		serializedObject.Update();

		ShowAddAlignementButton();
		GUILayout.Space(20);
		ShowAlignements();

		Undo.RecordObject(db, "Database changed");

		//Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI!
		serializedObject.ApplyModifiedProperties();
	}

	private void ShowAddAlignementButton()
	{
		GUILayout.BeginVertical("box");
		GUILayout.Space(5);
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);

		GUILayout.Label($"Total Alignments: {db.alignements.Count}");
		if (GUILayout.Button("Add Alignment"))
		{
			AddAlignment();
		}

		GUILayout.EndHorizontal();
		GUILayout.Space(5);
		GUILayout.EndVertical();
	}

	private void ShowAlignements()
	{
		for (int i = 0; i < db.alignements.Count; i++)
		{
			GUILayout.BeginHorizontal();
			db.alignements[i].alignementName = GUILayout.TextField(db.alignements[i].alignementName, GUILayout.Width(100f));
			db.alignements[i].backgroundColor = EditorGUILayout.ColorField(db.alignements[i].backgroundColor);
			if (GUILayout.Button("X"))
			{
				RemoveAlignment(i);
				return; //Returning here, since else the count of list will have changed → Index Out of range incoming (according to the video, I don't get an error when ignoring this).
			}
			GUILayout.EndHorizontal();
		}
	}

	private void AddAlignment()
	{
		db.alignements.Add(new Alignement());
	}

	private void RemoveAlignment(int index)
	{
		db.alignements.RemoveAt(index);
	}
}