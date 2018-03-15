using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using DSA.Extensions.Stories.DataStructure;
using DSA.Extensions.Base;
using System;
using System.Linq;

namespace DSA.Extensions.Stories
{
	public class StoryManager : ClickableCanvasedManagerBase<JournalCanvas>
	{
		public override ExtensionEnum Extension { get { return ExtensionEnum.Story; } }

		//StoryNameButtons 0
		//StoryStatusButtons 1
		//StoryStageButtons 2
		[SerializeField] StoryWriter writer;

		[SerializeField] private MythList hardMythList;
		public MythList HardMythList { get { return hardMythList; } }

		[SerializeField] private StoryProgressDataHolder storyProgressDataHolder;

		[SerializeField] /*[HideInInspector]*/ private List<Story> activeStories;
		public List<Story> ActiveStories { get { if (activeStories == null) { activeStories = new List<Story>(); } return activeStories; } }
		private StoryIndex[] ActiveIndicies { get { StoryIndex[] tempArray = GetIndicies(ActiveStories); return tempArray; } }
		[SerializeField] [HideInInspector] private Dictionary<int, Story> activeStoryDictionary;
		public Dictionary<int, Story> ActiveStoryDictionary;
		[SerializeField] /*[HideInInspector]*/ private List<Story> completedStories;
		public List<Story> CompletedStories { get { if (completedStories == null) { completedStories = new List<Story>(); } return completedStories; } }
		private StoryIndex[] CompletedIndicies { get { StoryIndex[] tempArray = GetIndicies(CompletedStories); return tempArray; } }

		public delegate void OnProgressStoryEvent(List<StoryIndex> storyIndicies);
		public event OnProgressStoryEvent OnProgressStory;

		public override void Initialize()
		{
			base.Initialize();
			uiController.CancelAction = EndProcess;
			storyProgressDataHolder.GetDataFunc = GetProgress;
			canvas.SetProgressFunc(GetProgress);
			SetStories();
		}

		protected override void StartProcess()
		{
			base.StartProcess();
			canvas.DisplayData();
		}

		public override void PassDelegatesToTraits(TraitedMonoBehaviour sentObj)
		{
			SetDataHolder<StoryDependentTrait, StoryProgressTracker?>(sentObj, storyProgressDataHolder);
		}

		public StoryProgressTracker? GetProgress()
		{
			StoryProgressTracker tempTracker = new StoryProgressTracker(ActiveStories.ToArray(), CompletedStories.ToArray());
			return tempTracker;
		}

		public void SetStories()
		{
			hardMythList = writer.ReadTFromJson();
			foreach (Myth myth in hardMythList.GetArray())
			{
				myth.SetDefault();
			}
			if (activeStories == null || activeStories.Count == 0)
			{
				Story startStory = (Story)hardMythList.GetIndexedData(new int[] { 1, 0 });
				activeStories.Add(startStory);
				StoryIndex startIndex = new StoryIndex(new int[] { 1, 0, 0, 0 });
				startStory.ActivateThread(startIndex);
			}
			ProgressStory();
		}

		public void IncreaseStoryStage(StoryIndex sentIndex)
		{
			for (int i = 0; i < activeStories.Count; i++)
			{
				Story story = activeStories[i];
				if (story.GetIsIndexedStory(sentIndex))
				{
					story.IncreaseStage(sentIndex);
				}
			}
			for (int i = 0; i < ActiveIndicies.Length; i++)
			{
				Debug.Log("Myth: " + ActiveIndicies[i].MythID + ", Story: " + ActiveIndicies[i].StoryID + ", Thread: " + ActiveIndicies[i].ThreadID + ", Stage: " + ActiveIndicies[i].StageID);
			}
		}

		public void ActivateStory(StoryIndex sentIndex)
		{
			Story story = (Story)hardMythList.GetIndexedData(new int[] { sentIndex.MythID, sentIndex.StoryID });
			activeStories.Add(story);
			story.ActivateThread(sentIndex);
		}

		public void ActivateThread(StoryIndex sentIndex)
		{
			Story story;
			if (activeStoryDictionary == null) { activeStoryDictionary = new Dictionary<int, Story>(); }
			if (activeStoryDictionary.TryGetValue(sentIndex.StoryID, out story))
			{
				story.ActivateThread(sentIndex);
			}
		}

		public override bool ProcessInstruction(InstructionData sentInstruction)
		{
			//return false base process conditions are not met
			if (!base.ProcessInstruction(sentInstruction)) { return false; }
			StoryIndex tempIndex = new StoryIndex(sentInstruction.identifier);
			switch (sentInstruction.identifier.Length)
			{
				//Activate story
				case 2:
					ActivateStory(tempIndex);
					break;
				//Activate thread
				case 3:
					ActivateThread(tempIndex);
					break;
				//Increase stage
				case 4:
					IncreaseStoryStage(tempIndex);
					break;
				//Increase stage point
				case 5:
					IncreaseStagePoint(tempIndex, sentInstruction.identifier[4]);
					break;
			}
			return true;
		}

		public void ProgressStory()
		{
			if (OnProgressStory == null) { return; }
		}

		public DataItem GetIndexedStory(StoryIndex sentIndex, int depth)
		{
			return HardMythList.GetIndexedData(sentIndex.IDs);
		}

		public DataItem GetIndexedStory(int[] sentIds)
		{
			DataItem dataItem = hardMythList.GetIndexedData(sentIds);
			return dataItem;
		}

		public void IncreaseStagePoint(StoryIndex sentIndex, int stagePointID)
		{
			for (int i = 0; i < activeStories.Count; i++)
			{
				Story story = activeStories[i];
				if (story.GetIsIndexedStory(sentIndex))
				{
					story.IncreaseStagePoint(sentIndex, stagePointID);
				}
			}
		}

		public StoryIndex[] GetIndicies(List<Story> sentList)
		{
			StoryIndex[] tempArray = new StoryIndex[sentList.Count];
			for (int i = 0; i < tempArray.Length; i++)
			{
				tempArray[i] = sentList[i].ActiveIndex;
			}
			return tempArray;
		}

		public override void AddDataToArrayList(ArrayList sentArrayList)
		{
			StoryProgressTracker progress = new StoryProgressTracker(activeStories.ToArray(), completedStories.ToArray());
			sentArrayList.Add(progress);
		}

		public override void ProcessArrayList(ArrayList sentArrayList)
		{
			for (int i = 0; i < sentArrayList.Count; i++)
			{
				if (sentArrayList[i] is StoryProgressTracker)
				{
					StoryProgressTracker progress = (StoryProgressTracker)sentArrayList[i];
					activeStories = progress.activeStories.ToList();
					completedStories = progress.completedStories.ToList();
				}
			}
		}
	}
}
