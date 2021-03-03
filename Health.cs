
using UnityEngine;

//public interface IHealth { public void ChangeHealth(float value);}
public class Health : GalaxyMain // , IHealth

{
    
    public int health = 100; //кол-во жизни
    int armor = 0; //кол-во брони TODO  пока не реализовано!
    int armorStrenght = 1; //TODO переменная для зависимости брон
   // bool godMode = true;
    void Start()
    {
        // где то в момент получения урона и включения неуязвимости
        // godMode = true;
        // Invoke("OffGodMode", 2f);
        ////
       
        //Debug.Log("объект получил здоровье");


    }
    //void OffGodMode()
    // {
    //    godMode = false;
    //}
    public virtual void ChangeHealth(int value)
    {

        if (armor > 0 && value < 0)
        {
            value -= armor * armorStrenght;
            if (value > 0)
                value = 0;
        }

        health += value;
        //Debug.Log(health);
        if (health <= 0)
        {

            Asteroid asteroid = gameObject.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                asteroid.DestroyAsteroid();
                //Debug.Log("уничтожили астероид");
                return;
            }

            Meteor meteor = gameObject.GetComponent<Meteor>();
            if (meteor != null)
            {
                meteor.DestroyMeteor();
                //Debug.Log("уничтожили meteor");
                return;
            }

            MainPlayer mainPlayer = gameObject.GetComponent<MainPlayer>();
            if (mainPlayer != null)
            {
                mainPlayer.DestroyMainPlayer();
               // Debug.Log("уничтожили mainPlayer");
                return;
            }
            Resource resource = gameObject.GetComponent<Resource>();
            if (resource != null)
            {
                resource.DestroyResource();
               // Debug.Log("уничтожили астероид");
                return;
            }





        }

    }
    public void SetHealth(int value)
    {
        health = value;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Health health = gameObject.GetComponent<Health>();
        Damage damage = collision.gameObject.GetComponent<Damage>();
        if (health != null && damage != null)
        {
           
            health.ChangeHealth(-damage.GetDamage());
           

        }
    }
   
    void Update()
    {
      
    }
}
