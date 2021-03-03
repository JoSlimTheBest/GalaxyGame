using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : GalaxyMain
{
    public float yourDesiredSize = 30f; // обычное расположении камеры
    public float minCamera = 15f; // приблежении камеры при залете на околоземнуюорбиту
    public float maxCamera = 30f; // отдаление камеры при вылете с орбиты
    //private bool moveCameraUp = false;
    //private bool moveCameraDown = false;
    public float speed = 0.03f; // скорость приблежния/отдаления камеры
    private string moveCamera = ""; //направление камеры
    public float newCameraPositionY; // для плавного смещения камеры
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Orbit>())
        {
            moveCamera = "Down";
            
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<Orbit>())
        {
            moveCamera = "Up";
            
        }
    }
    void FixedUpdate()
    {
        switch (moveCamera)
        {
            case "": Camera.main.orthographicSize = yourDesiredSize;
                break;
            case "Up":
                yourDesiredSize = Mathf.Lerp(yourDesiredSize, maxCamera, speed);
                //     Camera.main.transform.position = Camera.main.transform.position + new Vector3(0, 10, 0);
                // newCameraPositionY = Mathf.Lerp(Camera.main.transform.position.y, Camera.main.transform.position.y + 10, speed);
                newCameraPositionY = Mathf.Lerp(Camera.main.transform.localPosition.y, 10, speed);
                if (yourDesiredSize >= maxCamera - 0.3)
                {
                    moveCamera = "";
                    yourDesiredSize = maxCamera;

                }
                // Camera.main.transform.position.Set(0, 0, 0); //-= newCameraPositionY;  //new Vector3(0, -newCameraPositionY, 0);
                Camera.main.orthographicSize = yourDesiredSize;
                Camera.main.transform.localPosition = new Vector3(0, newCameraPositionY, -10);
                break;
            case "Down":
                yourDesiredSize = Mathf.Lerp(yourDesiredSize, minCamera, speed);
                newCameraPositionY = Mathf.Lerp(Camera.main.transform.localPosition.y, 0, speed);
                if (yourDesiredSize <= minCamera + 0.3)
                {
                    moveCamera = "";
                    yourDesiredSize = minCamera;


                }
                //Camera.main.transform.position += new Vector3(0, newCameraPositionY, 0);
                Camera.main.transform.localPosition = new Vector3(0, newCameraPositionY, -10);//Vector3.Lerp(transform.position, new Vector3(0, 0, -10), Time.deltaTime);
                
                Camera.main.orthographicSize = yourDesiredSize;
                
                break;
        }

       /* if (moveCameraDown == true)
        {
            yourDesiredSize = Mathf.Lerp(yourDesiredSize, 15, speed);
            if (yourDesiredSize == minCamera)
            {
                moveCameraDown = false;
            }
        }
        else
        {



            if (moveCameraUp == true)
            {
                yourDesiredSize = Mathf.Lerp(yourDesiredSize, 30, speed);
                if (yourDesiredSize == maxCamera)
                {
                    moveCameraUp = false;
                }
            }
        }
        Camera.main.orthographicSize = yourDesiredSize;
    */
    }

   // public virtual void ChangeCamera()
   // {
   //     yourDesiredSize = 10f;
    //}
}
    
         

    


