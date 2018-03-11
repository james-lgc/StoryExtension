using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base.Editor;

namespace DSA.Extensions.Stories.DataStructure.Editor
{
	[CustomPropertyDrawer(typeof(Stage))]
	public class StageDrawer : BaseStoryDrawer
	{
		private SerializedProperty notes;
		private SerializedProperty stagePoints;
		private UnityEditorInternal.ReorderableList notesList;
		private UnityEditorInternal.ReorderableList stagePointsList;
		protected override void SetProperties(SerializedProperty sentProperty)
		{
			if (GetIsCurrentProperty(sentProperty)) { return; }
			base.SetProperties(sentProperty);
			notes = sentProperty.FindPropertyRelative("notes");
			stagePoints = sentProperty.FindPropertyRelative("stagePoints");
			//method to return a string showing number of child elements in list item
			System.Func<SerializedProperty, string> endTextFunc = (SerializedProperty arrayProperty) =>
			{
				//return GetArrayCountString(arrayProperty, "dataArray", "Stage", "Stages");
				return "";
			};
			//create list
			//reorderableList = GetDefaultEditButtonList(dataArray, "Stages", editAction, endTextFunc);
			notesList = GetDefaultEditButtonList(notes, "Notes", editAction);
			stagePointsList = GetDefaultEditButtonList(stagePoints, "Stage Points", editAction, null, OnAddElement);
		}

		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = DrawTopLabel(position);
			//draw unique id
			newPosition = DrawUniqueID(newPosition);
			//draw name
			newPosition = DrawTextArea(newPosition, name, "Name");
			//draw id
			newPosition = DrawIntField(newPosition, id, "Stage ID");
			//draw notes
			newPosition = DrawReorderableList(newPosition, notesList, "Notes");
			//draw stage points
			newPosition = DrawReorderableList(newPosition, stagePointsList, "Stage Points");
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
			totalHeight += GetAddedHeight(GetHeight(name));
			//id
			totalHeight += GetAddedHeight(lineHeight);
			//label
			totalHeight += GetAddedHeight(lineHeight);
			//notes
			totalHeight += GetAddedHeight(notesList.GetHeight());
			//label
			totalHeight += GetAddedHeight(lineHeight);
			//stagePoints
			totalHeight += GetAddedHeight(stagePointsList.GetHeight());
			return totalHeight;
		}
	}
}
