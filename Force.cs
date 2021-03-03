
using UnityEngine;

public class Force : GalaxyMain
{
    private Rigidbody _rigidbody;
    public float forceHoriz = 1f;
    public float forceVert = 1f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _rigidbody.AddForce(forceHoriz, forceVert, 0f);
    }
}
