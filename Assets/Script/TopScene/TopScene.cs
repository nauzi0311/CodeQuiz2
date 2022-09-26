using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Debug = UnityEngine.Debug;

public class TopScene : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject director;
    GameObject TextCode;
    GameObject TextQuiz;
    GameObject TextTap;
    void Start()
    {
        director = GameObject.Find("GameDirector");
        TextCode = GameObject.Find("Code");
        TextQuiz = GameObject.Find("Quiz");
        TextTap = GameObject.Find("Tap");
        TextTap.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void MoveTitle(){
        TextCode.GetComponent<Animator>().enabled = false;
        TextQuiz.GetComponent<Animator>().enabled = false;
        TextCode.GetComponent<Transform>().localPosition = 
        new Vector3(
            -140, 
            230,
            0
        );
        TextQuiz.GetComponent<Transform>().localPosition = 
        new Vector3(
            170, 
            -130,
            0
        );
    }    
    public void VisualizeTap(){
        TextTap.GetComponent<TextMeshProUGUI>().text = "Tap";
    }

    public void MoveScene(){
        director.GetComponent<GameDirector>().MoveScene("TopPage","LoadingPage");
    }

    //Colutine Method
    IEnumerator AleartTap(){
        for(int i = 0; i < 6; i++){
            if(TextTap.activeSelf){
                TextTap.SetActive(false);
            }else{
                TextTap.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
