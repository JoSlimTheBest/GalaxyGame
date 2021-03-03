

public class SunRotation : GalaxyMain
{
    public float SpeedRotation = 1f; //Скорость вращения вокруг своей оси


    // Update is called once per frame
    private void FixedUpdate()
    {

        transform.Rotate(0,0 , SpeedRotation);

    }
}