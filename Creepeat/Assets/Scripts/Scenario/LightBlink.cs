using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightBlink : MonoBehaviour
{
	public float minBlinkTime = 0.2f;
	public float maxBlinkTime = 1.0f;

	public float minIntensity = 0.2f;
	public float maxIntensity = 1.0f;

	private float blinkTime = 0.0f;
	private float currentBlinkWait;
	private float currentIntensity;

	private Light light;

	void Awake()
	{
		light = GetComponent<Light>();
		currentBlinkWait = Random.Range(minBlinkTime, maxBlinkTime);
	}

	void Blink()
	{
		light.enabled = !light.enabled;
		light.intensity = Random.Range(minIntensity, maxIntensity);
	}

    void Update()
    {
	    if (blinkTime > currentBlinkWait)
	    {
		    Blink();
			currentBlinkWait = Random.Range(minBlinkTime, maxBlinkTime);
			blinkTime = 0.0f;
		}

	    blinkTime += Time.deltaTime;
    }
}
