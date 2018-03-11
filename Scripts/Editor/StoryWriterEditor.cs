using UnityEngine;
using System.Collections;
using UnityEditor;
using DSA.Extensions.Stories;
using DSA.Extensions.Stories.DataStructure;
using DSA.Extensions.Base.Editor;

namespace DSA.Extensions.Stories.DataStructure.Editor
{
	[CustomEditor(typeof(StoryWriter))]
	public class StoryWriterEditor : UnityEditor.Editor
	{
		protected SerializedProperty path;
		//Read stories from Json file when enabled in inspector 
		private void OnEnable()
		{
			StoryWriter storyWriter = (StoryWriter)target;
			storyWriter.Set();
		}

		//GUI Button to serialize conversation to Json file after edited in inspector
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			DrawDefaultInspector();
			if (GUILayout.Button("Edit"))
			{
				StoryEditorWindow.Init();
			}
			serializedObject.ApplyModifiedProperties();
			return;
		}
	}
}