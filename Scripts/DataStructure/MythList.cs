using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories.DataStructure
{
	[System.Serializable]
	public class MythList : NestedBaseData<Myth>, IDefault
	{
		[SerializeField] private string serializedUniqueIDPrefix = "storyMythList";
		protected override string uniqueIDPrefix { get { serializedUniqueIDPrefix = "storyMythList"; return serializedUniqueIDPrefix; } }

		public MythList(Myth[] sentArray) : base(sentArray) { }
		public MythList(Myth sentData) : base(sentData) { }

		public void SetDefault()
		{
			if (dataArray == null) { return; }
			for (int i = 0; i < dataArray.Length; i++)
			{
				dataArray[i].SetDefault();
			}
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
			string unitText = "Myths";
			if (GetArray().Length == 1) { unitText = "Myth"; }
			return "[" + GetArray().Length + " " + unitText + "]";
		}

		public override void SetAsNew()
		{
			name = "New MythList";
			uniqueID = null;
			id = 0;
			dataArray = new Myth[0];
		}
	}
}
