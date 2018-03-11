using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

[System.Serializable]
public class StagePoint : DataItem
{
	[SerializeField] private string name;
	public override string Text
	{
		get
		{
			if (isCompleted == true)
			{
				string strikeThroughText = GetStrikeThroughText(name);
				return strikeThroughText;
			}
			return name;
		}
	}

	[HideInInspector] [SerializeField] private bool isCompleted;
	public bool IsCompleted { get { return isCompleted; } }

	[SerializeField] private bool isVisibleInJounral = true;
	public bool IsVisibleInJounral { get { return isVisibleInJounral; } }

	[SerializeField] private int maxValue = 1;
	[HideInInspector] [SerializeField] private int currentValue;

	[HideInInspector] [SerializeField] private int id;
	public override int ID { get { return id; } }

	[SerializeField] private string serializedUniqueIDPrefix = "storyStagePoint";
	protected override string uniqueIDPrefix { get { serializedUniqueIDPrefix = "storyStagePoint"; return serializedUniqueIDPrefix; } }

	public StagePoint(string sentData)
	{
		name = sentData;
	}

	public void Increase()
	{
		if (isCompleted == true) return;
		currentValue++;
		if (currentValue >= maxValue) isCompleted = true;
	}

	private string GetStrikeThroughText(string sentText)
	{
		string newText = "";
		foreach (char c in sentText)
		{
			try
			{
				newText = newText + c + '\u0336';
			}
			catch (System.Exception e) { e.ToString(); }
		}
		return newText;
	}

	public override List<string> GetUniqueIDs()
	{
		List<string> tempList = GetNewStringList();
		tempList.Add(uniqueID);
		return tempList;
	}

	public override void SetUniqueID(IProvider<string, string, string> sentProvider)
	{
		uniqueID = sentProvider.GetItem(uniqueID, uniqueIDPrefix);
	}
}
