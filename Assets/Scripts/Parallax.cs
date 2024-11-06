using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transport transport;

    [SerializeField] public GameObject cam;
    
    [SerializeField] public float parallaxEffect;
    private float length;
    private float startPos;
    private float temp;
    private float dist;

    void Awake()
    {
        transport = FindObjectOfType<Transport>();
    }

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        temp = (cam.transform.position.x * (1 - parallaxEffect));
        dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        
        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
