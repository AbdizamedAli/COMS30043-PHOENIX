using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class SymbolChecker : MonoBehaviourPunCallbacks
{
    public GameObject SymbolOne;
    public GameObject SymbolTwo;
    public GameObject SymbolThree;
    public GameObject SymbolFour;
    public GameObject SymbolFive;
    public GameObject SymbolSix;
    public GameObject SymbolSeven;
    public GameObject SymbolEight;
    public GameObject SymbolNine;
    public GameObject SymbolTen;
    public GameObject SymbolEleven;
    public GameObject SymbolTwelve;
    public GameObject SymbolThirteen;
    public GameObject SymbolFourteen;
    public GameObject SymbolFifteen;

    public List<GameObject> SymbolList;

    public Transform SymbolSelectOnePos;
    public Transform SymbolSelectTwoPos;
    public Transform SymbolSelectThreePos;
    public Transform SymbolSelectFourPos;

    public GameObject SymbolSelectOne;
    public GameObject SymbolSelectTwo;
    public GameObject SymbolSelectThree;
    public GameObject SymbolSelectFour;

    public Material Invisible;
    public Material strart_room_colour;
    public GameObject SymbolSelectOneBack;
    public GameObject SymbolSelectTwoBack;
    public GameObject SymbolSelectThreeBack;
    public GameObject SymbolSelectFourBack;
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject exit1;

    private List<int> randoms;


    public int LastPressed = -1;
    int SymbolSelectOneLoc;
    int SymbolSelectTwoLoc;
    int SymbolSelectThreeLoc;
    int SymbolSelectFourLoc;
    int CorrectSymbols = 0;

    private FloorManagerOne FloorManagerOne;





    void Awake(){
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        var rnd = new System.Random();

        HashSet<int> numbers = new HashSet<int>();
        while (numbers.Count < 4)
        {
            numbers.Add(rnd.Next(1, 15));
        }
        randoms = numbers.ToList();

        SymbolSelectOneLoc   = randoms[0] - 1;
        SymbolSelectTwoLoc   = randoms[1] - 1;
        SymbolSelectThreeLoc = randoms[2] - 1;
        SymbolSelectFourLoc  = randoms[3] - 1;

        this.photonView.RPC(nameof(setSelectLocSymbol), RpcTarget.All, randoms[0] - 1, randoms[1] - 1, randoms[2] - 1, randoms[3] - 1);
        FloorManagerOne=GameObject.FindObjectOfType<FloorManagerOne>();
    }

    [PunRPC]
    void setSelectLocSymbol(int x, int y, int z, int q)
    {
        SymbolSelectOneLoc = x;
        SymbolSelectTwoLoc = y;
        SymbolSelectThreeLoc = z;
        SymbolSelectFourLoc = q;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Symbols", "symbol button " + randoms[0]), SymbolSelectOnePos.transform.position,Quaternion.Euler(0,180,90));
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Symbols", "symbol button " + randoms[1]), SymbolSelectTwoPos.transform.position, Quaternion.Euler(0, 180, 90));
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Symbols", "symbol button " + randoms[2]), SymbolSelectThreePos.transform.position, Quaternion.Euler(0, 180, 90));
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Symbols", "symbol button " + randoms[3]), SymbolSelectFourPos.transform.position, Quaternion.Euler(0, 180, 90));


    }

    // Update is called once per frame
    void Update()
    {
        if(LastPressed==SymbolSelectOneLoc&&CorrectSymbols==0){
            CorrectSymbols=1;
            this.photonView.RPC("setCorrectSymbol", RpcTarget.All, 1, SymbolList[randoms[0] - 1].name);
            print(CorrectSymbols);
        }
        else if(LastPressed==SymbolSelectTwoLoc && CorrectSymbols==1){
            CorrectSymbols=2;
            this.photonView.RPC("setCorrectSymbol", RpcTarget.All, 2, SymbolList[randoms[1] - 1].name);

            print(CorrectSymbols);
        
        }
        else if(LastPressed==SymbolSelectThreeLoc && CorrectSymbols==2){
            CorrectSymbols=3;
            this.photonView.RPC("setCorrectSymbol", RpcTarget.All, 3, SymbolList[randoms[2] - 1].name);

            print(CorrectSymbols);

        }
        else if(LastPressed==SymbolSelectFourLoc && CorrectSymbols==3){
            CorrectSymbols=4;
            this.photonView.RPC("setCorrectSymbol", RpcTarget.All, 4, SymbolList[randoms[3] - 1].name);

            print("success");
            this.photonView.RPC("activateExit", RpcTarget.All);
            SymbolSelectOneBack.GetComponent<Renderer>().material = Invisible;
            SymbolSelectTwoBack.GetComponent<Renderer>().material = Invisible;
            SymbolSelectThreeBack.GetComponent<Renderer>().material = Invisible;
            SymbolSelectFourBack.GetComponent<Renderer>().material = Invisible;


        }

        else if (LastPressed >= 0 && (LastPressed != SymbolSelectOneLoc && LastPressed != SymbolSelectTwoLoc && LastPressed != SymbolSelectThreeLoc && LastPressed != SymbolSelectFourLoc))
        {
            if(CorrectSymbols != 0)
            {
                CorrectSymbols = 0;
                this.photonView.RPC("setCorrectSymbol", RpcTarget.All, 0, "Empty");

                Debug.Log("try again");
            }

        }
    }
    [PunRPC]
    private void activateExit()
    {
        exit.SetActive(true);
        exit1.SetActive(true);
        FloorManagerOne.PuzzleComplete();

        //enter_text.GetComponent<Text>().text = "Room Done";
    }

    [PunRPC]
    private void setCorrectSymbol(int i,string name)
    {
        if (i == 0)
        {
            foreach (var item in SymbolList)
            {
                item.GetComponent<Renderer>().material = strart_room_colour;
            }
        }
        else if(i == 4)
        {
            foreach (var item in SymbolList)
            {
                item.GetComponent<Renderer>().material = Invisible;
            }
        }
        else
        {

            GameObject.Find(name).GetComponent<Renderer>().material.color = Color.green;
            CorrectSymbols = i;
        }
    }

    public void SymbolOnePressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 0);

        }

    }
    public void SymbolTwoPressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 1);
        }


    }
    public void SymbolThreePressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 2);
        }


    }
    public void SymbolFourPressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 3);
        }


    }
    public void SymbolFivePressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 4);
        }


    }
    public void SymbolSixPressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 5);
        }


    }
    public void SymbolSevenPressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 6);
        }


    }
    public void SymbolEightPressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 7);
        }


    }
    public void SymbolNinePressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 8);
        }


    }
    public void SymbolTenPressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 9);
        }


    }
    public void SymbolElevenPressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 10);
        }


    }
    public void SymbolTwelvePressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 11);
        }


    }
    public void SymbolThirteenPressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 12);
        }


    }
    public void SymbolFourteenPressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 13);
        }


    }
    public void SymbolFifteenPressed(bool pressed){
        if (pressed==true){
            this.photonView.RPC(nameof(setSymbolLastPressed), RpcTarget.All, 14);
        }


    }

    [PunRPC]
    private void setSymbolLastPressed(int pressed)
    {
        LastPressed = pressed;
    }
    
}
