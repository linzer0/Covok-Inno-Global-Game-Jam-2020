using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Lenin : MonoBehaviour
{
	private static Lenin instance;

	[SerializeField]
	private Animator animator;
	[SerializeField]
	private Text text;
	[SerializeField]
	private AudioSource source;
	[SerializeField]
	private RectTransform messageRect;

	[SerializeField]
	private Message startMessage;

	[SerializeField]
	private Message[] otherMessages = new Message[0];

	private Coroutine routine = null;

	public static void TryPlayMessage(string name)
	{
		if(instance == null) return;
		var variants = System.Array.FindAll(instance.otherMessages, m => m.name == name);
		if(variants.Length > 0) instance.TryShowMessage(variants[Random.Range(0, variants.Length)]);
	}

	public static void PlayMessage(string name)
	{
		if(instance == null) return;
		var variants = System.Array.FindAll(instance.otherMessages, m => m.name == name);
		if(variants.Length > 0) instance.SetMessage(variants[Random.Range(0, variants.Length)]);
	}

	void Awake()
	{
		instance = this;
		if(LevelLoader.isLevelLoaded) OnLevelLoaded();
		else LevelLoader.onLevelLoaded += OnLevelLoaded;
	}

	void OnDestroy()
	{
		instance = null;
		LevelLoader.onLevelLoaded -= OnLevelLoaded;
	}

	private void OnLevelLoaded()
	{
		LevelLoader.onLevelLoaded -= OnLevelLoaded;

		SetMessage(startMessage);
	}

	private void TryShowMessage(Message message)
	{
		if(routine != null) return;
		routine = StartCoroutine(ShowMessage(message));
	}

	private void SetMessage(Message message)
	{
		if(routine != null) StopCoroutine(routine);

		routine = StartCoroutine(ShowMessage(message));
	}

	private IEnumerator ShowMessage(Message message)
	{
		text.text = message.message;

		var rt = text.rectTransform;
		var min = rt.offsetMin;
		var max = -rt.offsetMax;
		messageRect.sizeDelta = new Vector2(min.x + max.x + text.preferredWidth, /*min.y + max.y + text.preferredHeight*/messageRect.sizeDelta.y);
		animator.SetBool("Showed", true);
		animator.SetTrigger(message.triggerName);
		if(message.clip != null) source.PlayOneShot(message.clip);

		yield return new WaitForSeconds(message.showTime);
		animator.SetBool("Showed", false);
		routine = null;
	}

	[System.Serializable]
	private struct Message
	{
		public string name;
		[TextArea]
		public string message;
		public float showTime;
		public AudioClip clip;
		public string triggerName;
	}
}