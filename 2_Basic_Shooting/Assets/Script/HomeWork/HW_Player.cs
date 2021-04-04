using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_Player : MonoBehaviour, IDamageInterface
{
    [SerializeField]   public GameObject Bullet;
    //private float x_pos = 0.0f;
    //private float y_pos = 0.0f;
    private float moving_speed = 10.0f;
    private float rot_speed = 200.0f;
    private float speed_rota = 5.0f;
    float damage;
    float hp;


    // Start is called before the first frame update
    void Start()
    {
        damage = 5.0f;
        hp = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);


        playerMovingContorl();
      //  playerRotation();
        Shoot();
    }

    private void playerMovingContorl()
    {
        float keyHorizontal = Input.GetAxis("Horizontal");
        float keyVertical = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * this.moving_speed * Time.smoothDeltaTime * keyHorizontal);
        transform.Translate(Vector3.forward * this.moving_speed * Time.smoothDeltaTime * keyVertical);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.up * speed_rota * mouseX);
       // transform.Rotate(Vector3.left * speed_rota * mouseY);

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
            Vector3 fire_point = gameObject.transform.position + gameObject.transform.forward * 1.0f;
            var tempObj = Instantiate(Bullet, fire_point, gameObject.transform.rotation);
            var tempComp = tempObj.GetComponent<HW_Bullet>();
            if (!tempComp)
            {
                print("Bullet init error on player");
                return;
            }

          //  Debug.Log(gameObject.name + "damage is " + damage);
            tempComp.SetBulletType(gameObject, damage);

        }

    }

   public float TakeDamage(GameObject OtherObject, float Damage)
    {
        print(OtherObject.name + "-------" + Damage + "-----> " + gameObject.name);

        hp -= Damage;

        if (hp <= 0) { Destroy(gameObject, 0); return Damage; }

        return Damage;
    }

}
