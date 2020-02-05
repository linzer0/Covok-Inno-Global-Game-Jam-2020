using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDropManager : MonoBehaviour
{
	private static DragDropManager instance;

	[SerializeField]
	private RectTransform rect;

	[SerializeField]
	private Image dragIcon;

	private IDragSource source = null;
	private IDragTarget target = null;

	public static void BeginDrag(IDragSource source, Vector3 screenPos)
	{
		instance.source = source;

		instance.dragIcon.sprite = source.icon;
		Vector2 pos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(instance.rect, screenPos, null, out pos);
		instance.dragIcon.rectTransform.anchoredPosition = pos;
		instance.dragIcon.gameObject.SetActive(true);
	}

	public static void StopDrag()
	{
		instance.source = null;
		instance.target = null;
		instance.dragIcon.gameObject.SetActive(false);
	}

	public static void SetTarget(IDragTarget target)
	{
		if(instance.target != null) instance.target.OnDragExit();
		instance.target = target;
	}

	public static void RemoveTarget(IDragTarget target)
	{
		if(instance.target == target) instance.target = null;
	}

	public static IDragSource GetCurrentSource()
	{
		return instance.source;
	}

	public static void UpdatePosition(Vector3 screenPos)
	{
		Vector2 pos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(instance.rect, screenPos, null, out pos);
		instance.dragIcon.rectTransform.anchoredPosition = pos;
	}

	void Awake()
	{
		instance = this;
	}

	void OnDestroy()
	{
		instance = null;
	}
}