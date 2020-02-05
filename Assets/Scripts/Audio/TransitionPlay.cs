using System.Collections;
using UnityEngine;

public class TransitionPlay : MonoBehaviour
{
	[SerializeField]
	private AudioSource interruprionSource;
	[SerializeField]
	private AudioSource source1;
	[SerializeField]
	private AudioSource source2;

	private Coroutine routine = null;
	private AudioClip nextClip;
	private bool interruptionEnded;

	public void PlayTransition(AudioClip nextClip, AudioClip interrClip, float fadingTime, float increaseTime)
	{
		this.nextClip = nextClip;
		if(routine != null)
		{
			if(interruptionEnded)
			{
				StopCoroutine(routine);
				routine = null;
			}
		}
		if(routine == null)
		{
			interruptionEnded = false;
			routine = StartCoroutine(Transition(interrClip, fadingTime, increaseTime));
		}
	}

	private IEnumerator Transition(AudioClip interrClip, float fadingTime, float increaseTime)
	{
		source2.Stop();
		float time = fadingTime * source1.volume;
		while(time > 0f)
		{
			time -= Time.deltaTime;
			if(time < 0f) time = 0f;

			source1.volume = time / fadingTime;
			yield return null;
		}

		var tmp = source1;
		source1 = source2;
		source2 = tmp;

		interruprionSource.PlayOneShot(interrClip);
		yield return new WaitForSeconds(interrClip.length);
		interruptionEnded = true;

		source1.clip = nextClip;
		source1.Play();

		time = 0f;
		while(time < increaseTime)
		{
			time += Time.deltaTime;
			if(time > increaseTime) time = increaseTime;

			source1.volume = time / fadingTime;
			yield return null;
		}
		routine = null;
	}
}