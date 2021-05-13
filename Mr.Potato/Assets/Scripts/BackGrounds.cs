using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] backgrounds;
    public float fparallax = 0.4f; //定义一个float
    public float layerFraction = 5f;
    public float fSmooth = 5f;
    private Transform cam;
    Vector3 previousCamPos;
    private void Awake()
    {
        cam = Camera.main.transform; //找到摄像机的位置
        
    }

    void Start()
    {
        previousCamPos = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        float fParrlaxX = (previousCamPos.x - cam.position.x) * fparallax;
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float fNewX = backgrounds[i].position.x + fParrlaxX * (1 + i * layerFraction);
            Vector3 newPos = new Vector3(fNewX, backgrounds[i].position.y);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, newPos, fSmooth * Time.deltaTime);
        }
    }
}
