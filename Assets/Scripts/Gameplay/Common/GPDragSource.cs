public class GPDragSource : DragSource
{
	public string sourceType;

	public System.Action<IDragSource> onUsed;

	public void Used()
	{
		if(onUsed != null) onUsed.Invoke(this);
	}
}