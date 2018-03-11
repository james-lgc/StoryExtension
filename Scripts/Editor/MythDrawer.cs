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
				return GetArrayCountString(arrayProperty, "dataArray", "Thread", "Threads");
			};
			//create list
			reorderableList = GetDefaultEditButtonList(dataArray, "Stories", editAction, endTextFunc, OnAddElement);
		}

		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = DrawTopLabel(position);
			//draw unique id
			newPosition = DrawUniqueID(newPosition);
			//draw name
			newPosition = DrawTextField(newPosition, name, "Name");
			//draw id
			newPosition = DrawIntField(newPosition, id, "Myth ID");
			//draw dev description
			newPosition = DrawTextArea(newPosition, devDescription, "Dev Description");
			//draw data array
			newPosition = DrawReorderableList(newPosition, reorderableList, "Stories");
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
			//label
			totalHeight += GetAddedHeight(lineHeight);
			//array
			totalHeight += GetAddedHeight(reorderableList.GetHeight());
			return totalHeight;
		}
	}
}