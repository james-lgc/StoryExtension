using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories
{
	[RequireComponent(typeof(TraitedMonoBehaviour))]
	[System.Serializable]
	public class StoryProgressTrait : TraitBase, ISendable<StoryProgressTracker?>
	{
		public override ExtensionEnum Extension { get { return ExtensionEnum.Story; } }

		protected StoryProgressDataHolder dataHolder;

		public Action<StoryProgressTracker?> SendAction { get; set; }

		[SerializeField] private StoryProgressTracker? data;
		public StoryProgressTracker? Data { get { return data; } protected set { data = value; } }

		protected void Use()
		{
			if (!GetIsExtensionLoaded() || SendAction == null) { return; }
			Data = GetProgressTacker();
			if (Data == null) { return; }
			Use();
		}
		StoryProgressTracker? GetProgressTacker()
		{
			return dataHolder.GetData();
		}
	}
}