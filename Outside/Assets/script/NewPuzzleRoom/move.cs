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
            photonView.RPC("_go", RpcTarget.All);
        }
    }
    [PunRPC]
    void _go()
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
            Cc.Move(new Vector3(5*moveh, 0, 0));
    }

}
