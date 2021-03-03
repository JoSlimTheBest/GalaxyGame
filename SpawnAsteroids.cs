
using UnityEngine;

public class SpawnAsteroids : GalaxyMain // , IHealth
{

    public int numObjects = 100; //кол-во  новых объектов
    public GameObject asteroidPrefab; // Object который хотим заспаунить
    public float distance = 100.0f; //минимальная дистанция
    public float distance2 = 100.0f; //максимальная дистанция
    //private Health health;  
    //public GameObject meteorPrefab;
    

    private void Start()
    {
        //health = new Health();
        //this.gameObject.AddComponent<Health>();

        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
            SpawnAsteroid(center);

       

    }
    private void SpawnAsteroid(Vector3 center) 
    { 
            
        
            Vector3 pos = RandomCircle(center, Random.Range(distance, distance2));
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, center - pos);
            Instantiate(asteroidPrefab, pos, rot);
           

    }


    
   

   

 
}
    

   

