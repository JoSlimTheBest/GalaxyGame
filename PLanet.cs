
using UnityEngine;

public class PLanet : GalaxyMain

{

    public GameObject newSun; //Префаб нового солнца
    public GameObject MarkPrefab; //Префаб метки столкновения
    private void Start()
    {


    }
    private void OnCollisionEnter(Collision collision)
    {


        Vector3 position = collision.contacts[0].point;
        Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal);

        GameObject newObject = Instantiate(MarkPrefab, position, rotation);
        newObject.transform.SetParent(transform);
        Destroy(newObject, 3f);


        transform.gameObject.GetComponent<Rigidbody>().mass += collision.gameObject.GetComponent<Rigidbody>().mass;

    }


    private void FixedUpdate()
    {
        if (transform.gameObject.GetComponent<Rigidbody>().mass > 6100)
        {
            GameObject moon = transform.parent.Find("Core Moon6").gameObject;
            Destroy(gameObject, 0f);
            GameObject newSun1 = Instantiate(newSun, transform.position,transform.rotation, transform.parent);
            transform.parent.GetComponent<RotationAround>().targetmass = newSun1.GetComponent<Rigidbody>();
            
            moon.GetComponent<RotationAround>().aroundPoint = newSun1.transform;
           
               
        }

    }
}
