
using UnityEngine;

public class Horizon : GalaxyMain
{

    // границы игры при выходе за неё уничтожаем все объекты
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject, 0);
        Debug.Log(other.name);
    }
}
