using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScript : MonoBehaviour
{
    int a=10;
    int b=5;
    int c;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //c=a+b;
        //Debug.Log("c is "+c);
       // Debug.Log("hung boo ga gi ga mak hyu hung boo ga gi ga mak hyu");
        c=Add(a,b);
        Debug.Log(c);

    }

    int Add(int a, int b)
    {
        return a+b;
    }

    
}
