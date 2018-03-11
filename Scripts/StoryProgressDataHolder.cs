using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories
{
	[CreateAssetMenu(fileName = "StoryProgressDataHolder", menuName = "DataHolder/Story/StoryProgressDataHolder")]
	[System.Serializable]
	//Intermediary between high and low level Story classes as IDataHoldable
	//Allows for communication between extensions
	//returns story progress from the manager
	public class StoryProgressDataHolder : ExtendedScriptableObject, IDataHoldable<StoryProgressTracker?>, IProvider<int[][], bool>
	{
		public override ExtensionEnum.Extension Extension { get { return ExtensionEnum.Extension.Story; } }

		public Func<StoryProgressTracker?> GetDataFunc { protected get; set; }

		public StoryProgressTracker? GetData()
		{
			if (!GetIsExtensionLoaded() || GetDataFunc == null) { return null; }
			return GetDataFunc();
		}

		public int[][] GetItem(bool isActive)
		{
			if (!GetIsExtensionLoaded() || GetDataFunc == null) { return null; }
			StoryProgressTracker? nullableTracker = GetDataFunc();
			if (nullableTracker == null) { return null; }
			StoryProgressTracker tracker = (StoryProgressTracker)nullableTracker;
			if (isActive)
			{
				return tracker.GetActiveStoryIdentifiers();
			}
			return tracker.GetCompletedStoryIdentifiers();
		}
	}
}