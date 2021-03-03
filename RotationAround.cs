
using UnityEngine;

public class RotationAround : GalaxyMain
{
    public Transform aroundPoint;//вокруг какого объекта крутиться
    public float circle = 1f;//кругов в секунду

    public float offsetSin = 1; //если значения не 1 or -1, то будет овальное смещение
    public float offsetCos = 1;

    float dist; //радиус
    float circleRadians = Mathf.PI * 2; //движение по радиусу
    float currentAng = 0; //текущий градус
    public Rigidbody targetmass; //Масса планеты
    public float coefMass = 0.01f; //Отношении массы к скорости вращения

    private void Start()
    {
        //targetmass = GetComponent<Rigidbody>();
        dist = (transform.position - aroundPoint.position).magnitude;
    }

    private void FixedUpdate()
    {
        Vector3 p = aroundPoint.position;
        currentAng += circleRadians * circle * Time.deltaTime * targetmass.mass * coefMass; 
        p.x += Mathf.Sin(currentAng) * dist * offsetSin;
        p.y += Mathf.Cos(currentAng) * dist * offsetCos;
        transform.position = p;

        
    }
}
