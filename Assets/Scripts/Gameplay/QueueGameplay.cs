using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueGameplay : DragTarget
{
	[SerializeField]
	private Animator animator;

	private int index = 0;

	public override void OnDragDrop(IDragSource source)
	{
		if(source is GPDragSource && (source as GPDragSource).sourceType == index.ToString())
		{
			index++;
			animator.SetInteger("Action", index);
			Lenin.TryPlayMessage("GoodQueue");
		}
		else Lenin.TryPlayMessage("WrongQueue");
	}

	public override void OnDragExit() { }

	protected override void OnDragEnter(IDragSource source) { }
}