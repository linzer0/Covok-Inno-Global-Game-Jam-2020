using UnityEngine;

public class GPDragTarget : DragTarget
{
	[SerializeField]
	private string targetType;

	[SerializeField]
	private GameObject onView;
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
			onView.SetActive(true);
			offView.SetActive(false);
		}
	}

	public override void OnDragExit()
	{
		if(isUsed) return;
		onView.SetActive(false);
		offView.SetActive(true);
	}
}