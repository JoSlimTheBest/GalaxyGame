using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : GalaxyMain
{
    public Dictionary<string, float> resources = new Dictionary<string, float>(); //TODO доработать как будет известен БАланс игры

    private void Awake()
    {
        resources.Add("Ferum", 0);
        resources.Add("Gold", 0);

    }
    
    public void DestroyResource()
    {
        Destroy(gameObject, 0f);
    }
}
