using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EnemyHealth : NetworkBehaviour
{

    const int _maxHealth = 200;

    [SyncVar(hook = "OnChangeHealth")]
    int _nowHealth = _maxHealth;

    Slider _healthSlider;

    private void Start()
    {
        _healthSlider = transform.Find("Blood/Slider").gameObject.GetComponent<Slider>();
    }

    public void TakeDamage(int damage)
    {
        if (isServer)
        {
            _nowHealth -= damage;
            if (_nowHealth<=0)
            {
                _nowHealth = _maxHealth;

                RpcRespawn();
            }
        }
    }


    [ClientRpc] //远程调用  在所有客户端调用
    void RpcRespawn()
    {
        transform.localPosition = new Vector3(Random.Range(-7f, 7f), 0, Random.Range(-3f, 10f));
        transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }


    void OnChangeHealth(int health)
    {
        _healthSlider.value = health / (float)_maxHealth;
    }

}
