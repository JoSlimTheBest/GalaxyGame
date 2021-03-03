using System.Collections.Generic;
using UnityEngine;

public class SpawnLava : MonoBehaviour
{
    public GameObject lavaPrefab; // Первый ОбЪект вылетающий из вулкана
    public GameObject lavaPrefab2; // Второй Объект 
    public bool spawnLava = true; 
    public float spawnTimer = 10f; //таймер перед новым спауном TODO сделать рандомным? 
    public GameObject smoke; // дым перед извержением
    private Dictionary<int, GameObject> randomLavaE;

    void Start()
    {
        randomLavaE = new Dictionary<int, GameObject>();
        randomLavaE.Add(1, lavaPrefab);
        randomLavaE.Add(2, lavaPrefab2);
    }
    void CreatorLava()
    {
        //System.Random lavaCount = new System.Random();

        for (int i = 0; i < Random.Range(2, 5); i++)
        {
           
            Invoke("SpawnSpawn", i);
        }
        
        spawnLava = true;
    }

    void SpawnSpawn()
    {

        int b = Random.Range(1, 3);


        /* if (b == 1)
         {
             randomLava = lavaPrefab;
             Debug.Log("1 variant");
         }

         else
         {
             randomLava = lavaPrefab2;
             Debug.Log("2 variant");
         }*/

        GameObject lava = Instantiate(randomLavaE[b], transform.position, transform.rotation);

        lava.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(-3, 4), 2, 0) * Random.Range(45, 80), ForceMode.Impulse);
        lava.GetComponent<Rigidbody>().AddTorque(0,0, Random.Range(-2, 3), ForceMode.Impulse);
    }
    void LavaSmoke()
    {
        GameObject smokeLava = Instantiate(smoke, transform.position, transform.rotation);
        smokeLava.transform.SetParent(transform);
        Destroy(smokeLava, 10f);
    }

    void FixedUpdate()
    {
        if (spawnLava == true)
        {
            Invoke("CreatorLava", spawnTimer);
            Invoke("LavaSmoke", spawnTimer - 4);
            spawnLava = false;
        }
        
    }

}
