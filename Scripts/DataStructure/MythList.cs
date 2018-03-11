using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories.DataStructure
{
	[System.Serializable]
	public class MythList : NestedBaseData<Myth>, IDefault
	{
		[SerializeField] private string name = "Myth List";

		[SerializeField] private int id;
		public override int ID { get { return id; } }

		public override string Text { get { return "Myth List"; } }

		[SerializeField] private Myth[] dataArray;
		public Myth[] Myths { get { return dataArray; } }

		[SerializeField] private string serializedUniqueIDPrefix = "storyMythList";
		protected override string uniqueIDPrefix { get { serializedUniqueIDPrefix = "storyMythList"; return serializedUniqueIDPrefix; } }

		public MythList(Myth[] sentArray) : base(sentArray) { }
		public MythList(Myth sentData) : base(sentData) { }

		public void SetDefault()
		{
			for (int i = 0; i < dataArray.Length; i++)
			{
				dataArray[i].SetDefault();
			}
		}

		public override DataItem[] GetArray()
		{
			return dataArray;
		}

		protected override void SetArray(Myth[] sentData)
		{
			dataArray = sentData;
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
