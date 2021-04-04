using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_Bullet : MonoBehaviour
{
    [SerializeField] public GameObject fbx;

    float speed;
    float timer;
    private float atk;
    private GameObject OwnerObject;
    private float damage;
    private bool bIsOnDestory;
    private float destoryTimer;
    private const float M_destoryTime=1.0f;
    const float max_Time = 3.0f;



    private void Awake()
    {
         speed = 1.0f;
        timer = 0.0f;
        damage = 0.0f;
        bIsOnDestory = false;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!bIsOnDestory)
        {
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
            timer += Time.deltaTime;
            if (timer >= max_Time) { Destroy(gameObject, 0); return; }
            speed += 0.8f;

            Vector3 dest = gameObject.transform.forward * Time.deltaTime * speed;
            dest.y = 0;

            transform.position += dest;
        }
        else
        {
            destoryTimer += Time.deltaTime;
            if (destoryTimer > M_destoryTime)
                Destroy(gameObject);


        }

    }
    public void SetBulletType(GameObject obj, float Damage)
    {
        
        OwnerObject = obj;
        damage = Damage;

        if (OwnerObject.tag == "Enemy_Tag")
        {
            var ps = fbx.GetComponent<ParticleSystem>();
            var psMain = ps.main;
            psMain.startColor = Color.red;

            return;
        }

        if(OwnerObject.tag=="Player")
        {
            var ps = fbx.GetComponent<ParticleSystem>();
            var psMain = ps.main;
            psMain.startColor = Color.green;

            return;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other) return;
        if (!OwnerObject) return;

        if (other.tag == OwnerObject.tag) return;



        //other.tag;
        Debug.Log(other.name);

        var damageInteface = other.GetComponent<IDamageInterface>();
        if(damageInteface==null)
        {
            Debug.Log("fail to find damage interface");
            return;
        }

        Instantiate(fbx, gameObject.transform);

        damageInteface.TakeDamage(OwnerObject, damage);
        //  Debug.Log(OwnerObject.name + "damage is " + damage);

        gameObject.hideFlags = HideFlags.HideInInspector;

        bIsOnDestory = true;

    }


}
