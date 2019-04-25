using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{


    public const int MaxHealth = 100; //血总理

    [SyncVar(hook = "OnChangeHealth")] //同步变量  当下方变量发生变化的时候 将会调用方法 字符串为函数名称
    int _currentHealth = MaxHealth; //当前血量

    Slider _bloodSlider;


    NetworkStartPosition[] spwanerPoints;
    
    private void Start()
    {

        if (isLocalPlayer)
        {
            spwanerPoints = FindObjectsOfType<NetworkStartPosition>();
        }

        _bloodSlider = transform.Find("Blood/Slider").gameObject.GetComponent<Slider>();
    }

    public void TakeDamage(int damage)
    {
        if (!isServer) //该状态在服务器上处于活动状态则返回True 
        {
            return;
        }
        //下方代码旨在服务器端运行
        _currentHealth -= damage;
        if (_currentHealth<=0)
        {
            //人物死亡
            _currentHealth = MaxHealth;
            RpcRespawn();
        }
    }

    //同步血量
    void OnChangeHealth(int health)
    {
        _bloodSlider.value = health / (float)MaxHealth;
    }


    [ClientRpc] //远程调用  在所有客户端调用
    void RpcRespawn()
    {
        Debug.Log("复活");
        if (!isLocalPlayer)
        {
            return;
        }

        Vector3 spwanerPosition = Vector3.zero;

        if (spwanerPoints.Length > 0)
        {
            spwanerPosition = spwanerPoints[Random.Range(0, spwanerPoints.Length)].transform.position;

        }

        transform.position = spwanerPosition;
        
    }

}
