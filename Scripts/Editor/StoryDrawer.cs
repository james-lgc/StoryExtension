using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using DSA.Extensions.Base.Editor;

namespace DSA.Extensions.Stories.DataStructure.Editor
{
	[CustomPropertyDrawer(typeof(Story))]
	public class StoryDrawer : BaseStoryDrawer
	{
		protected override void SetProperties(SerializedProperty sentProperty)
		{
			if (GetIsCurrentProperty(sentProperty)) { return; }
			base.SetProperties(sentProperty);
			//create list
			reorderableList = GetDefaultEditButtonList(dataArray, "Threads");
		}

		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = DrawTopLabel(position);
			//draw unique id
			newPosition = DrawUniqueID(newPosition);
			//draw name
			newPosition = EditorTool.DrawTextField(newPosition, name, "Name");
			//draw id
			newPosition = EditorTool.DrawIntField(newPosition, id, "Story ID");
			//draw dev description
			newPosition = EditorTool.DrawTextArea(newPosition, devDescription, "Dev Description");
			//draw journal description
			newPosition = EditorTool.DrawTextArea(newPosition, journalDescription, "Journal Description");
			//draw data array
			newPosition = EditorTool.DrawReorderableList(newPosition, reorderableList, "Threads");
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