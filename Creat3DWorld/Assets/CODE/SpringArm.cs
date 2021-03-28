using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    [SerializeField] public GameObject TargetObject;
    [SerializeField] public float DistanceTo;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        DistanceTo = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TargetObject) return;

        //var currentDistance =Vector3.Distance( transform.localPosition,TargetObject.transform.localPosition);
        //if(currentDistance!=DistanceTo)
        //{
        //    Vector3 n_Vector = Vector3.Normalize(transform.localPosition) * DistanceTo;

        //    transform.localPosition = n_Vector;

        //}
    }
    
   
    

}
