using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : GalaxyMain
{
    public Dictionary<string, float> resources = new Dictionary<string, float>();  //TODO ресурсы не доработаны! Лист будет зависить от проработки баланса
    

       
    private void Start()
    {
        resources.Add("Ferum", 0);
        resources.Add("Gold", 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Resource>())
        {
            // Dictionary<string, float> temp = collision.gameObject.GetComponent<Resource>().resources;
            foreach (KeyValuePair<string, float> tempResource in collision.gameObject.GetComponent<Resource>().resources)
            {

                resources[tempResource.Key] += tempResource.Value;



            }
            Destroy(collision.gameObject, 0.01f);

            foreach(KeyValuePair<string,float> temp in resources)
            Debug.Log("KOLI4ESTVO "+ temp.Value);

        }
    }
    public void DestroyMainPlayer()
    {

        transform.position = new Vector3(250, 615, 0);
        Health health = gameObject.GetComponent<Health>();
        if (health != null)
        health.SetHealth(100);
    }
    void SetHealth()
    {
        gameObject.AddComponent<Health>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            DestroyMainPlayer();
        }
    }
        
}
