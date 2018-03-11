using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories.DataStructure
{
	[System.Serializable]
	public class Story : NestedBaseData<Thread>, IPrintable, IDefault<int>
	{
		[SerializeField] private string name;
		public override string Text { get { return name; } }

		[SerializeField] private int id;
		public override int ID { get { return id; } }

		[TextArea] [SerializeField] private string journalDescription;
		public string PrintableText { get { return journalDescription; } }

		[TextArea] [SerializeField] private string devDescription;

		public StoryIndex ActiveIndex
		{
			get
			{
				int[] tempArray = new int[4];
				tempArray[0] = parentID;
				tempArray[1] = ID;
				Thread tempThread = dataArray[GetActiveThreadPlace()];
				tempArray[2] = tempThread.ID;
				tempArray[3] = tempThread.GetActiveStagePlace();
				StoryIndex tempIndex = new StoryIndex(tempArray);
				return tempIndex;
			}
		}

		[SerializeField] private Thread[] dataArray;

		public Story(Thread[] sentArray) : base(sentArray) { }
		public Story(Thread sentData) : base(sentData) { }

		private int parentID;
		public int ParentID { get { return parentID; } }

		[SerializeField] private string serializedUniqueIDPrefix = "story";
		protected override string uniqueIDPrefix { get { serializedUniqueIDPrefix = "story"; return serializedUniqueIDPrefix; } }

		public void SetDefault(int i)
		{
			parentID = i;
			foreach (Thread thread in dataArray)
			{
				thread.SetDefault();
			}
		}

		public override DataItem[] GetArray()
		{
			List<int> places = new List<int>();
			for (int i = 0; i < dataArray.Length; i++)
			{
				if (dataArray[i].IsActive == true)
				{
					if (dataArray[i].IsCompleted == false)
					{
						places.Add(i);
					}
				}
			}
			Thread[] tempThreads = new Thread[places.Count];
			for (int i = 0; i < tempThreads.Length; i++)
			{
				tempThreads[places.Count - 1 - i] = dataArray[places[i]];
			}
			return tempThreads;
		}

		protected override void SetArray(Thread[] sentData)
		{
			dataArray = sentData;
		}

		public int GetActiveThreadPlace()
		{
			int counter = 0;
			for (int i = 0; i < dataArray.Length; i++)
			{
				if (dataArray[i].IsActive == true)
				{
					if (dataArray[i].IsCompleted == false)
					{
						break;
					}
					counter++;
				}
			}
			return counter;
		}

		public void ActivateThread(StoryIndex sentIndex)
		{
			for (int i = 0; i < dataArray.Length; i++)
			{
				if (dataArray[i].GetIsMatch(sentIndex) == true)
				{
					dataArray[i].Activate();
				}
			}
		}

		public bool GetIsIndexedStory(StoryIndex sentIndex)
		{
			if (sentIndex.MythID == parentID)
			{
				if (sentIndex.StoryID == id)
				{
					return true;
				}
			}
			return false;
		}

		public void IncreaseStage(StoryIndex sentIndex)
		{
			for (int i = 0; i < dataArray.Length; i++)
			{
				if (dataArray[i].GetIsIndexedData(sentIndex))
				{
					dataArray[i].IncreaseStage(sentIndex.StageID);
				}
			}
		}

		public void IncreaseStagePoint(StoryIndex sentIndex, int pointID)
		{
			for (int i = 0; i < dataArray.Length; i++)
			{
				if (dataArray[i].GetIsIndexedData(sentIndex))
				{
					dataArray[i].IncreaseStagePoint(sentIndex, pointID);
				}
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
	}
}
