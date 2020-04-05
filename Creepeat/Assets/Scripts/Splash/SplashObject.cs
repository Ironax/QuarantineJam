using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashObject : MonoBehaviour
{
    [SerializeField]
    GameObject FlourEffect;
    private bool isEnable = true;
    private float currentLifeTime;
    ParticleSystem particleSystem;
    // Start is called before the first frame update
    private void Awake()
    {
        particleSystem = FlourEffect.GetComponent<ParticleSystem>();
    }
    void Start()
    {
        //SetSplash();
        FlourEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= particleSystem.duration && !isEnable)
        {
            FlourEffect.SetActive(false);
            isEnable = true;
        }

    }


    public void PaintOn()
    {
        FlourEffect.SetActive(true);
        isEnable = false;
        currentLifeTime = 0.0f;
    }
}

