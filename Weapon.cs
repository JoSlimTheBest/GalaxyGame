using UnityEngine.UI;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public GameObject BulletPrefab; //префаб пули
	public float BulletVelocity = 20f; //скорость пули
    public int weapon = 0; //переключение пули

    public bool usingGravityGun = false;
    public GameObject gravityObject; // какой объект прикрепили TODO не переводить в привате пока не починим
	


	public float impulseForce = 2500; // сила отталкивания
	public float impulseDistance = 100; // расстояние с которого можно толкнуть объект
	public float maxImpulseMass = 105; // максимальная масса объекта, который можно толкнуть
	public float gravityForce = 50; // сила с которой будем притягивать объект
	public float gravityDistance = 100; // расстояние с которого можно притянуть объект
	public float maxGravityMass = 100; // максимальная масса объекта, который можно притянуть
	public float minDistance = 100; // дистанция с которой объект будет подхвачен гравипушкой
    public float movementSpeed = 25; // скорость движения захваченного объекта (сглаживание)
	//TODO гравитационная пушка работает не корректно!

	private Transform obj; // 
	private GameObject localPoint;
	private GameObject clone;
	private bool gravity = true;
	private bool move;
    private float curTimeout;

	public Text Gun;
	 private void Start()
    {
		Gun = (Text)GameObject.Find("Gun").GetComponent<Text>();
	}
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Q))
		{
			SwitchWeapon();
			Gun.text = weapon.ToString() + "  GUN";
		}
        if (Input.GetKey(KeyCode.E))
        {
            switch (weapon)
            {
                
                case 1: UseGravityGun(); break;

            }



        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            switch (weapon)
            {

                case 1: StopUseGravityGun(); break;

            }
        }
		if (Input.GetKeyDown(KeyCode.E))
		{
			switch (weapon)
			{
				case 0: UseGun(); break;
				case 1: StartUseGravityGun(); break;

			}
		}
	}
	private void SwitchWeapon()
	{
        switch (weapon)
        {
            case 0: weapon = 1; break;
            case 1: weapon = 0; break;
        }
       
        
			Gun.text = weapon.ToString() + "\nGUN";

		
	}

    private void UseGun()
    {


        GameObject newBullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
		newBullet.GetComponent<Damage>().damage += Random.Range(-10, 11);
        //transform.GetChild(0).gameObject.transform.Rotate(0, 10, 0, Space.Self);
		
		//Debug.Log(transform.GetChild(0).name +"     "+ transform.GetChild(0).gameObject.transform.eulerAngles);
       // newBullet.GetComponent<Rigidbody>().velocity = transform.forward * BulletVelocity;


    }

    private void StartUseGravityGun()
    {
       




		Debug.Log("используем ГРАВПуШКУ");

		Vector3 fwd = transform.TransformDirection(new Vector3(0, 100, 0)); //transform.TransformDirection(Vector3.up);



		//if (Physics.Raycast(transform.position, fwd, 20));
		RaycastHit hit;
		Ray ray = new Ray(transform.position + new Vector3(0, 2.5f, 0), fwd);
		Debug.DrawRay(transform.position, fwd, Color.red, 5f);
		Debug.Log("выстрелили лучем");
		if (Physics.Raycast(ray, out hit))
		{
			Debug.Log("попали лучем");
			if (hit.rigidbody != null)
			{
				/*  Debug.Log("Попали в объект на дистанции " + hit.distance + " с массой " + hit.rigidbody.mass);
				  if (hit.distance < impulseDistance && hit.rigidbody.mass < maxImpulseMass)
				  {
					  Debug.Log("отталкиваем объект");
					  if (obj) gravity = false;
					  //ResetObj();
					  hit.rigidbody.AddForce(ray.direction.normalized * impulseForce);
				  }*/
				// transform.GetComponent<HingeJoint>().connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();
				//hit.collider.gameObject.GetComponent<HingeJoint>().connectedBody = transform.GetComponent<Rigidbody>();
				//GameObject temp = hit.collider.gameObject;
				// temp.transform.SetParent(transform);
				//Debug.Log("захватываем : " + temp.name + " " + transform.name);
				// hit.collider.enabled = false;
				if (hit.distance < gravityDistance && hit.rigidbody.mass < maxGravityMass && !obj)
				{
					if (hit.distance > minDistance)
					{
						gravityObject = hit.rigidbody.gameObject;
						hit.rigidbody.AddForce(-ray.direction.normalized * gravityForce);
                        hit.rigidbody.isKinematic = true;
						hit.rigidbody.useGravity = false;
						usingGravityGun = true;
						Debug.Log("USINGGRAVITYGUN 2  " + usingGravityGun);
					}
					else
					{
						Debug.Log(" Захват???");
						move = true;
						obj = hit.transform;
						obj.GetComponent<Rigidbody>().Sleep();
                        obj.GetComponent<Rigidbody>().isKinematic = true;
						obj.GetComponent<Rigidbody>().useGravity = false;
						obj.GetComponent<Rigidbody>().freezeRotation = true;

						// создание пустышки, копирование трансформа и назначение родителя localPoint
						clone = new GameObject();
						clone.transform.position = obj.transform.position;
						clone.transform.rotation = obj.transform.rotation;
						clone.transform.parent = localPoint.transform;
					}
				}
			}

		}

	}
	private void StopUseGravityGun()
    {
        //usingGravityGun = false;
        if (gravityObject != null)
        {
			
            gravityObject.GetComponent<Rigidbody>().isKinematic = false;
			gravityObject.GetComponent<Rigidbody>().useGravity= true;

			gravityObject.GetComponent<Rigidbody>().freezeRotation = false;
            Debug.Log("otklu4aem");
            //if (!move) ResetObj();
            move = false;
            
            gravityObject = null;
            //gravityObject.GetComponent<Rigidbody>().AddForce(0, 0, 0);
        }
        if (obj != null)
        {

            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.GetComponent<Rigidbody>().useGravity = true;
			obj = null;
		}
		Destroy(clone);
	}
	private void UseGravityGun()
	{
		if (obj) obj.GetComponent<Rigidbody>().position = Vector3.Lerp(obj.GetComponent<Rigidbody>().position, localPoint.transform.position, movementSpeed * Time.smoothDeltaTime);
		



	}
	//public Camera mainCamera; // камера персонажа от первого лица
	

	void Awake()
	{
		localPoint = new GameObject();
		localPoint.transform.parent = transform;
		localPoint.transform.up = transform.up;
		localPoint.transform.localPosition = new Vector3(0, 20, 0); // расстояние на котором держится захваченный объект
	}

	void FixedUpdate()
	{

		


	}
}
	/*void ResetObj()
	{
		if (obj)
		{
			obj.GetComponent<Rigidbody>().useGravity = true;
			obj.GetComponent<Rigidbody>().freezeRotation = false;
			obj = null;
			Destroy(clone);
		}
	}
}

	/*void Update()
	{
		//Vector3 center = new Vector3(transform.position.x , transform.position.y, 0);
		/*Vector3 up = transform.TransformDirection(new Vector3(0, 100, 0)); //transform.TransformDirection(Vector3.up);
		RaycastHit hit;
		Ray ray = new Ray(transform.position, up);
		Debug.DrawRay(transform.position, up, Color.red, 5f);
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.rigidbody) // фильтр, все объекты с физикой
			{
				if (hit.distance < impulseDistance && hit.rigidbody.mass < maxImpulseMass)
				{
					if (obj) gravity = false;
					ResetObj();
					hit.rigidbody.AddForce(ray.direction.normalized * impulseForce);
				}

				if (Input.GetMouseButton(1) && hit.distance < gravityDistance && gravity && hit.rigidbody.mass < maxGravityMass && !obj)
				{
					if (hit.distance > minDistance)
					{
						hit.rigidbody.AddForce(-ray.direction.normalized * gravityForce);
					}
					else
					{
						move = true;
						obj = hit.transform;
						obj.GetComponent<Rigidbody>().Sleep();
						obj.GetComponent<Rigidbody>().useGravity = false;
						obj.GetComponent<Rigidbody>().freezeRotation = true;

						// создание пустышки, копирование трансформа и назначение родителя localPoint
						clone = new GameObject();
						clone.transform.position = obj.transform.position;
						clone.transform.rotation = obj.transform.rotation;
						clone.transform.parent = localPoint.transform;
					}
				}
			}
		} 

		if (Input.GetMouseButtonUp(0))
		{
			gravity = true;
		}

		if (Input.GetMouseButtonUp(1))
		{
			if (!move) ResetObj();
			move = false;
		}

		if (obj)
		{
			obj.transform.rotation = clone.transform.rotation; // передача вращения из клона, чтобы объект вращался как дочерний

			float dis = Vector3.Distance(obj.transform.position, localPoint.transform.position); // дистанция между центром объекта и точкой его назначения

			// если она больше указанного значения, в течении 3 секунд, то сброс
			// это нужно на случай, когда объект "застрял в графике"
			if (dis > 0.8f)
			{
				curTimeout += Time.deltaTime;
				if (curTimeout > 3)
				{
					curTimeout = 0;
					gravity = false;
					ResetObj();
				}
			}
			else
			{
				curTimeout = 0;
			}
		}
	}
}
    }
   
            
               
    

}*/
