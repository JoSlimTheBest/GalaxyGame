using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : GalaxyMain
{
    
    public float angelPerSecond = -1f;  // скорость вращения в градусах
    private List<GameObject> orbitBodies = new List<GameObject>(); // Лист объектов которые должны крутиться по орбите
    private float sinPos; // вычисление точки соприкосновения по синусу
    private float cosPos;// вычисление точки соприкосновения по косинусу
    private float radius; //радиус орбиты
    public Transform aroundPoint;//вокруг какого объекта крутиться
    public float circle = 2f;//кругов в секунду
    private float captureTime = 0f; //время удерживания на орбите, без возможности выхода из нее
    

    public float offsetSin = 1; //если значения не 1 or -1, то будет овальное смещение
    public float offsetCos = 1; //если значения не 1 or -1, то будет овальное смещение



    float currentAng; // градус обхекта на орбите

    private void Start()
    {
        
        radius = transform.GetComponent<SphereCollider>().radius;
        Vector3 center = transform.position;
    }
    private void OnTriggerEnter(Collider collider)
    {

        
     //  if (collision.gameObject.name == "MainPlayer")
      //  {
       //     collision.gameObject.GetComponent<MyCamera>().yourDesiredSize = Mathf.Lerp(15, 30, movementSpeed* Time.smoothDeltaTime);
       // }
       if (collider.gameObject.name == "MainPlayer")
        {
            captureTime = 1.5f;
            float currentAngR, currentAngU;
            orbitBodies.Add(collider.gameObject);
            collider.GetComponent<Rigidbody>().drag = 20f;
            // Vector3 center = transform.position;
            collider.gameObject.transform.SetParent(transform);
            sinPos = collider.transform.localPosition.y / collider.transform.localPosition.magnitude;
            cosPos = collider.transform.localPosition.x / collider.transform.localPosition.magnitude;
            // Vector3 pos = RandomCircle(center, radius);
            currentAngR = Vector3.Angle(Vector3.right, collider.transform.localPosition);
            currentAngU = Vector3.Angle(Vector3.up, collider.transform.localPosition);
            if (currentAngR < 90 && currentAngU < 90 || currentAngR > 90 && currentAngU < 90)
                currentAng = currentAngR;
            else if (currentAngR > 90 && currentAngU > 90 || currentAngR < 90 && currentAngU > 90)
                currentAng = 360 - currentAngR;
            


           // Debug.Log(currentAng + "  ONTRIGER ANGLE");
            
            //Quaternion rot = Quaternion.LookRotation(Vector3.forward, center - pos);
        }
    }
    /*private void OnTriggerExit(Collider collider)
    {

        orbitBodies.Remove(collider.gameObject);
        // if (collision.gameObject.name == "MainPlayer")
        // {
        //     collision.gameObject.GetComponent<MyCamera>().yourDesiredSize = Mathf.Lerp(30, 15, movementSpeed* Time.smoothDeltaTime); 
        // }

        if (collider.gameObject.name == "MainPlayer")
        {
            collider.gameObject.GetComponent<Rigidbody>().drag = 1f;

        }
    }*/

    private void Circle(GameObject orbitObject)
    {
       // Debug.Log("KRUTIMSYA PO ORBITE");
        orbitObject.transform.RotateAround(transform.position, Vector3.forward, angelPerSecond * Time.deltaTime);

    }
    void FixedUpdate()
    {
        
         

        
        for (int i = 0; i < orbitBodies.Count; i++)
        {
            GameObject body = orbitBodies[i];
            body.GetComponent<PlayerMoveController>().Fuel += 1 * Time.deltaTime;

            if (body != null) // &&(inPlanetGravity == true && this.name != "Sun" ||  inPlanetGravity == false && this.name == "Sun"))
            {
                captureTime -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.LeftAlt) && captureTime <=0 && body.GetComponent<PlayerMoveController>().Fuel > 10)
                {
                    body.GetComponent<Rigidbody>().drag = 1f;
                    body.transform.SetParent(null);
                    orbitBodies.Remove(body);
                    return;
                }
                //sinPos = collider.transform.position.y / radius;
                // cosPos = collider.transform.position.x / radius;

                Vector3 p = transform.position;
                //Debug.Log("TRANSFORM body " + p );
                currentAng -= circle*Time.deltaTime;
                p.y += Mathf.Sin(currentAng *Mathf.Deg2Rad ) * radius;
                p.x += Mathf.Cos(currentAng * Mathf.Deg2Rad) * radius;
                p.z = body.transform.position.z;

                body.transform.rotation = Quaternion.Lerp(body.transform.rotation, Quaternion.LookRotation(Vector3.forward, p - body.transform.position),Time.deltaTime);
                body.transform.position = p;
               
                //body.transform.rotation = Quaternion.LookRotation(Vector3.forward);


                // Vector3 pos = transform.position;
                //Debug.Log("SINN  = " + Mathf.Sin(currentAng) + " COS = " + Mathf.Cos(currentAng) + " radius "+ radius + " ANG " + currentAng);

                //Debug.Log("AcosPos  = " + (Mathf.Sin(Mathf.Asin(sinPos) * Mathf.Deg2Rad)) + " AsinPos = " + (Mathf.Cos(Mathf.Acos(cosPos) * Mathf.Deg2Rad)));
                // body.transform.SetParent(transform);
                //body.transform.localPosition = new Vector3(radius*Mathf.Sin(cosPos * Mathf.Deg2Rad), Mathf.Cos(sinPos * Mathf.Deg2Rad),0);
                //Debug.Log("NewcosPos  = " + Mathf.Cos((Mathf.Acos(cosPos) - 0.1f)) + " NewsinPos = " + Mathf.Sin((Mathf.Asin(sinPos) - 0.1f)));
                //body.transform.localPosition = new Vector3(radius * Mathf.Cos(Mathf.Acos(cosPos*Mathf.Deg2Rad)), radius * Mathf.Sin(Mathf.Asin(sinPos* Mathf.Deg2Rad) - 0.1f), 0);

                // pos.x =  radius * (Mathf.Sin(Mathf.Asin(sinPos * Mathf.Deg2Rad)-0.1f)) ;
                //pos.y =  radius * (Mathf.Cos(Mathf.Acos(cosPos * Mathf.Deg2Rad)- 0.1f));
                // transform.position = pos;

                //Debug.Log("KRUTIMSYA PO ORBITE");
                //Circle(body);
                //  if (body.name == "MainPlayer")
                //  Debug.Log("притяжение от " + transform.name +  " СИЛА "+ (body.velocity));
            }
            else
            {
                if (body == null)
                {
                    //Debug.Log("Удаляем: " + i + " "  + ". всего элементов : " + planetBodies.Count);
                    orbitBodies.Remove(body);

                }

                //nullBodies.Add(body);

            }


        }




    }
}
