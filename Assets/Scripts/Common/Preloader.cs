using UnityEngine;

public class Preloader : MonoBehaviour
{
	public GameManager gameManager;

	void Awake()
	{
		GameManager.Init(gameManager);
	}
}