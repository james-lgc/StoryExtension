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
			//method to return a string showing number of child elements in list item
			System.Func<SerializedProperty, string> endTextFunc = (SerializedProperty arrayProperty) =>
			{
				return GetArrayCountString(arrayProperty, "dataArray", "Stage", "Stages");
			};
			//create list
			reorderableList = GetDefaultEditButtonList(dataArray, "Threads", editAction, endTextFunc, OnAddElement);
		}

		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = DrawTopLabel(position);
			//draw unique id
			newPosition = DrawUniqueID(newPosition);
			//draw name
			newPosition = DrawTextField(newPosition, name, "Name");
			//draw id
			newPosition = DrawIntField(newPosition, id, "Story ID");
			//draw dev description
			newPosition = DrawTextArea(newPosition, devDescription, "Dev Description");
			//draw journal description
			newPosition = DrawTextArea(newPosition, journalDescription, "Journal Description");
			//draw data array
			newPosition = DrawReorderableList(newPosition, reorderableList, "Threads");
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