using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingBehaviour : MonoBehaviour
{
    public bool isFlashing;
    public float timeSpan;
    public Color setColor;
    Color color;
    new Renderer renderer;
    float t;
    PauseBehaviour pauseBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        pauseBehaviour = gameObject.AddComponent<PauseBehaviour>();
        renderer = GetComponent<Renderer>();
        color = renderer.material.color;
        t = timeSpan;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFlashing)
        {
            t += Time.deltaTime;
            if(t >= timeSpan)
            {
                if(renderer.material.color == setColor)
                {
                    renderer.material.color = color;
                }
                else
                {
                    renderer.material.color = setColor;
                }
                t = 0;
            }
        }
        else
        {
            t = 0;
        }
    }
}
