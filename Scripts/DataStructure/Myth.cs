using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories.DataStructure
{
	[System.Serializable]
	public class Myth : NestedBaseData<Story>, IConditional, IDefault
	{
		[TextArea] [SerializeField] private string devDescription;

		[HideInInspector] [SerializeField] private bool isCompleted;

		[SerializeField] private string serializedUniqueIDPrefix = "storyMyth";
		protected override string uniqueIDPrefix { get { serializedUniqueIDPrefix = "storyMyth"; return serializedUniqueIDPrefix; } }

		public Myth(Story[] sentArray) : base(sentArray) { }
		public Myth(Story sentData) : base(sentData) { }

		public void SetDefault()
		{
			foreach (Story story in dataArray)
			{
				story.SetDefault(id);
			}
		}

		public override DataItem[] GetArray()
		{
			return dataArray;
		}

		public bool GetIsConditionMet()
		{
			return isCompleted;
		}

		public void MeetCondition()
		{
			isCompleted = true;
		}

		public override List<string> GetUniqueIDs()
		{
			List<string> tempList = GetChildUniqueIDs(dataArray);
			tempList.Add(uniqueID);
			return tempList;
		}

		public override void SetUniqueID(IProvider<string, string, string> sentProvider)
		{
			uniqueID = sentProvider.GetItem(uniqueID, uniqueIDPrefix);
			SetChildUnqueIDs(sentProvider);
		}

		public override string GetEndLabelText()
		{
			string unitText = "Stories";
			if (dataArray.Length == 1) { unitText = "Story"; }
			return "[" + dataArray.Length + " " + unitText + "]";
		}

		public override void SetAsNew()
		{
			name = "New Myth";
			uniqueID = null;
			id = 0;
			devDescription = "";
			dataArray = new Story[0];
		}
	}
}