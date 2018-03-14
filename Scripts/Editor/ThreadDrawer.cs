using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base.Editor;

namespace DSA.Extensions.Stories.DataStructure.Editor
{
	[CustomPropertyDrawer(typeof(Thread))]
	public class ThreadDrawer : BaseStoryDrawer
	{
		protected override void SetProperties(SerializedProperty sentProperty)
		{
			if (GetIsCurrentProperty(sentProperty)) { return; }
			base.SetProperties(sentProperty);
			//method to return a string showing number of child elements in list item
			System.Func<SerializedProperty, string> endTextFunc = (SerializedProperty arrayProperty) =>
			{
				//return GetArrayCountString(arrayProperty, "dataArray", "Stage", "Stages");
				return "";
			};
			System.Action<UnityEditorInternal.ReorderableList> addAction = (UnityEditorInternal.ReorderableList list) =>
			{
				int index = list.serializedProperty.arraySize;
				OnAddElement(list);
				SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
				element.FindPropertyRelative("notes").arraySize = 0;
				element.FindPropertyRelative("stagePoints").arraySize = 0;
			};
			//create list
			reorderableList = GetDefaultEditButtonList(dataArray, "Stages");
		}

		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = DrawTopLabel(position);
			//draw unique id
			newPosition = DrawUniqueID(newPosition);
			//draw name
			newPosition = EditorTool.DrawTextField(newPosition, name, "Name");
			//draw id
			newPosition = EditorTool.DrawIntField(newPosition, id, "Thread ID");
			//draw dev description
			newPosition = EditorTool.DrawTextArea(newPosition, devDescription, "Dev Description");
			//draw journal description
			newPosition = EditorTool.DrawTextArea(newPosition, journalDescription, "Journal Description");
			//draw data array
			newPosition = EditorTool.DrawReorderableList(newPosition, reorderableList, "Stages");
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SetProperties(property);
			float totalHeight = EditorTool.InitialVerticalPadding;
			//label
			totalHeight += EditorTool.AddedLineHeight;
			//unique id
			totalHeight += EditorTool.AddedLineHeight;
			//name
			totalHeight += EditorTool.AddedLineHeight;
			//id
			totalHeight += EditorTool.AddedLineHeight;
			//dev description
			totalHeight += EditorTool.GetAddedHeight(EditorTool.GetHeight(devDescription));
			//journal description
			totalHeight += EditorTool.GetAddedHeight(EditorTool.GetHeight(journalDescription));
			//label
			totalHeight += EditorTool.AddedLineHeight;
			//array
			totalHeight += EditorTool.GetAddedHeight(reorderableList.GetHeight());
			return totalHeight;
		}
	}
}