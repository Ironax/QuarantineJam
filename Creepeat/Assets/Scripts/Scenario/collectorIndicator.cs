using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectorIndicator : MonoBehaviour
{
	[SerializeField]
	TextMesh text;
	[SerializeField]
	Collector collector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		text.text = collector.collected + " / 4";
    }
}
