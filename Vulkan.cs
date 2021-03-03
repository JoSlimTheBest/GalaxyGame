
using UnityEngine;

public class Vulkan : GalaxyMain
{
    public GameObject Lava; //префаб лавы
    // Start is called before the first frame update
    void Start()
    {

        
    }

    private void eruption()
    {
        GameObject newBullet = Instantiate(Lava, transform.position, transform.rotation);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        
        //Invoke("eruption", 5f);
        Debug.Log("shotttt");
    }
}
