using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{
	[SerializeField]
	AudioSource audioSource;
	[SerializeField]
	public float pitchMax;
	[SerializeField]
	public float pitchMin;
	[SerializeField]
	float pitchSpeed;

	int pitchIncreasing = 1;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource)
		{
			float rand = Random.value;

			audioSource.pitch += Time.deltaTime * pitchIncreasing * pitchSpeed * rand;
			if(audioSource.pitch >= pitchMax)
			{
				audioSource.pitch = pitchMax;
				pitchIncreasing = -1;
			}
			else if (audioSource.pitch <= pitchMin)
			{
				audioSource.pitch = pitchMin;
				pitchIncreasing = 1;
			}
		}
    }
}
