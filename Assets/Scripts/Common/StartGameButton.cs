using UnityEngine;
using UnityEngine.EventSystems;

public class StartGameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public string nextLevel;
	public AudioSource source;
	public float fadingTime = 0.5f;
	public float increaseTime = 1f;

	private bool increase = false;

	void Awake()
	{
		source.volume = 0f;
	}

	public void Play()
	{
		LevelLoader.LoadLevel(nextLevel);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		increase = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		increase = false;
	}

	void Update()
	{
		float volume = source.volume;
		if(increase)
		{
			if(volume < 1f)
			{
				volume += 1f / increaseTime * Time.deltaTime;
				if(volume > 1f) volume = 1f;

				source.volume = volume;
			}
		}
		else if(volume > 0f)
		{
			volume -= 1f / fadingTime * Time.deltaTime;
			if(volume < 0f) volume = 0f;

			source.volume = volume;
		}
	}
}
