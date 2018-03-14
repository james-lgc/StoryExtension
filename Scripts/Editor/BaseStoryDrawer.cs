using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base.Editor;
using DSA.Extensions.Base;
using System.Reflection;

namespace DSA.Extensions.Stories.DataStructure.Editor
{
	public abstract class BaseStoryDrawer : DataItemDrawer
	{
		protected SerializedProperty journalDescription;
		protected SerializedProperty devDescription;
		protected SerializedProperty dataArray;
		protected UnityEditorInternal.ReorderableList reorderableList;
		//action for edit button in list
		//action opens property in editor window
		protected override System.Action<SerializedProperty> editAction
		{
			get
			{
				System.Action<SerializedProperty> tempAction = (SerializedProperty sentProperty) =>
				{
					DSA.Extensions.Stories.DataStructure.Editor.StoryEditorWindow.Init(sentProperty);
				};
				return tempAction;
			}
		}

		protected override void SetProperties(SerializedProperty sentProperty)
		{
			if (GetIsCurrentProperty(sentProperty)) { return; }
			base.SetProperties(sentProperty);
			try { journalDescription = sentProperty.FindPropertyRelative("journalDescription"); }
			catch (System.Exception e) { Debug.Log("JournalDescription property not found.\n" + e.ToString()); }
			try { devDescription = sentProperty.FindPropertyRelative("devDescription"); }
			catch (System.Exception e) { Debug.Log("DevDescription property not found.\n" + e.ToString()); }
			try { dataArray = sentProperty.FindPropertyRelative("dataArray"); }
			catch (System.Exception e) { Debug.Log("DataArray property not found.\n" + e.ToString()); }
		}

		protected override void OnAddElement(UnityEditorInternal.ReorderableList list)
		{
			int index = list.serializedProperty.arraySize;
			list.serializedProperty.arraySize++;
			SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
			element.FindPropertyRelative("uniqueID").stringValue = null;
			element.FindPropertyRelative("name").stringValue = "New Item";
			element.FindPropertyRelative("id").intValue = 0;
			try
			{
				element.FindPropertyRelative("journalDescription").stringValue = null;
			}
			catch (System.Exception e) { e.ToString(); }
			try
			{
				element.FindPropertyRelative("devDescription").stringValue = null;
			}
			catch (System.Exception e) { e.ToString(); }
			try
			{
				element.FindPropertyRelative("dataArray").arraySize = 0;
			}
			catch (System.Exception e) { e.ToString(); }
			string serializedUniqueIDPrefix = element.FindPropertyRelative("serializedUniqueIDPrefix").stringValue;
			SerializedProperty uniqueID = element.FindPropertyRelative("uniqueID");
			IProvider<string, string, string> writer = (IProvider<string, string, string>)list.serializedProperty.serializedObject.targetObject;
			uniqueID.stringValue = writer.GetItem(uniqueID.stringValue, serializedUniqueIDPrefix);
		}
	}
}