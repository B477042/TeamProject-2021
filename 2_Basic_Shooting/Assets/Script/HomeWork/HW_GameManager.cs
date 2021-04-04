using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 /*
    ���忡 ���� ���� �ϳ��� �� ������Ų��.
    ������Ų ���� �� Ŭ������ ����� �ż� �����ȴ�
    �÷��̾ ������ ��
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
        //0�� �Ǹ� ���带 �ø��� ���� �� ��ŭ ���� ����
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
