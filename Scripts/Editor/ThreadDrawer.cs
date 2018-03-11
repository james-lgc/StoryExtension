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
			reorderableList = GetDefaultEditButtonList(dataArray, "Stages", editAction, endTextFunc, addAction);
		}

		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = DrawTopLabel(position);
			//draw unique id
			newPosition = DrawUniqueID(newPosition);
			//draw name
			newPosition = DrawTextField(newPosition, name, "Name");
			//draw id
			newPosition = DrawIntField(newPosition, id, "Thread ID");
			//draw dev description
			newPosition = DrawTextArea(newPosition, devDescription, "Dev Description");
			//draw journal description
			newPosition = DrawTextArea(newPosition, journalDescription, "Journal Description");
			//draw data array
			newPosition = DrawReorderableList(newPosition, reorderableList, "Stages");
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SetProperties(property);
			float totalHeight = initialVerticalPaddingHeight;
			//label
			totalHeight += GetAddedHeight(lineHeight);
			//unique id
			totalHeight += GetAddedHeight(lineHeight);
			//name
			totalHeight += GetAddedHeight(lineHeight);
			//id
			totalHeight += GetAddedHeight(lineHeight);
			//dev description
			totalHeight += GetAddedHeight(GetHeight(devDescription));
			//journal description
			totalHeight += GetAddedHeight(GetHeight(journalDescription));
			//label
			totalHeight += GetAddedHeight(lineHeight);
			//array
			totalHeight += GetAddedHeight(reorderableList.GetHeight());
			return totalHeight;
		}
	}
}