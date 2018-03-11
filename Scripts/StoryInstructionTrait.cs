using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DSA.Extensions.Base;

[RequireComponent(typeof(TraitedMonoBehaviour))]
[System.Serializable]
public abstract class StoryInstructionTrait : TraitBase, ISendable<StoryInstruction>
{
	public override ExtensionEnum.Extension Extension { get { return ExtensionEnum.Extension.Story; } }

	public Action<StoryInstruction> SendAction { get; set; }

	[SerializeField] private StoryInstruction data;
	public StoryInstruction Data { get { return data; } protected set { data = value; } }

	protected virtual void Use()
	{
		if (!GetIsExtensionLoaded() || SendAction == null || Data == default(StoryInstruction)) { return; }
		SendAction(Data);
	}
}

[System.Serializable]
public abstract class ReceivableStoryInstructionTrait : StoryInstructionTrait, IReceivable<StoryInstruction>
{
	public Func<StoryInstruction> ReceiveFunction { get; set; }

	protected override void Use()
	{
		Receive();
		if (Data == null) return;
		base.Use();
	}

	public void Receive()
	{
		if (!GetIsExtensionLoaded() || ReceiveFunction == null) { return; }
		Data = ReceiveFunction();
	}
}