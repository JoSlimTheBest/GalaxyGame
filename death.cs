
using UnityEngine;

public class death : GalaxyMain
{

    public float _lifeTime = 3f;
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    
}
