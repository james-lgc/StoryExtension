using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Stories
{
	[System.Serializable]
	public class StoryRequirement
	{
		[SerializeField] private bool isCompleted;
		public bool IsCompleted { get { return isCompleted; } }
		[SerializeField] private int currentProgress;
		[SerializeField] private int maxProgress;

		public virtual void UpdateRequirement(int i = 0)
		{
			currentProgress = currentProgress + i;
		}
	}
}