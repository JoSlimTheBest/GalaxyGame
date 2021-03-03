
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public  class Gravity : GalaxyMain
{
    public List<Rigidbody> gravityBodies = new List<Rigidbody>(); //лист объектов которые притягиваются к планете
    private static List<Rigidbody> sunBodies = new List<Rigidbody>(); //лист объектов которые притягиваються к солнцу
    private Rigidbody componentRigidbody;
    public float G = 6.667f; //гравитационная постоянная
    public float  strenghtAccelerationMin = 0.00001f; //минимальная сила притяжения
   // public float strenghtCentrAcceleration = 1f; // изменение формулы притяжения, использовать если хотим неправдивую физику
    //static bool inPlanetGravity = false;
    //public Text CHAT;

    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody>();
        
    }

    private void OnTriggerEnter(Collider other)
    {

        // Debug.Log("ontrigerENTER: " + this.name);
        if (other.attachedRigidbody != null)
        {
            if (this.name != "Sun")
            {

                gravityBodies.Add(other.attachedRigidbody);
                sunBodies.Remove(other.attachedRigidbody);
            }
            else
            {
                // gravityBodies.Remove(other.attachedRigidbody);
                sunBodies.Add(other.attachedRigidbody);
            }

        }



        // if (this.name != "Sun" && other.name == "MainPlayer")
        // {
        //inPlanetGravity = true;

        // Debug.Log("TRUE: " + this.name );
        // }

    }

    private void OnTriggerExit(Collider other)
    {
       // Debug.Log("ontrigerEXIT: " + this.name);
        if (other.attachedRigidbody != null)
        {
           
            if (this.name != "Sun")
            {

                gravityBodies.Remove(other.attachedRigidbody);
                if (!sunBodies.Contains(other.attachedRigidbody)) //|| !gravityBodies.Contains(other.attachedRigidbody)
                {
                    sunBodies.Add(other.attachedRigidbody);
                }
               
            }
            else
            {
                
                sunBodies.Remove(other.attachedRigidbody);
            }
        }
            if (other.attachedRigidbody == null)
        {
            gravityBodies.Remove(other.attachedRigidbody);
            sunBodies.Remove(other.attachedRigidbody);

        }
       // if (this.name != "Sun" && other.name == "MainPlayer")
       // {
           // inPlanetGravity = false;
       //     Debug.Log("FALSE: " + this.name);
       // }


    }


    // Update is called once per frame
    private void FixedUpdate()

    {
        List<Rigidbody> attachedBodies;
        if (this.name == "Sun")
        {
            attachedBodies = sunBodies;
             
        }
        else
        {
            attachedBodies = gravityBodies;
        }
        
        
        for (int i = 0; i < attachedBodies.Count; i++) 
        {
            Rigidbody body = attachedBodies[i];
            

            if (body != null) // &&(inPlanetGravity == true && this.name != "Sun" ||  inPlanetGravity == false && this.name == "Sun"))

            {

                Vector3 directionToPlanet = (transform.position - body.position).normalized;

                Vector3 normal = transform.forward.normalized;
                
                Vector3 forceCentr = Vector3.Cross(directionToPlanet, normal).normalized;
               
                float distance = (transform.position - body.position).sqrMagnitude;
                float strenght = (G * body.mass * componentRigidbody.mass) /  distance;

                if (strenght < strenghtAccelerationMin)
                   strenght = strenghtAccelerationMin;

                var forceGravity = (-forceCentr + directionToPlanet) * strenght; 
                body.AddForce(forceGravity);

              // if (body.name == "MainPlayer")
                // Debug.Log("притяжение от " + transform.name + " СИЛА " + strenght+ "    Vector " + forceGravity);

               
            }
            else 
            {
               
                   // Debug.Log("Удаляем: " + i + " " + ". всего элементов : " + attachedBodies.Count);
                   
                    attachedBodies.Remove(body);
                    


                
                
                //nullBodies.Add(body);

            }
        }
        //if (nullBodies.)
    }
}
