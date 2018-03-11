using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DSA.Extensions.Base;

namespace DSA.Extensions.Stories
{
	public class StoryStatusButton : DataUIButton
	{
		public override ExtensionEnum.Extension Extension { get { return ExtensionEnum.Extension.Story; } }
	}
}