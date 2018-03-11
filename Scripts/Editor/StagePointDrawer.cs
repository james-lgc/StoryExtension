using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base.Editor;

namespace DSA.Extensions.Stories.DataStructure.Editor
{
	[CustomPropertyDrawer(typeof(StagePoint))]
	public class StagePointDrawer : BaseStoryDrawer
	{
		SerializedProperty maxValue;
		protected override void SetProperties(SerializedProperty sentProperty)
		{
			if (GetIsCurrentProperty(sentProperty)) { return; }
			base.SetProperties(sentProperty);
			maxValue = sentProperty.FindPropertyRelative("maxValue");
		}

		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = new Rect(position.x, position.y, position.width, 0F);
			//draw unique id
			newPosition = DrawUniqueID(newPosition);
			//draw name
			newPosition = DrawTextField(newPosition, name, "Name");
			//draw id
			newPosition = DrawIntField(newPosition, maxValue, "Max Value");
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SetProperties(property);
			float totalHeight = 0F;
			//unique id
			totalHeight += GetAddedHeight(lineHeight);
			//name
			totalHeight += GetAddedHeight(GetHeight(name));
			//id
			totalHeight += GetAddedHeight(lineHeight);
			return totalHeight;
		}
	}
}