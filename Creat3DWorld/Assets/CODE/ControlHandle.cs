using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//input 받아서 차를 움직이게 해줄 것
public class ControlHandle : MonoBehaviour
{
    uint Gear;
    uint L_MaxGear = 6;
    float RPM;
    float Velocity;


   
    private void Awake()
    {
        Gear = 0;
        Velocity = 0.0f;
        RPM = 0.0f;

        

    }
    // Start is called before the first frame update
    void Start()
    {
        //Input Event Mapping
    }

    // Update is called once per frame
    void Update()
    {

        switch (Input.inputString)
        {
            case "A":
                
                break;

            case "S":
                
                break;

            case "D":
                
                break;
            case "W":
                break;
                
        }
    }

    
    


}
