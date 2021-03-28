using UnityEngine;
using UnityEngine.SceneManagement;



enum AccelDirection
{
    Forward, Backward, 
}
enum RotationDirection
{
   None, Left,Right
}


//input 받아서 차를 움직이게 해줄 것
public class ControlHandle : MonoBehaviour
{
    uint Gear;
    uint L_MaxGear = 6;
    float RPM;
    float Velocity;
    float HandleAngle;
    const float MaxVelocity = 0.7f;
    const float MinVelocity = 0;
    //W 가속, 자연 감속에 이용
    bool bIsAccelerating;
    //W,S 조작. 후진
    AccelDirection accelDirection;
    //A,D 차량 회전
    bool bIsRotating;
    //회전방향
    RotationDirection rotationDirection;

    //Shift 조작 
    bool bIsBreaking;

    float Acceleration;
    
    //RigidBody
    Rigidbody rgbody;

    [SerializeField]
    public Vector3 StartingPoint = Vector3.zero;
   


    private void Awake()
    {
        Gear = 0;
        Velocity = MinVelocity;
        RPM = 0.0f;
        HandleAngle = 0.2f;
        bIsAccelerating = false;
        bIsRotating = false;
        bIsBreaking = false;
        Acceleration = 0.001f;
        accelDirection = AccelDirection.Forward;
        rotationDirection = RotationDirection.None;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Input Event Mapping

        rgbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
        //Debug.Log("Actor Forward Vector : " + transform.forward);
        //Debug.Log("Actor Right Vector : " + transform.right);
        //Debug.Log("Actor Up Vector : " + transform.up);
     

        if (Input.GetKeyDown(KeyCode.W))
        {
            bIsAccelerating = true;
          //  Debug.Log("ff");
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            bIsAccelerating = false;
           // Velocity = MinVelocity;
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            bIsAccelerating = true;
            accelDirection = AccelDirection.Backward;
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            bIsAccelerating = false;
            accelDirection = AccelDirection.Forward;
        }


        if(Input.GetKeyDown(KeyCode.A))
        {
            rotationDirection = RotationDirection.Left;
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            rotationDirection = RotationDirection.None;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            rotationDirection = RotationDirection.Right;
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            rotationDirection = RotationDirection.None;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            //gameObject.transform.position = StartingPoint;
            //ResetInputActionParams();
            //gameObject.transform.rotation = StartingRotation;

            // SceneManager.LoadScene("SampleScene");
            // UnityEngine.SceneManagement.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        

       Accereate();
       Deceleration();
       RotateCar();
    }

    
    
    //전 후 가속함수
    void Accereate()
    {

        if (!bIsAccelerating) return;
        //가속도 계산

        //전진
        //가속 방향이 forward라면
        if(accelDirection==AccelDirection.Forward)
        {
            Velocity += Acceleration*0.8f;
             if (Velocity > MaxVelocity) Velocity = MaxVelocity;
      
        }

        //후진
        //s가 눌렸다면 가속 방향을 변경 시켜준다
        if (accelDirection == AccelDirection.Backward)
        {
            Velocity -= Acceleration*0.5f;
            if (Velocity < -MaxVelocity*0.5f) Velocity = -MaxVelocity*0.5f;
        }


        var MovingPoint = gameObject.transform.forward * Velocity;
        //이동
       // gameObject.GetComponent<Rigidbody>().AddForce(MovingPoint,ForceMode.Force);

        gameObject.transform.position += MovingPoint;
      //  gameObject.transform.Translate(MovingPoint);
        //print("Accel");

    }
    //마찰 감속함수
    void Deceleration()
    {
        if (bIsAccelerating) return;
        //Velcoity의 절대값이 MinVelocity(0)이 되면 멈춘다
        if (Mathf.Abs(Velocity) <= MinVelocity) { Velocity = MinVelocity; return; }
        //if (Velocity <= MinVelocity) {  return; }
       

        //Velocity가 양수였다면
        if(Velocity>0)
        {

           Velocity -= Acceleration;
           gameObject.transform.position += gameObject.transform.forward * Velocity;
            return;
        }

        //Velocity가 음수라면
        if(Velocity<0)
        {
            Velocity += Acceleration;

            gameObject.transform.position += gameObject.transform.forward * Velocity;

            return;
        }

        //print("Decelerated");

    }
    //회전 함수
    void RotateCar()
    {
        //1초에 45도가 회전 되게
        //Vector3 RotateValue=new Vector3(0,Time.deltaTime*45,0);

        Vector3 RotateValue = new Vector3(0, 1.07f, 0);

        print(Time.deltaTime);

        switch (rotationDirection)
        {
            case RotationDirection.None:
               
                break;

            case RotationDirection.Left:
                gameObject.transform.Rotate(-RotateValue);
              
                break;

            case RotationDirection.Right:
                gameObject.transform.Rotate(RotateValue);

                break;
        }
    }

    void ResetInputActionParams()
    {
        Gear = 0;
        Velocity = MinVelocity;
        RPM = 0.0f;
        HandleAngle = 0.2f;
        bIsAccelerating = false;
        bIsRotating = false;
        bIsBreaking = false;
        Acceleration = 0.001f;
        accelDirection = AccelDirection.Forward;
        rotationDirection = RotationDirection.None;
    }


}
