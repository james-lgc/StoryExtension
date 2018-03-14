using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories.DataStructure
{
	[System.Serializable]
	public class Stage : NestedBaseData<PrintableString>, IDefault
	{
		public override string Text
		{
			get
			{
				if (isCompleted == true)
				{
					string strikeThroughText = GetStrikeThroughText(name);
					return strikeThroughText;
				}
				return name;
			}
		}

		[HideInInspector] [SerializeField] private bool isCompleted = false;
		public bool IsCompleted { get { return isCompleted; } }

		[SerializeField] private StagePoint[] secondDataArray;

		[SerializeField] private string serializedUniqueIDPrefix = "storyStage";
		protected override string uniqueIDPrefix { get { serializedUniqueIDPrefix = "storyStage"; return serializedUniqueIDPrefix; } }

		public Stage(PrintableString[] sentArray) : base(sentArray) { }
		public Stage(PrintableString sentData) : base(sentData) { }

		public void CompleteStage()
		{
			isCompleted = true;
		}

		public void IncreaseStagePoint(int i)
		{
			if (i >= secondDataArray.Length) return;
			if (i < 0) return;
			secondDataArray[i].Increase();
			CheckCompletion();
		}

		private void CheckCompletion()
		{
			for (int i = 0; i < secondDataArray.Length; i++)
			{
				if (secondDataArray[i].IsCompleted == false)
				{
					return;
				}
			}
			isCompleted = true;
		}

		public override DataItem[] GetArray()
		{
			if (isCompleted == false)
			{
				if (secondDataArray == null) return null;
				if (secondDataArray.Length == 0) return null;
				return secondDataArray;
			}
			if (dataArray == null) return null;
			if (dataArray.Length == 0) return null;
			return dataArray;
		}

		protected override void SetArray(PrintableString[] sentData)
		{
			dataArray = sentData;
		}

		private string GetStrikeThroughText(string sentText)
		{
			string newText = "";
			foreach (char c in sentText)
			{
				try
				{
					newText = newText + c + '\u0336';
				}
				catch (System.Exception e) { e.ToString(); }
			}
			return newText;
		}

		public void SetDefault()
		{
		}

		public override List<string> GetUniqueIDs()
		{
			List<string> tempList = GetNewStringList();
			tempList.Add(uniqueID);
			tempList = tempList.Concat(GetChildUniqueIDs(dataArray)).ToList();
			tempList = tempList.Concat(GetChildUniqueIDs(secondDataArray)).ToList();
			return tempList;
		}

		public override void SetUniqueID(IProvider<string, string, string> sentProvider)
		{
			uniqueID = sentProvider.GetItem(uniqueID, uniqueIDPrefix);
			SetArrayUniqueIDs(dataArray, sentProvider);
			SetArrayUniqueIDs(secondDataArray, sentProvider);
		}

		public override string GetEndLabelText()
		{
			string unitText1 = "StagePoints";
			if (secondDataArray.Length == 1) { unitText1 = "StagePoint"; }
			string unitText2 = "Notes";
			if (secondDataArray.Length == 1) { unitText1 = "Note"; }
			return "[" + secondDataArray.Length + " " + unitText1 + ", " + dataArray.Length + " " + unitText2 + "]";
		}

		public override void SetAsNew()
		{
			name = "New Stage";
			uniqueID = null;
			id = 0;
			//dataArray = new Story[0];
			secondDataArray = new StagePoint[0];
			dataArray = new PrintableString[0];
		}
	}
}