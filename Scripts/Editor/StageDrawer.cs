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
		private SerializedProperty stagePoints;
		private UnityEditorInternal.ReorderableList notesList;
		private UnityEditorInternal.ReorderableList stagePointsList;
		protected override void SetProperties(SerializedProperty sentProperty)
		{
			if (GetIsCurrentProperty(sentProperty)) { return; }
			base.SetProperties(sentProperty);
			stagePoints = sentProperty.FindPropertyRelative("secondDataArray");
			//create list
			notesList = GetDefaultEditButtonList(dataArray, "Notes");
			stagePointsList = GetDefaultEditButtonList(stagePoints, "Stage Points");
		}

		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = DrawTopLabel(position);
			//draw unique id
			newPosition = DrawUniqueID(newPosition);
			//draw name
			newPosition = EditorTool.DrawTextArea(newPosition, name, "Name");
			//draw id
			newPosition = EditorTool.DrawIntField(newPosition, id, "Stage ID");
			//draw notes
			newPosition = EditorTool.DrawReorderableList(newPosition, notesList, "Notes");
			//draw stage points
			newPosition = EditorTool.DrawReorderableList(newPosition, stagePointsList, "Stage Points");
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
			totalHeight += EditorTool.GetAddedHeight(EditorTool.GetHeight(name));
			//id
			totalHeight += EditorTool.AddedLineHeight;
			//label
			totalHeight += EditorTool.AddedLineHeight;
			//notes
			totalHeight += EditorTool.GetAddedHeight(notesList.GetHeight());
			//label
			totalHeight += EditorTool.AddedLineHeight;
			//stagePoints
			totalHeight += EditorTool.GetAddedHeight(stagePointsList.GetHeight());
			return totalHeight;
		}
	}
}
