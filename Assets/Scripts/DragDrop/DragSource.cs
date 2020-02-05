using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DragSource : MonoBehaviour, IDragSource, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
	[SerializeField]
	private Sprite iconSprite;

	public Sprite icon { get { return iconSprite; } }

	public void OnBeginDrag(PointerEventData eventData) { }

	public void OnDrag(PointerEventData eventData) { DragDropManager.UpdatePosition(eventData.position); }

	public void OnPointerDown(PointerEventData eventData)
	{
		eventData.dragging = true;
		eventData.pointerDrag = gameObject;
		DragDropManager.BeginDrag(this, eventData.position);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if(eventData.pointerCurrentRaycast.isValid)
		{
			var target = eventData.pointerCurrentRaycast.gameObject.GetComponent<IDragTarget>();
			if(target != null) target.OnDragDrop(this);
		}
		DragDropManager.StopDrag();
	}
}