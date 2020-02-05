using UnityEngine;

public class LeninTarget : DragTarget
{
	[SerializeField]
	private string targetType;

	[SerializeField]
	private GameObject onView;
	[SerializeField]
	private GameObject preView;
	[SerializeField]
	private GameObject offView;

	private bool isUsed = false;

	public override void OnDragDrop(IDragSource source)
	{
		if(isUsed) return;
		if(source is GPDragSource && (source as GPDragSource).sourceType == targetType)
		{
			isUsed = true;
			onView.SetActive(true);
			offView.SetActive(false);
			preView.SetActive(false);
			(source as GPDragSource).Used();
			Lenin.TryPlayMessage("GoodPlace");
		}
		else Lenin.TryPlayMessage("WrongPlace");
	}

	protected override void OnDragEnter(IDragSource source)
	{
		if(isUsed) return;
		if(source is GPDragSource && (source as GPDragSource).sourceType == targetType)
		{
			preView.SetActive(true);
		}
	}

	public override void OnDragExit()
	{
		if(isUsed) return;
		preView.SetActive(false);
	}
}