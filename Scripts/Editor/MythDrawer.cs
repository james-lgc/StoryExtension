using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using DSA.Extensions.Base.Editor;

namespace DSA.Extensions.Stories.DataStructure.Editor
{
	[CustomPropertyDrawer(typeof(Myth))]
	public class MythDrawer : BaseStoryDrawer
	{
		protected override void SetProperties(SerializedProperty sentProperty)
		{
			if (GetIsCurrentProperty(sentProperty)) { return; }
			base.SetProperties(sentProperty);
			//method to return a string showing number of child elements in list item
			System.Func<SerializedProperty, string> endTextFunc = (SerializedProperty arrayProperty) =>
			{
				return EditorTool.GetArrayCountString(arrayProperty, "dataArray", "Thread", "Threads");
			};
			//create list
			reorderableList = GetDefaultEditButtonList(dataArray, "Stories");
		}

		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = DrawTopLabel(position);
			//draw unique id
			newPosition = DrawUniqueID(newPosition);
			//draw name
			newPosition = EditorTool.DrawTextField(newPosition, name, "Name");
			//draw id
			newPosition = EditorTool.DrawIntField(newPosition, id, "Myth ID");
			//draw dev description
			newPosition = EditorTool.DrawTextArea(newPosition, devDescription, "Dev Description");
			//draw data array
			newPosition = EditorTool.DrawReorderableList(newPosition, reorderableList, "Stories");
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
			//label
			totalHeight += EditorTool.AddedLineHeight;
			//array
			totalHeight += EditorTool.GetAddedHeight(reorderableList.GetHeight());
			return totalHeight;
		}
	}
}