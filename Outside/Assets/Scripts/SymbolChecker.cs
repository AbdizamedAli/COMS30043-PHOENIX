using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

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
    [SerializeField] private GameObject enter_text;


    public int LastPressed;
    int SymbolSelectOneLoc;
    int SymbolSelectTwoLoc;
    int SymbolSelectThreeLoc;
    int SymbolSelectFourLoc;
    int CorrectSymbols;





    void Awake(){
        LastPressed=-1;
        CorrectSymbols=0;
        SymbolList.Add(SymbolOne);
        SymbolList.Add(SymbolTwo);
        SymbolList.Add(SymbolThree);
        SymbolList.Add(SymbolFour);
        SymbolList.Add(SymbolFive);
        SymbolList.Add(SymbolSix);
        SymbolList.Add(SymbolSeven);
        SymbolList.Add(SymbolEight);
        SymbolList.Add(SymbolNine);
        SymbolList.Add(SymbolTen);
        SymbolList.Add(SymbolEleven);
        SymbolList.Add(SymbolTwelve);
        SymbolList.Add(SymbolThirteen);
        SymbolList.Add(SymbolFourteen);
        SymbolList.Add(SymbolFifteen);
        SymbolSelectOneLoc=9;
        //SymbolSelectOneLoc=Random.Range(0,14);
        //SymbolSelectTwoLoc = Random.Range(0,14);
        SymbolSelectTwoLoc = 11;
        //while (SymbolSelectOneLoc==SymbolSelectTwoLoc){
        //    SymbolSelectTwoLoc=Random.Range(0,14);
        //}
        SymbolSelectThreeLoc=2;
        //SymbolSelectThreeLoc=Random.Range(0,14);
        //while (SymbolSelectThreeLoc==SymbolSelectOneLoc ||SymbolSelectThreeLoc==SymbolSelectTwoLoc){
        //    SymbolSelectThreeLoc=Random.Range(0,14);
        //}
        SymbolSelectFourLoc = 13;
        //SymbolSelectFourLoc=Random.Range(0,14);
        //while(SymbolSelectFourLoc==SymbolSelectOneLoc||SymbolSelectFourLoc==SymbolSelectTwoLoc||SymbolSelectFourLoc==SymbolSelectThreeLoc){
        //    SymbolSelectFourLoc=Random.Range(0,14);
        //}

        SymbolSelectOne =SymbolList[SymbolSelectOneLoc];
        SymbolSelectTwo=SymbolList[SymbolSelectTwoLoc];
        SymbolSelectThree=SymbolList[SymbolSelectThreeLoc];
        SymbolSelectFour=SymbolList[SymbolSelectFourLoc];
    }


    // Start is called before the first frame update
    void Start()
    {
        SymbolSelectOne.transform.position=SymbolSelectOnePos.transform.position;
        SymbolSelectTwo.transform.position=SymbolSelectTwoPos.transform.position;
        SymbolSelectThree.transform.position=SymbolSelectThreePos.transform.position;
        SymbolSelectFour.transform.position=SymbolSelectFourPos.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LastPressed==SymbolSelectOneLoc&&CorrectSymbols==0){
            CorrectSymbols=1;
            print(CorrectSymbols);
        }
        else if(LastPressed==SymbolSelectTwoLoc && CorrectSymbols==1){
            CorrectSymbols=2;
            print(CorrectSymbols);
        
        }
        else if(LastPressed==SymbolSelectThreeLoc && CorrectSymbols==2){
            CorrectSymbols=3;
            print(CorrectSymbols);

        }
        else if(LastPressed==SymbolSelectFourLoc && CorrectSymbols==3){
            CorrectSymbols=4;
            print("success");
            this.photonView.RPC("activateExit", RpcTarget.All);
            SymbolSelectOneBack.GetComponent<Renderer>().material = Invisible;
            SymbolSelectTwoBack.GetComponent<Renderer>().material = Invisible;
            SymbolSelectThreeBack.GetComponent<Renderer>().material = Invisible;
            SymbolSelectFourBack.GetComponent<Renderer>().material = Invisible;


        }
        
        else if(LastPressed>=0 && (LastPressed!=SymbolSelectOneLoc&&LastPressed!=SymbolSelectTwoLoc&&LastPressed!=SymbolSelectThreeLoc&&LastPressed!=SymbolSelectFourLoc)){
            CorrectSymbols=0;
            print("try again");
        }
    }
    [PunRPC]
    private void activateExit()
    {
        exit.SetActive(true);
        //enter_text.GetComponent<Text>().text = "Room Done";
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
