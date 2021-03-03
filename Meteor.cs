
using UnityEngine;

public class Meteor : GalaxyMain
{
    public GameObject resourcesPrefab; //Префаб ресурса, который спаунится после уничтожения метеора
    
    void Start()
    {
        Health health = gameObject.GetComponent<Health>();
        //Debug.Log("Проверяем ХП" + gameObject.name);
        if (health == null)
        {
            Invoke("SetHealth", 5f);
            
        }
        Vector3 center = transform.position;
        int damageAsteroid = 10;
        Damage damage = gameObject.GetComponent<Damage>();
        if (damage != null)
            damage.SetDamage(damageAsteroid);
    }
    void SetHealth()
    {
        gameObject.AddComponent<Health>();
    }
    public void DestroyMeteor()
    {
        for (int i = 0; i < 3; i++)
            SpawnResources(transform.position);
        Destroy(gameObject, 0f);
    }
    private void SpawnResources(Vector3 center)
    {


        Vector3 pos = RandomCircle(center, Random.Range(1, 5));
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, center - pos);
        GameObject res = Instantiate(resourcesPrefab, pos, rot);
        res.GetComponent<Resource>().resources["Ferum"] += 1;
        res.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, 10, 0f, ForceMode.Impulse);
    }
}
