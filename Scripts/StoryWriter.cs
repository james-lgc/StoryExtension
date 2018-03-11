using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DSA.Extensions.Stories.DataStructure;
using System;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories
{
	[CreateAssetMenu(menuName = "Writers/StoryWriter", fileName = "StoryWriter")]
	[System.Serializable]
	public class StoryWriter : JsonWriter<MythList>
	{
		[HideInInspector] [SerializeField] private MythList mythList;
		public MythList MythList { get { return mythList; } }

		public override void Process()
		{
			mythList.SetDefault();
			mythList.SetUniqueID(this);
			uniqueIDs = mythList.GetUniqueIDs();
			uniqueIDs.Sort();
			WriteToJson(mythList);
		}

		public override void Set()
		{
			mythList = ReadTFromJson();
		}

		public void SetTList()
		{
			mythList = base.ReadTFromJson();
		}

		private void Awake()
		{
			mythList = default(MythList);
		}
	}
}
