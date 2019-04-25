using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{

    public GameObject Enemy;
    public int Num;

    /// <summary>
    /// 启动服务器
    /// </summary>
    public override void OnStartServer()
    {
        CmdEnemySpawnerShow(Num);
    }

    
    
    public void CmdEnemySpawnerShow(int Num)
    {
        for (int i = 0; i < Num; i++)
        {
            GameObject enemy = Instantiate(Enemy, transform);
            enemy.transform.localPosition = new Vector3(Random.Range(-7f, 7f), 0, Random.Range(-3f, 10f));

            enemy.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            

            NetworkServer.Spawn(enemy);
        }
    }


}
