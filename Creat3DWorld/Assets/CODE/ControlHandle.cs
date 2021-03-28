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


//input �޾Ƽ� ���� �����̰� ���� ��
public class ControlHandle : MonoBehaviour
{
    uint Gear;
    uint L_MaxGear = 6;
    float RPM;
    float Velocity;
    float HandleAngle;
    const float MaxVelocity = 0.7f;
    const float MinVelocity = 0;
    //W ����, �ڿ� ���ӿ� �̿�
    bool bIsAccelerating;
    //W,S ����. ����
    AccelDirection accelDirection;
    //A,D ���� ȸ��
    bool bIsRotating;
    //ȸ������
    RotationDirection rotationDirection;

    //Shift ���� 
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

    
    
    //�� �� �����Լ�
    void Accereate()
    {

        if (!bIsAccelerating) return;
        //���ӵ� ���

        //����
        //���� ������ forward���
        if(accelDirection==AccelDirection.Forward)
        {
            Velocity += Acceleration*0.8f;
             if (Velocity > MaxVelocity) Velocity = MaxVelocity;
      
        }

        //����
        //s�� ���ȴٸ� ���� ������ ���� �����ش�
        if (accelDirection == AccelDirection.Backward)
        {
            Velocity -= Acceleration*0.5f;
            if (Velocity < -MaxVelocity*0.5f) Velocity = -MaxVelocity*0.5f;
        }


        var MovingPoint = gameObject.transform.forward * Velocity;
        //�̵�
       // gameObject.GetComponent<Rigidbody>().AddForce(MovingPoint,ForceMode.Force);

        gameObject.transform.position += MovingPoint;
      //  gameObject.transform.Translate(MovingPoint);
        //print("Accel");

    }
    //���� �����Լ�
    void Deceleration()
    {
        if (bIsAccelerating) return;
        //Velcoity�� ���밪�� MinVelocity(0)�� �Ǹ� �����
        if (Mathf.Abs(Velocity) <= MinVelocity) { Velocity = MinVelocity; return; }
        //if (Velocity <= MinVelocity) {  return; }
       

        //Velocity�� ������ٸ�
        if(Velocity>0)
        {

           Velocity -= Acceleration;
           gameObject.transform.position += gameObject.transform.forward * Velocity;
            return;
        }

        //Velocity�� �������
        if(Velocity<0)
        {
            Velocity += Acceleration;

            gameObject.transform.position += gameObject.transform.forward * Velocity;

            return;
        }

        //print("Decelerated");

    }
    //ȸ�� �Լ�
    void RotateCar()
    {
        //1�ʿ� 45���� ȸ�� �ǰ�
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
