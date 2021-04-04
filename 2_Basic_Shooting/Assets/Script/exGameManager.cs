using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exGameManager : MonoBehaviour
{

    public GameObject obj;
    private int counter;


    // Start is called before the first frame update
    void Start()
    {
        counter = 0;

    }

    // Update is called once per frame
    void Update()
    {
        int a;

        ++counter;

        a = counter % 100;
        if (a == 1)
        {
            for (int i = 0; i < 5; i++)
            {

                float randomX = Random.Range(-5f, 5f);

                //·£´ý ÁÂÇ¥·Î ½ºÆù ½ÃÅ²´Ù
                Instantiate(obj, new Vector3(randomX, 0.0f,25.0f), 
                    Quaternion.identity);

            }
        }


    }
}
