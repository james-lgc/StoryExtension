using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Stories.DataStructure;

[System.Serializable]
public struct StoryProgressTracker
{
	public Story[] activeStories;
	public Story[] completedStories;

	public StoryProgressTracker(Story[] sentActiveStories, Story[] sentCompletedStories)
	{
		activeStories = sentActiveStories;
		completedStories = sentCompletedStories;
	}

	private StoryIndex[] GetIndiciesFromArray(Story[] sentStories)
	{
		if (sentStories == null) return null;
		StoryIndex[] tempArray = new StoryIndex[sentStories.Length];
		for (int i = 0; i < sentStories.Length; i++)
		{
			tempArray[i] = sentStories[i].ActiveIndex;
		}
		return tempArray;
	}

	public StoryIndex[] GetActiveStoryIndicies()
	{
		return GetIndiciesFromArray(activeStories);
	}

	public StoryIndex[] GetCompletedStoryIndicies()
	{
		return GetIndiciesFromArray(completedStories);
	}

	public bool GetIsStoryActive(StoryIndex sentIndex)
	{
		for (int i = 0; i < GetActiveStoryIndicies().Length; i++)
		{
			if (GetActiveStoryIndicies()[i] == sentIndex)
			{
				return true;
			}
		}
		return false;
	}
}
