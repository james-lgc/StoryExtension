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
			//create list
			reorderableList = GetDefaultEditButtonList(dataArray, "Myths");
		}

		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = DrawTopLabel(position);
			//draw unique id
			newPosition = DrawUniqueID(newPosition);
			//draw name
			newPosition = EditorTool.DrawTextField(newPosition, name, "Name");
			//draw data array
			newPosition = EditorTool.DrawReorderableList(newPosition, reorderableList, "Myths");
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
			//label
			totalHeight += EditorTool.AddedLineHeight;
			//array
			totalHeight += EditorTool.GetAddedHeight(reorderableList.GetHeight());
			return totalHeight;
		}
	}
}