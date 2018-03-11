using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using DSA.Extensions.Base.Editor;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories.DataStructure.Editor
{
	//an editor window class to edit conversation json data
	public class StoryEditorWindow : DataEditorWindow<MythList>
	{
		protected override int maxNestedButtons { get { return 5; } }

		//Menu item initiailisation to load the top level conversation list
		[MenuItem("Window/Stories")]
		public static void Init()
		{
			StoryEditorWindow window = (StoryEditorWindow)EditorWindow.GetWindow(typeof(StoryEditorWindow));
			window.Set();
			window.Show();
		}

		//initialisation method with a sent property to display
		public static void Init(SerializedProperty sentProperty)
		{
			StoryEditorWindow window = (StoryEditorWindow)EditorWindow.GetWindow(typeof(StoryEditorWindow));
			window.Set();
			if (window.propertyList == null) { window.propertyList = new List<SerializedProperty>(); }
			window.propertyList.Add(sentProperty);
			window.Show();
		}

		protected void OnFocus()
		{
			if (propertyList == null)
			{
				Init();
			}
		}

		protected void Set()
		{
			writer = (JsonWriter<MythList>)Resources.Load("StoryWriter");
			base.Set(writer, "mythList");
		}
	}
}
