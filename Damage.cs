
using UnityEngine;

public class Damage : GalaxyMain
{
    public int damage = 0;


    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int value)
    {
        damage = value;
    }
}
