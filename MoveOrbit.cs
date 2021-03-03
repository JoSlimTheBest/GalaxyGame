
using UnityEngine;

public class MoveOrbit : GalaxyMain
{

    //скрипт устаревший, правильный ORBIT!
    public Transform aroundPoint;//вокруг какого объекта крутиться
    public float circle = 0.1f;//кругов в секунду

    public float offsetSin = 1; //если значения не 1 or -1, то будет овальное смещение
    public float offsetCos = 1;

    float dist;
    float circleRadians = Mathf.PI * 2;
    float currentAng = 0;
   

    private void Start()
    {
        //targetmass = GetComponent<Rigidbody>();
        dist = (transform.position - aroundPoint.position).magnitude;
    }

    private void FixedUpdate()
    {
        Vector3 p = aroundPoint.position;
        currentAng += circleRadians * circle * Time.deltaTime;
        p.x += Mathf.Sin(currentAng) * dist * offsetSin;
        p.y += Mathf.Cos(currentAng) * dist * offsetCos;
        transform.position = p;
        


    }
}
