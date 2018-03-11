using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

[AddComponentMenu("Trait/Story/(Sequenced) Receivable Story Instruction Trait")]
[System.Serializable]
public class SequencedReceivableStoryInstructionTrait : ReceivableStoryInstructionTrait, ISequenceable
{
	[SerializeField] private int sequenceOrder;
	public int SequenceOrder { get { return sequenceOrder; } }

	public void CallInSequence(int sequenceID)
	{
		if (sequenceID == sequenceOrder)
		{
			Use();
		}
	}
}