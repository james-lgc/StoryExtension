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
		[TextArea] [SerializeField] private string name;

		[HideInInspector] [SerializeField] private int id;
		public override int ID { get { return id; } }

		[HideInInspector] [SerializeField] private bool isCompleted = false;
		public bool IsCompleted { get { return isCompleted; } }

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

		[SerializeField] private StagePoint[] stagePoints;
		public StagePoint[] StagePoints { get { return stagePoints; } }

		[SerializeField] private PrintableString[] notes;
		public PrintableString[] Notes { get { return notes; } }

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
			if (i >= stagePoints.Length) return;
			if (i < 0) return;
			stagePoints[i].Increase();
			CheckCompletion();
		}

		private void CheckCompletion()
		{
			for (int i = 0; i < stagePoints.Length; i++)
			{
				if (stagePoints[i].IsCompleted == false)
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
				if (stagePoints == null) return null;
				if (stagePoints.Length == 0) return null;
				return stagePoints;
			}
			if (notes == null) return null;
			if (notes.Length == 0) return null;
			return notes;
		}

		protected override void SetArray(PrintableString[] sentData)
		{
			notes = sentData;
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
			tempList = tempList.Concat(GetChildUniqueIDs(notes)).ToList();
			tempList = tempList.Concat(GetChildUniqueIDs(stagePoints)).ToList();
			return tempList;
		}

		public override void SetUniqueID(IProvider<string, string, string> sentProvider)
		{
			uniqueID = sentProvider.GetItem(uniqueID, uniqueIDPrefix);
			SetArrayUniqueIDs(notes, sentProvider);
			SetArrayUniqueIDs(stagePoints, sentProvider);
		}
	}
}