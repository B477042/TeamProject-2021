using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABullet : MonoBehaviour
{
    private float z_pos;


    // Start is called before the first frame update
    void Start()
    {
        z_pos = 0.0f;
       // Debug.Log("bullet test");
    }

    // Update is called once per frame
    void Update()
    {
        z_pos += 0.2f;
        transform.Translate(0.0f, 0.0f, z_pos * Time.deltaTime);

     //   Debug.Log(z_pos);

        if (z_pos > 240.0f)
        {
            // Kills the game object in 0 seconds 
            Destroy(gameObject, 0);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BulletCollider "+other.name);
        if (other.gameObject.tag == "Enemy_Tag2")
        {

            Debug.Log("collision");
            Destroy(gameObject, 0);
            Destroy(other.gameObject, 0);
            //Instantiate(fx_obj, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }


}
