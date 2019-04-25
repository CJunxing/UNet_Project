using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {


    public GameObject Bullet;

    Transform _gunKou;


    // Use this for initialization
    void Start () {
        _gunKou = transform.Find("GunKou"); 
	}





    /// <summary>
    /// 这个方法指挥再本地角色调用  当角色被创建时调用
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
    }

    // Update is called once per frame
    void Update () {

        if (isLocalPlayer)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            transform.Rotate(Vector3.up * h * 128 * Time.deltaTime);

            transform.Translate(Vector3.forward * v * 3 * Time.deltaTime);
        } 
        else
        {
            return;
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
        
	}

    [Command]  //called in Client,run in server  生成子弹 同步子弹 使下面函数在服务端运行
    void CmdFire()//需要再Server 调用
    {

        //子弹生成需要Server完成 后同步到客户端
        GameObject zidan = Instantiate(Bullet, _gunKou.position, _gunKou.transform.rotation);
        zidan.GetComponent<Rigidbody>().velocity = zidan.transform.forward * 10;
        //zidan.GetComponent<Rigidbody>().AddForce(Vector3.forward * 500);
        Destroy(zidan, 3f);


        NetworkServer.Spawn(zidan);//将子弹同步到左右客户端上

    }

}
