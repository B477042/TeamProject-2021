using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 /*
    라운드에 마다 적을 하나씩 더 스폰시킨다.
    스폰시킨 적은 이 클래스에 등록이 돼서 추적된다
    플레이어가 죽으면 끝
  */
public class HW_GameManager : MonoBehaviour
{
    [SerializeField] GameObject enemyClass;
   
    uint rounds;

    private static HW_GameManager instance = null;
    public static HW_GameManager Instance
    {
        get
        {
            return instance;
        }
    }



    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        // Debug.LogWarning("Game manger instance Called");

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        rounds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //0이 되면 라운드를 올리고 라운드 수 만큼 적을 스폰
       if(HW_Enemy.LEneimies.Count==0)
        {
            ++rounds;
            spawnEnemy();
            Debug.Log("round " + rounds + " Start!");
        }


    }

    void spawnEnemy()
    {

        float rand_x; 
        float rand_z; 
        for(int i=0;i<rounds;++i)
        {
            rand_x = Random.Range(-15.0f, 15.0f);
            rand_z = Random.Range(-15.0f, 15.0f);

            Instantiate(enemyClass, new Vector3(rand_x,0,rand_z), Quaternion.identity);
        }
       
    }

}
