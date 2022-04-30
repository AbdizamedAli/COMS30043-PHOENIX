using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class move : MonoBehaviour
{
    PhotonView photonView;
    public bool flag = false;
    private CharacterController Cc;
    // Start is called before the first frame update
    void Awake()
    {
        Cc = gameObject.GetComponent<CharacterController>();
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flag == true)
        {
            float moveh = 0;

            if (Input.GetKey(KeyCode.B))
            {
                moveh += Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.G))
            {
                moveh -= Time.deltaTime;
            }
            photonView.RPC("Go_cy", RpcTarget.Others,moveh);
        }
    }
    [PunRPC]
    void Go_cy(float xgo)
    {

            Cc.Move(new Vector3(5*xgo, 0, 0));
    }

}
