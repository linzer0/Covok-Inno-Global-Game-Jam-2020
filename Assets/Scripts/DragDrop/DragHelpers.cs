using UnityEngine;

public interface IDragSource
{
	Sprite icon { get; }
}

public interface IDragTarget
{
	void OnDragDrop(IDragSource source);
	void OnDragExit();
}