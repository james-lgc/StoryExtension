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
		[SerializeField] private string name;
		public override string Text { get { return name; } }

		[SerializeField] private int id;
		public override int ID { get { return id; } }

		[TextArea] [SerializeField] private string devDescription;

		[HideInInspector] [SerializeField] private bool isCompleted;

		[SerializeField] private Story[] dataArray;

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

		protected override void SetArray(Story[] sentData)
		{
			dataArray = sentData;
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
	}
}