using UnityEngine;

public class LevelProgress : MonoBehaviour
{
	[SerializeField]
	private LevelLoader loader;
	[SerializeField]
	private Animator animator;

	[SerializeField]
	private AudioClip[] clips;
	[SerializeField]
	private AudioSource source;

	void Awake()
	{
		loader.onStartLoad += StartLoad;
		loader.onHide += OnLevelHide;
		LevelLoader.onLevelLoaded += DropProgress;
	}

	void OnDestroy()
	{
		loader.onStartLoad -= StartLoad;
		loader.onHide -= OnLevelHide;
		LevelLoader.onLevelLoaded -= DropProgress;
	}

	public void PlayClip(AudioClip clip)
	{
		source.PlayOneShot(clip);
	}

	private void OnLevelHide()
	{
		animator.SetInteger("Progress", GameManager.GetProgress());
	}

	private void DropProgress()
	{
		animator.SetInteger("Progress", 0);
	}

	private void StartLoad()
	{
		int index = GameManager.GetProgress();
		if(index < clips.Length && clips[index] != null)
		{
			source.clip = clips[index];
			source.Play();
		}
		else source.Stop();
	}
}