using System;
using UnityEngine;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories
{
	[AddComponentMenu("Trait/Story/Story Dependent Trait")]
	[System.Serializable]
	//Concrete trait to trigger an action if story progress has reached a defined stage
	public class StoryDependentTrait : TraitBase, IConditional, IDataRetrievable<StoryProgressTracker?>
	{
		public override ExtensionEnum Extension { get { return ExtensionEnum.Story; } }

		//Data holder to get progress from manager
		public IDataHoldable<StoryProgressTracker?> DataHolder { protected get; set; }
		//The progress threshold that must be met
		[SerializeField] private StoryIndex storyIndex;

		//Compare defined index with current progress to determine if condition is met
		public bool GetIsConditionMet()
		{
			//Get progress from data holder
			StoryProgressTracker? progressTracker = DataHolder.GetData();
			if (progressTracker != null)
			{
				//Cast nullable progress as non-nullable
				StoryProgressTracker tracker = (StoryProgressTracker)progressTracker;
				//Check if condition is met
				if (tracker.GetIsStoryActive(storyIndex))
				{
					return true;
				}
			}
			return false;
		}

		public void MeetCondition()
		{
			throw new NotImplementedException();
		}
	}
}