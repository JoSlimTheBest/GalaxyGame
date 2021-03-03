
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveController : GalaxyMain
{
    private Rigidbody _rigidbody;
    public float Speed = 5f; //скорость движения по оси У 
    public float RotationSpeed = 1f; //скорость поворота
    public Rigidbody TargetRigidbody;
    public float ForceValue = 200f;  // Сила дополнительного ускорения через левый АЛьт
    public float Fuel = 100f;  // Топливо, при 0 не двигаемся
    public Slider slider; //слайдер UI для отображения на экране
    public GameObject Sun; 
    public float solarPowerDistance = 1000f; // топливо восполняеться при дистанции не превышающей данного значения
    public float fuelRefresh = 1f; // скорость восполнения топлива
    public Text fuelPrecent; // процент топлива, вывод на экран
    public Text SunDistance; //вывод дистанции до солнца
    public float distance;
    public float fuelVertical = 1f; //кол-во затрат топлива при движении ось У
    public float fuelHorizontal = 0.5f; // кол-во затраты топлива при повороте
    public  float fuelTurboAcceleration = 10f; //затраты топлива при ускорении через левыйАльт
    private const float FUEL_CUP = 100f; //максимальное значение топлива
    
    private ParticleSystem.EmissionModule emisionBackTurboLeft;
    private ParticleSystem.EmissionModule emisionBackTurboRight;
    private ParticleSystem.EmissionModule emisionLeftTurbo;
    private ParticleSystem.EmissionModule emisionRightTurbo;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        // ParticleSystem.EmissionModule emisionBackTurboLeft;
        emisionBackTurboLeft = gameObject.transform.Find("BackTurboLeft").GetComponent<ParticleSystem>().emission;
        emisionBackTurboLeft.enabled = false;

        // ParticleSystem.EmissionModule emisionBackTurboRight;
        emisionBackTurboRight = gameObject.transform.Find("BackTurboRight").GetComponent<ParticleSystem>().emission;
        emisionBackTurboRight.enabled = false;


        emisionLeftTurbo = gameObject.transform.Find("LeftTurbo").GetComponent<ParticleSystem>().emission;
        emisionLeftTurbo.enabled = false;

        emisionRightTurbo = gameObject.transform.Find("RightTurbo").GetComponent<ParticleSystem>().emission;
        emisionRightTurbo.enabled = false;

        fuelPrecent = (Text)GameObject.Find("FuelText").GetComponent<Text>();
        SunDistance = (Text)GameObject.Find("DD").GetComponent<Text>();


    }

    private void DisableTurbo()
    {
        emisionBackTurboRight.rate = 30;
        emisionBackTurboLeft.rate = 30;
        emisionBackTurboRight.enabled = false;
        emisionBackTurboLeft.enabled = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        

        if (Fuel > 0)
        {
            float sideForce = Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime;
            float moveForce = 0f;
            if (Input.GetAxis("Vertical") != 0) //|| Input.GetAxis("Mouse Y") != 0)
            {

                if (Input.GetAxis("Vertical") > 0)
                {
                    emisionBackTurboRight.enabled = true;
                    emisionBackTurboLeft.enabled = true;
                    moveForce = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
                    Fuel -= 1f * Time.deltaTime;
                }
                else
                {
                    emisionLeftTurbo.enabled = true;
                    emisionRightTurbo.enabled = true;
                    moveForce = Input.GetAxis("Vertical") * Speed * Time.deltaTime * 0.25f;
                    Fuel -= 0.25f * Time.deltaTime;
                }

                

            }
            else
            {
                emisionBackTurboRight.enabled = false;
                emisionBackTurboLeft.enabled = false;
            }


            if (Input.GetAxis("Horizontal") != 0) // || Input.GetAxis("Mouse X") != 0)
            {
                Fuel -= 0.25f * Time.deltaTime;
                if (Input.GetAxis("Horizontal") > 0)
                {
                    emisionLeftTurbo.enabled = true;
                    emisionRightTurbo.enabled = false;
                }

                else
                {
                    emisionRightTurbo.enabled = true;
                    emisionLeftTurbo.enabled = false;
                }
                   

                
            }
                else if (Input.GetAxis("Vertical") >= 0)
                {
                    emisionLeftTurbo.enabled = false;
                    emisionRightTurbo.enabled = false;
                }
            
            
            if (Fuel < 0)
                Fuel = 0;




            
            _rigidbody.AddRelativeForce(0f, moveForce, 0f);
            _rigidbody.AddRelativeTorque(0f, 0f, -sideForce);
        }

        FuelRefresh();
        PrecentOnDisplay();
        fuelPrecent.text = Fuel.ToString("0.00") + "\nFuel";
        
        SunDistance.text = distance.ToString("0.0") + "\nDistance";
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftAlt) && Fuel >= fuelTurboAcceleration)               //если нажата кнопка "пробел" и игрок на земле
        {
            TargetRigidbody.AddRelativeForce(Vector2.up * ForceValue, ForceMode.Impulse);
            Fuel -= fuelTurboAcceleration;

            emisionBackTurboRight.rate = 300;
            emisionBackTurboLeft.rate = 300;
            emisionBackTurboRight.enabled = true;
            emisionBackTurboLeft.enabled = true;

            Invoke("DisableTurbo", 2f);




        }
        slider.value = Fuel;


        /*     //Debug.Log(Fuel);
             if (Input.GetKeyDown(KeyCode.Mouse0))
             {
                 Debug.Log("нажали");

                 Vector3 fwd = transform.TransformDirection(new Vector3(0, 100, 0)); //transform.TransformDirection(Vector3.up);



                 //if (Physics.Raycast(transform.position, fwd, 20));
                 RaycastHit hit;
                 Ray ray = new Ray(transform.position, fwd);
                 Debug.DrawRay(transform.position, fwd, Color.red, 5f);
                 Debug.Log("выстрелили лучем");
                 if (Physics.Raycast(ray, out hit))
                 {
                     Debug.Log("попали лучем");
                     if (hit.collider != null)
                     {
                         // transform.GetComponent<HingeJoint>().connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();
                         hit.collider.gameObject.GetComponent<HingeJoint>().connectedBody = transform.GetComponent<Rigidbody>();
                         //GameObject temp = hit.collider.gameObject;
                         // temp.transform.SetParent(transform);
                         //Debug.Log("захватываем : " + temp.name + " " + transform.name);
                         // hit.collider.enabled = false;
                     }
                 }


             }*/
    }
    public float Distance()
    {
        distance = Vector3.Distance(_rigidbody.transform.position, Sun.transform.position);
        //Debug.Log(distance.ToString());
        return distance;

    }
    private void PrecentOnDisplay()
    {

        fuelPrecent.text = Fuel.ToString("0.0") + "Fuel";
        SunDistance.text = distance.ToString("0.0") + "/nDistance";
    }
    
    private void FuelRefresh()
    {
        float distancePlayerSun = Distance();


        if (distancePlayerSun <= solarPowerDistance && Fuel <= FUEL_CUP)
        {
            Fuel += Time.deltaTime * fuelRefresh / (distancePlayerSun * distancePlayerSun);
            if (Fuel > FUEL_CUP)
                Fuel = FUEL_CUP;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
       
    }
}
