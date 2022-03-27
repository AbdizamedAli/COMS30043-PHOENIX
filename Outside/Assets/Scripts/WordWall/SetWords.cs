using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System.IO;

public class SetWords : MonoBehaviourPunCallbacks
{

    public TextAsset textFile;
    string[] words;
    List<string> correct_words = new List<string>();
    public TextMeshPro tm;
    int random_idx_1;
    int random_idx_2;
    int random_idx_3;
    List<List<string>> random_words_list = new List<List<string>>();

    void Start()
    {
        words = (textFile.text.Split('\n'));



        if (textFile != null & PhotonNetwork.IsMasterClient)
        {

            this.photonView.RPC("getIndexRandoms", RpcTarget.All);

            this.photonView.RPC("updateCorrectWords", RpcTarget.AllBuffered, words[Random.Range(0, words.Length)]);
            this.photonView.RPC("updateCorrectWords", RpcTarget.AllBuffered, words[Random.Range(0, words.Length)]);
            this.photonView.RPC("updateCorrectWords", RpcTarget.AllBuffered, words[Random.Range(0, words.Length)]);

            this.photonView.RPC("setRandomWordList", RpcTarget.AllBuffered, words[Random.Range(0, words.Length)], words[Random.Range(0, words.Length)], words[Random.Range(0, words.Length)],
                                                                            words[Random.Range(0, words.Length)], words[Random.Range(0, words.Length)], words[Random.Range(0, words.Length)],
                                                                            words[Random.Range(0, words.Length)], words[Random.Range(0, words.Length)], words[Random.Range(0, words.Length)]);




            if (correct_words.Count == 3)
            {

                this.photonView.RPC("setWords", RpcTarget.AllBuffered);
            }
        }

    }
    [PunRPC]
    void setRandomWordList(string word1 , string word2 , string word3, string word4, string word5, string word6, string word7, string word8, string word9)
    {
        random_words_list.Add(new List<string> { word1,word2,word3 });
        random_words_list.Add(new List<string> { word4,word5,word6 });
        random_words_list.Add(new List<string> { word7,word8,word9 });
    }

    [PunRPC]
    void updateCorrectWords(string word)
    {
        Debug.Log(word);
        correct_words.Add(word);
    }
    [PunRPC]
    private void setWords()
    {


        for (int i = 1; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {

                if (j - 1 == random_idx_1 && i == 1)
                {
                    this.photonView.RPC("setCorrectRow", RpcTarget.All, i, j);
                    this.photonView.RPC("updateText", RpcTarget.AllBuffered,i,j, correct_words[0]);
                }
                else if (j - 1 == random_idx_2 && i == 2)
                {
                    this.photonView.RPC("setCorrectRow", RpcTarget.All, i, j);
                    this.photonView.RPC("updateText", RpcTarget.AllBuffered,i,j, correct_words[1]);
                }
                else if (j - 1 == random_idx_3 && i == 3)
                {
                    this.photonView.RPC("setCorrectRow", RpcTarget.All, i, j);
                    this.photonView.RPC("updateText", RpcTarget.AllBuffered,i,j, correct_words[2]);
                }

                else
                {
                    this.photonView.RPC("updateText", RpcTarget.AllBuffered,i,j, random_words_list[i-1][j-1]);
                }
            }
        }
    }

    [PunRPC]
    void updateText(int i, int j,string k)
    {
        GameObject.Find("row_" + i + "_" + j + "(Clone)").GetComponent<TextMeshPro>().text = k;
    }

    [PunRPC]
    void setCorrectRow(int i, int j)
    {
        GameObject.Find("row" + i + "(Clone)").GetComponent<FindOnButton>().row_correct = j;

    }
    [PunRPC]
    void getIndexRandoms()
    {
        random_idx_1   = Random.Range(0, 3);
        random_idx_2   = Random.Range(0, 3);
        random_idx_3   = Random.Range(0, 3);
    }


}
