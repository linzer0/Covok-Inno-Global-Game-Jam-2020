using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;

	private int progressCounter = 0;

	public static void Init(GameManager prefab)
	{
		if(instance != null)
		{
			instance.progressCounter = 0;
			return;
		}
		instance = Instantiate(prefab);
		DontDestroyOnLoad(instance.gameObject);
	}

	public static void AddProgress()
	{
		instance.progressCounter++;
	}

	public static int GetProgress()
	{
		return instance.progressCounter;
	}
}