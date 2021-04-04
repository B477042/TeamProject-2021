using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APlayer : MonoBehaviour
{
    //private float x_pos = 0.0f;
    //private float y_pos = 0.0f;
    private float moving_speed = 10.0f;
    private float rot_speed = 200.0f;
    [SerializeField] GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
      //  Debug.Log("hi");
    }

    // Update is called once per frame
    void Update()
    {
        playerMovingContorl();
        playerRotation();
        Shoot();
    }
    private void playerMovingContorl()
    {
        float keyHorizontal = Input.GetAxis("Horizontal");
        float keyVertical = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * this.moving_speed * Time.smoothDeltaTime * keyHorizontal, Space.World);
        transform.Translate(Vector3.up * this.moving_speed * Time.smoothDeltaTime * keyVertical, Space.World);

    }
    private void playerRotation()
    {

        //keybord input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");



        h = h * this.rot_speed * Time.deltaTime;
        v = v * this.rot_speed * Time.deltaTime;


        transform.Rotate(Vector3.back * h);
        transform.Rotate(Vector3.right * v);


    }
    void Shoot()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("space pressed");
            Instantiate(obj, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

        }

    }


}
