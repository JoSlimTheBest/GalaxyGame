
using UnityEngine;

public class Asteroid : GalaxyMain
{

    public GameObject meteorPrefab;  //префаб астероида
    public GameObject explosPrefab; //префаб метки столкновения с объектом
    private int damageAsteroid = 30;  //урон при столкновении
    
    void Start()
    {

        Health health = gameObject.GetComponent<Health>();
        // Debug.Log("Проверяем ХП"+ gameObject.name);
        if (health == null)
        {
            Invoke("SetHealth", 2f);    //задержка в добавлении ХП

        }
        //Debug.Log("Время прошло - держи ХП ");
        Vector3 center = transform.position;
        
        Damage damage = gameObject.GetComponent<Damage>();   
        if (damage != null)
            damage.SetDamage(damageAsteroid);
    }
    void SetHealth()
    {
        gameObject.AddComponent<Health>();
    }
    public void DestroyAsteroid()
    {
        for (int i = 0; i < 2; i++)
            SpawnMeteor(transform.position);
        GameObject explos = Instantiate(explosPrefab, transform.position, transform.rotation); //создание метки столкновения
        //gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000, transform.position, 10, 0f);
        Destroy(explos, 2f);   //уничтожение метки
        Destroy(gameObject, 0f);  
        
    }
    private void SpawnMeteor(Vector3 center)
    {

        // создание объектов при разрушении
        Vector3 pos = RandomCircle(center, Random.Range(1, 5));
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, center - pos);
        GameObject meteor = Instantiate(meteorPrefab, pos, rot);
        meteor.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, 10, 0f, ForceMode.Impulse);
        Debug.Log(meteor.name + "Zaspaunilsya" );
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
