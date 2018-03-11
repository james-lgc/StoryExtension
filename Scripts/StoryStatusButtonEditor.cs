using System.Collections;
using UnityEditor;

//Editor script required to display defualt Button UI in Unity Editor
[CustomEditor(typeof(StoryStatusButtonEditor))]
public class StoryStatusButtonEditor : UnityEditor.UI.ButtonEditor
{

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
	}
}
