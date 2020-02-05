using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DragTarget : MonoBehaviour, IDragTarget, IPointerEnterHandler, IPointerExitHandler
{
	private bool isDragEntered = false;

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		IDragSource source = DragDropManager.GetCurrentSource();
		if(source != null)
		{
			DragDropManager.SetTarget(this);
			isDragEntered = true;
			OnDragEnter(source);
		}
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		if(isDragEntered)
		{
			DragDropManager.RemoveTarget(this);
			OnDragExit();
		}
		isDragEntered = false;
	}

	protected abstract void OnDragEnter(IDragSource source);
	public abstract void OnDragDrop(IDragSource source);
	public abstract void OnDragExit();
}
