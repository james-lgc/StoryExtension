using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories.DataStructure
{
	[System.Serializable]
	public class Thread : NestedBaseData<Stage>, IDefault
	{
		[TextArea] [SerializeField] private string devDescription;

		[HideInInspector] [SerializeField] private bool isActive;
		public bool IsActive { get { return isActive; } }

		[HideInInspector] [SerializeField] private bool isCompleted;
		public bool IsCompleted { get { return isCompleted; } }

		[TextArea] [SerializeField] private string journalDescription;

		public override string Text
		{
			get
			{
				if (isCompleted == true)
				{
					return journalDescription;
				}
				return journalDescription;
			}
		}

		[SerializeField] private string serializedUniqueIDPrefix = "storyThread";
		protected override string uniqueIDPrefix { get { serializedUniqueIDPrefix = "storyThread"; return serializedUniqueIDPrefix; } }

		public Thread(Stage[] sentArray) : base(sentArray) { }
		public Thread(Stage sentData) : base(sentData) { }

		public int GetActiveStagePlace()
		{
			int activeCounter = 0;
			for (int i = 0; i < dataArray.Length; i++)
			{
				if (dataArray[i].IsCompleted == false)
				{
					break;
				}
				activeCounter++;
			}
			return activeCounter;
		}

		public bool GetIsMatch(StoryIndex sentIndex)
		{
			if (sentIndex.ThreadID == id) { return true; }
			return false;
		}

		public void Activate()
		{
			isActive = true;
		}

		public bool GetIsIndexedData(StoryIndex sentIndex)
		{
			if (sentIndex.ThreadID == id)
			{
				return true;
			}
			return false;
		}

		public void IncreaseStage(int sentID)
		{
			Stage stage = dataArray[GetActiveStagePlace()];
			if (stage.ID == sentID)
			{
				stage.CompleteStage();
				Debug.Log("Completing stage: " + sentID);
			}
		}

		public void IncreaseStagePoint(StoryIndex sentIndex, int i)
		{
			if (GetActiveStagePlace() == sentIndex.StageID)
			{
				dataArray[GetActiveStagePlace()].IncreaseStagePoint(i);
			}
		}

		public override DataItem[] GetArray()
		{
			int activePlace = GetActiveStagePlace();
			Stage[] tempStages;
			if (activePlace < dataArray.Length)
			{
				tempStages = new Stage[activePlace + 1];
			}
			else
			{
				tempStages = new Stage[dataArray.Length];
			}
			for (int i = 0; i < tempStages.Length; i++)
			{
				tempStages[tempStages.Length - 1 - i] = dataArray[i];
			}
			return tempStages;
		}

		public void SetDefault()
		{
			foreach (Stage stage in dataArray)
			{
				stage.SetDefault();
			}
		}

		public override List<string> GetUniqueIDs()
		{
			List<string> tempList = GetNewStringList();
			tempList.Add(uniqueID);
			tempList = tempList.Concat(GetChildUniqueIDs(dataArray)).ToList();
			return tempList;
		}

		public override void SetUniqueID(IProvider<string, string, string> sentProvider)
		{
			uniqueID = sentProvider.GetItem(uniqueID, uniqueIDPrefix);
			SetChildUnqueIDs(sentProvider);
		}

		public override string GetEndLabelText()
		{
			string unitText = "Stages";
			if (dataArray.Length == 1) { unitText = "Stage"; }
			return "[" + dataArray.Length + " " + unitText + "]";
		}

		public override void SetAsNew()
		{
			name = "New Thread";
			uniqueID = null;
			id = 0;
			devDescription = "";
			journalDescription = "";
			dataArray = new Stage[0];
		}
	}
}