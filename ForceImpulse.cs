
using UnityEngine;

public class ForceImpulse : GalaxyMain
{
  
    public GameObject target;
    private bool isRotate = true; 
    private Rigidbody _rigidbody;
    public float angelPerSecond = 1f; //колво кругов в секунду

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRotate)
            Circle();
        


    }
 
    private void Circle()
    {
        transform.RotateAround(target.transform.position, Vector3.forward, angelPerSecond * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        isRotate = false;
       
    }
}
    
