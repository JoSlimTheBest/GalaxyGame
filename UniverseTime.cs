using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseTime : GalaxyMain
{
    private float time = 0; // время с начала запуска в секундах
    //TODO довести до ума часы:минуты:секунды
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        Debug.Log(time);
    }
}
