using System.Collections;
using UnityEngine;

public class PlaceLeninGameplay : MonoBehaviour
{
	[SerializeField]
	private GPDragSource[] sources;
	[SerializeField]
	private string nextLevel = "";
	[SerializeField]
	private float endGameWait = 5f;

	[SerializeField]
	private AudioClips[] transitions;
	[SerializeField]
	private AmbientTransitionPlay play;
	[SerializeField]
	private float fadingTime = 1f;
	[SerializeField]
	private float interruptionTime = 1f;
	[SerializeField]
	private float ambientFade = 1f;

	private int targetCount;
	private int index = 0;

	void Awake()
	{
		targetCount = sources.Length;
		for(int i = 0;i < sources.Length;i++)
		{
			sources[i].onUsed += OnSourceUse;
		}
	}

	private void OnSourceUse(IDragSource source)
	{
		var ds = source as GPDragSource;
		ds.onUsed -= OnSourceUse;
		Destroy(ds.gameObject);

		targetCount--;

		if(index < transitions.Length) play.PlayTransition(transitions[index].clip,
				transitions[index].interruptClips[Random.Range(0, transitions[index].interruptClips.Length)], fadingTime, interruptionTime, ambientFade);
		index++;

		if(targetCount <= 0) StartCoroutine(GameEnd());
	}

	private IEnumerator GameEnd()
	{
		Lenin.PlayMessage("GameEnd");
		yield return new WaitForSeconds(endGameWait);
		GameManager.AddProgress();
		LevelLoader.LoadLevel(nextLevel);
	}

	[System.Serializable]
	private struct AudioClips
	{
		public AudioClip clip;
		public AudioClip[] interruptClips;
	}
}