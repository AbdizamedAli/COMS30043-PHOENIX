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
    public GameObject SymbolSelectOneBack;
    public GameObject SymbolSelectTwoBack;
    public GameObject SymbolSelectThreeBack;
    public GameObject SymbolSelectFourBack;
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject exit1;

    private List<int> randoms;


    public int LastPressed;
    int SymbolSelectOneLoc;
    int SymbolSelectTwoLoc;
    int SymbolSelectThreeLoc;
    int SymbolSelectFourLoc;
    int CorrectSymbols;





    void Awake(){
        LastPressed=-1;
        CorrectSymbols=0;


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
            this.photonView.RPC("setCorrectSymbol", RpcTarget.All, 1);
            print(CorrectSymbols);
        }
        else if(LastPressed==SymbolSelectTwoLoc && CorrectSymbols==1){
            CorrectSymbols=2;
            this.photonView.RPC("setCorrectSymbol", RpcTarget.All, 2);

            print(CorrectSymbols);
        
        }
        else if(LastPressed==SymbolSelectThreeLoc && CorrectSymbols==2){
            CorrectSymbols=3;
            this.photonView.RPC("setCorrectSymbol", RpcTarget.All, 3);

            print(CorrectSymbols);

        }
        else if(LastPressed==SymbolSelectFourLoc && CorrectSymbols==3){
            CorrectSymbols=4;
            this.photonView.RPC("setCorrectSymbol", RpcTarget.All, 4);

            print("success");
            this.photonView.RPC("activateExit", RpcTarget.All);
            SymbolSelectOneBack.GetComponent<Renderer>().material = Invisible;
            SymbolSelectTwoBack.GetComponent<Renderer>().material = Invisible;
            SymbolSelectThreeBack.GetComponent<Renderer>().material = Invisible;
            SymbolSelectFourBack.GetComponent<Renderer>().material = Invisible;


        }
        
        else if(LastPressed>=0 && (LastPressed!=SymbolSelectOneLoc&&LastPressed!=SymbolSelectTwoLoc&&LastPressed!=SymbolSelectThreeLoc&&LastPressed!=SymbolSelectFourLoc)){
            CorrectSymbols=0;
            this.photonView.RPC("setCorrectSymbol", RpcTarget.All, 0);

            Debug.Log("try again");
        }
    }
    [PunRPC]
    private void activateExit()
    {
        exit.SetActive(true);
        exit1.SetActive(true);
        //enter_text.GetComponent<Text>().text = "Room Done";
    }

    [PunRPC]
    private void setCorrectSymbol(int i)
    {
        CorrectSymbols = i;
    }

    public void SymbolOnePressed(bool pressed){
        if (pressed==true){
            LastPressed=0;
            
        }

    }
    public void SymbolTwoPressed(bool pressed){
        if (pressed==true){
            LastPressed=1;
            
        }


    }
    public void SymbolThreePressed(bool pressed){
        if (pressed==true){
            LastPressed=2;
            
        }


    }
    public void SymbolFourPressed(bool pressed){
        if (pressed==true){
            LastPressed=3;
            
        }


    }
    public void SymbolFivePressed(bool pressed){
        if (pressed==true){
            LastPressed=4;
            
        }


    }
    public void SymbolSixPressed(bool pressed){
        if (pressed==true){
            LastPressed=5;
            
        }


    }
    public void SymbolSevenPressed(bool pressed){
        if (pressed==true){
            LastPressed=6;
            
        }


    }
    public void SymbolEightPressed(bool pressed){
        if (pressed==true){
            LastPressed=7;
            
        }


    }
    public void SymbolNinePressed(bool pressed){
        if (pressed==true){
            LastPressed=8;
            
        }


    }
    public void SymbolTenPressed(bool pressed){
        if (pressed==true){
            LastPressed=9;
            
        }


    }
    public void SymbolElevenPressed(bool pressed){
        if (pressed==true){
            LastPressed=10;
            
        }


    }
    public void SymbolTwelvePressed(bool pressed){
        if (pressed==true){
            LastPressed=11;
            
        }


    }
    public void SymbolThirteenPressed(bool pressed){
        if (pressed==true){
            LastPressed=12;
            
        }


    }
    public void SymbolFourteenPressed(bool pressed){
        if (pressed==true){
            LastPressed=13;
            
        }


    }
    public void SymbolFifteenPressed(bool pressed){
        if (pressed==true){
            LastPressed=14;
            
        }


    }
    
}
