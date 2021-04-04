using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageInterface
{
     float TakeDamage(GameObject Other, float Damage);
}


public class HW_Enemy : MonoBehaviour,IDamageInterface
{
    //private float detectRadius;
    private float min_Range;
    private HW_Player Target;
    private float speed;
    private float fireTimer;
    private float rotateTimer;
    private float hp;
    private float damage;
    private const float max_fireTime=0.6f;
    private const float max_rotateTime = 0.01f;
    public static LinkedList<HW_Enemy> LEneimies = new LinkedList<HW_Enemy>();
    [SerializeField] public HW_Bullet Bullet;


    private void Awake()
    {
        //detectRadius = 10.0f;
        min_Range = 0.0f;
        LEneimies.AddLast(this);
        fireTimer = 0.0f;
        rotateTimer = 0.0f;
        speed = 0.3f;
        hp = 10.0f;
        damage = 5.0f;
        Target = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        min_Range = Random.Range(2.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, transform.forward * 10f, Color.cyan);
        scanAround();
        rotateToTarget();
        moveToTarget();
        fire();
    }

    bool scanAround()
    {
        if (Target)
        {
            print("Target on");
            return false;
        }
        var LPlayer = GameObject.FindObjectsOfType<HW_Player>();
        if (LPlayer.Length == 0)
        {
            print("player list is null");
            return false;
        }


        //Palyer들을 탐색. 현재 위치와 가장 가까운 player가 타겟이 된다
        float dist_player = Mathf.Infinity;
        foreach(var it in LPlayer)
        {
            float tempDist = Vector3.Distance(gameObject.transform.position, it.transform.position);
            //새로 탐색한 것과 거리가 더 가깝다면, 우선 순위로 타겟으로 삼는다
            if (dist_player > tempDist)
            { 
                dist_player= tempDist;
                Target = it;
            }
        }
        return true;

    }
    bool rotateToTarget()
    {
        if (!Target) return false;
        Vector3 forward = gameObject.transform.forward;
        Vector3 to_target = Target.transform.localPosition - gameObject.transform.localPosition;

        float angle = Vector3.SignedAngle(forward, to_target,Vector3.up);
        

        rotateTimer += Time.deltaTime;
        if (rotateTimer < max_rotateTime) return false;
        rotateTimer = 0.0f;

       // print("Angle is " + angle);
        if (Mathf.Abs( angle) < 10.0f) {  return false; }
        transform.Rotate(Vector3.up, angle);
       // transform.Rotate(0.0f,angle,0,0f);
        return true;
    }
    bool moveToTarget()
    {
        if (!Target) return false;
        //타겟 전방 min_range까지 접근
        float to_target = Vector3.Distance(gameObject.transform.position, Target.transform.position);
        if (to_target < min_Range) return false;


        //transform.position = Target.transform.position * Time.deltaTime * speed*1.5f;
        transform.position = Vector3.Lerp(gameObject.transform.position, Target.transform.position, Time.deltaTime * speed*1.5f);
        return true;
    }
    bool fire()
    {
       
        if(fireTimer!=0.0f)
        {
            fireTimer += Time.deltaTime;
            if(fireTimer>max_fireTime)
            {
                fireTimer = 0.0f;
                return false;
            }

            return false;
        }

        if (!Target) return false;

        fireTimer += Time.deltaTime;

        Vector3 fire_point = gameObject.transform.position + gameObject.transform.forward * 1.1f;
        // 앞에 총알 생성
        var bullet = Instantiate<HW_Bullet>(Bullet, fire_point, gameObject.transform.rotation) ;
       // Debug.Log(gameObject.name + "damage is " + damage);
        bullet.SetBulletType(gameObject, damage);


        return true;
    }
    //return damage
    public float TakeDamage( GameObject OtherObject,float Damage)
    {



        print(OtherObject.name + "-------" + Damage + "-----> " + gameObject.name);

        hp -= Damage;

        //사망처리
        if (hp <= 0)
        {

            LEneimies.Remove(this);

            Destroy(gameObject, 0);
            return Damage;
        }
        return Damage;
    }
    
}
