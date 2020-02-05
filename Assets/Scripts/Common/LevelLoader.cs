using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	private const float MAX_VOLUME = 0f;
	private const float MIN_VOLUME = -80f;

	private static LevelLoader instance;

	public static event System.Action onLevelLoaded;
	public static bool isLevelLoaded { get; private set; }

	public event System.Action onStartLoad;
	public event System.Action onHide;

	[SerializeField]
	private Animator animator;
	[SerializeField]
	private float minShowTime = 5f;
	[SerializeField]
	private AudioMixer mixer;

	public static void LoadLevel(string name)
	{
		instance.StartCoroutine(LoadScene(name));
		isLevelLoaded = false;
	}

	private static IEnumerator LoadScene(string name)
	{
		if(instance.onStartLoad != null) instance.onStartLoad.Invoke();
		instance.animator.SetBool("Hidden", true);
		var showAnim = System.Array.Find(instance.animator.runtimeAnimatorController.animationClips, a => a.name == "Show");
		float len = showAnim.length;
		float time = 0f;
		while(time < len)
		{
			time += Time.deltaTime;
			if(time > len) time = len;
			instance.mixer.SetFloat("GameVolume", Mathf.Lerp(MIN_VOLUME, MAX_VOLUME, 1f - (time / len)));
			instance.mixer.SetFloat("LevelLoadVolume", Mathf.Lerp(MIN_VOLUME, MAX_VOLUME, time / len));
			yield return null;
		}

		yield return new WaitForSeconds(showAnim.length);
		if(instance.onHide != null) instance.onHide.Invoke();

		time = Time.time;
		yield return SceneManager.LoadSceneAsync(name);
		float timeLeft = instance.minShowTime - (Time.time - time);
		if(timeLeft > 0f) yield return new WaitForSeconds(timeLeft);

		instance.animator.SetBool("Hidden", false);
		var hideAnim = System.Array.Find(instance.animator.runtimeAnimatorController.animationClips, a => a.name == "Hide");

		len = hideAnim.length;
		time = 0f;
		while(time < len)
		{
			time += Time.deltaTime;
			if(time > len) time = len;
			instance.mixer.SetFloat("GameVolume", Mathf.Lerp(MIN_VOLUME, MAX_VOLUME, time / len));
			instance.mixer.SetFloat("LevelLoadVolume", Mathf.Lerp(MIN_VOLUME, MAX_VOLUME, 1f - (time / len)));
			yield return null;
		}

		if(onLevelLoaded != null) onLevelLoaded.Invoke();

		isLevelLoaded = true;
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