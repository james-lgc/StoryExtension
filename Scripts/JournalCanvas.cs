using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DSA.Extensions.Stories.DataStructure;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories
{
	[System.Serializable]
	public class JournalCanvas : ClickableCanvas
	{
		public override ExtensionEnum.Extension Extension { get { return ExtensionEnum.Extension.Story; } }
		//StoryNameButtons 0
		//StoryStatusButtons 1
		//StoryStageButtons 2

		private Func<StoryProgressTracker?> GetProgressFunc;

		private StoryProgressTracker currentProgress;

		public override void Initialize()
		{
			base.Initialize();
			SetSendActions<DataItem>(ProcessSelectStoryNameButton, 0);
			SetSendActions<DataItem>(ProcessClickStoryStatusButton, 1);
		}

		public void SetProgressFunc(Func<StoryProgressTracker?> sentFunc)
		{
			GetProgressFunc = sentFunc;
		}

		public override void DisplayData()
		{
			StoryProgressTracker? nullableProgress = GetProgressFunc();
			currentProgress = (StoryProgressTracker)nullableProgress;
			PrintableString[] strings = new PrintableString[2];
			strings[0] = new PrintableString("Active", 0);
			strings[1] = new PrintableString("Completed", 1);
			SetDisplayableArrayData<DataItem>(strings, 1);
			ProcessClickStoryStatusButton(strings[0]);
		}

		public void ProcessSelectStoryNameButton(DataItem sentData)
		{
			PrintableString[] printableDescription = new PrintableString[1];
			//Cast sentData as IPrintable to access explictly implemented Text property.
			IPrintable journalDescriptionPrintable = (IPrintable)sentData;
			printableDescription[0] = new PrintableString(journalDescriptionPrintable.PrintableText);
			SetDisplayableArrayData<DataItem>(printableDescription, 3);
			if (!(sentData is INestable<DataItem>)) return;
			INestable<DataItem> returnable = (INestable<DataItem>)sentData;
			SetDisplayableArrayData<DataItem>(returnable.GetArray(), 2);
		}

		public void ProcessClickStoryStatusButton(DataItem sentData)
		{
			Story[] stories;
			if (sentData.ID == 0)
			{
				stories = currentProgress.activeStories;
			}
			else
			{
				stories = currentProgress.completedStories;
			}
			SetDisplayableArrayData<DataItem>(stories, 0);
			if (stories.Length == 0)
			{
				panels[2].Clear();
				panels[0].Clear();
				panels[3].Clear();
				SetSelectedDisplayable(1, sentData.ID);
				return;
			}
			SetSelectedDisplayable(0, 0);
		}

		private void ClearStageTexts()
		{
			DataItem[] data = new DataItem[0];
			SetDisplayableArrayData<DataItem>(data, 2);
			return;
		}
	}
}