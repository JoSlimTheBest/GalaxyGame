using UnityEngine.UI;
using UnityEngine;

public class ShipBullet : GalaxyMain
{
    private bool isActive = true; // если выключено - то не сработает ничего
    public GameObject MarkPrefab; // префаб метки при уничтожении пули

    [SerializeField] private float _speed; //скорость пули
    [SerializeField] private float _lifeTime; //жизнь пули

    private Rigidbody _rigidbody;
    public Text damage1; //отображение урона

    private void Start()
    {
        damage1 = (Text)GameObject.Find("damage").GetComponent<Text>();



        Destroy(gameObject, _lifeTime);
        

        _rigidbody = GetComponent<Rigidbody>();

        var impulse = transform.up * _rigidbody.mass * _speed;

        _rigidbody.AddForce(impulse, ForceMode.Impulse);


        
       // float damageBullet = 10f;
       // Damage damage = gameObject.GetComponent<Damage>();
       // if (damage != null)
        //    damage.SetDamage(damageBullet);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return; // если выключено - то не сработает ничего
        Debug.Log("Взаимодействуем с:" + collision.gameObject.name);
        Vector3 position = collision.contacts[0].point;
        Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal);
        GameObject newExplosion = Instantiate(MarkPrefab, position, rotation);
        
        int b = (int)transform.GetComponent<Damage>().damage;
        Destroy(gameObject, 0f);
        Destroy(newExplosion, 1f);
        isActive = false;   // отключение пули*/
        

        damage1.text = b.ToString();

        //if (collision.gameObject.name.StartsWith("BulletShip"))
        // {
        //     Physics.IgnoreCollision(collision.contacts[0].otherCollider, collision.contacts[0].thisCollider);

        //     Debug.Log("Попали в пулю:" + collision.contacts[0].otherCollider.name + collision.contacts[0].thisCollider.name);
        //    return;

        // }
        /* Health health = collision.gameObject.GetComponent<Health>();

         if (health != null)
         {
             health.ChangeHealth(-damage);
         }
        */




        //Debug.Log("Объект который взаимодействует :" + collision.gameObject.name);

        // Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        // if (enemy)
        // {
        //    enemy.OnHit();
        // }
        /*
         Vector3 position = collision.contacts[0].point;
         Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal);

        Instantiate(MarkPrefab, position, rotation);
        */
    }
    void FixedUpdate()
    {
        
    }
}
