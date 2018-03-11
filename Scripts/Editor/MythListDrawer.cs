using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DSA.Extensions.Base.Editor;

namespace DSA.Extensions.Stories.DataStructure.Editor
{
	[CustomPropertyDrawer(typeof(MythList))]
	public class MythListDrawer : BaseStoryDrawer
	{
		protected override void SetProperties(SerializedProperty sentProperty)
		{
			if (GetIsCurrentProperty(sentProperty)) { return; }
			base.SetProperties(sentProperty);
			//method to return a string showing number of child elements in list item
			System.Func<SerializedProperty, string> endTextFunc = (SerializedProperty arrayProperty) =>
			{
				return GetArrayCountString(arrayProperty, "dataArray", "Story", "Stories");
			};
			//create list
			reorderableList = GetDefaultEditButtonList(dataArray, "Myths", editAction, endTextFunc, OnAddElement);
		}

		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = DrawTopLabel(position);
			//draw unique id
			newPosition = DrawUniqueID(newPosition);
			//draw name
			newPosition = DrawTextField(newPosition, name, "Name");
			//draw data array
			newPosition = DrawReorderableList(newPosition, reorderableList, "Myths");
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
			//label
			totalHeight += GetAddedHeight(lineHeight);
			//array
			totalHeight += GetAddedHeight(reorderableList.GetHeight());
			return totalHeight;
		}
	}
}