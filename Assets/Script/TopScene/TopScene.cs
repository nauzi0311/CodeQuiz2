using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Debug = UnityEngine.Debug;

public class TopScene : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject TextCode;
    GameObject TextQuiz;
    GameObject TextTap;
    void Start()
    {
        TextCode = GameObject.Find("Code");
        TextQuiz = GameObject.Find("Quiz");
        TextTap = GameObject.Find("Tap");
        TextTap.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void MoveTitle(){
        TextCode.GetComponent<Transform>().localPosition = 
        new Vector3(
            TextCode.GetComponent<TextAnimation>().stop_x, 
            TextCode.GetComponent<TextAnimation>().stop_y,
            0
        );
        TextQuiz.GetComponent<Transform>().localPosition = 
        new Vector3(
            TextQuiz.GetComponent<TextAnimation>().stop_x, 
            TextQuiz.GetComponent<TextAnimation>().stop_y,
            0
        );
    }    
    public void VisualizeTap(){
        TextTap.GetComponent<TextMeshProUGUI>().text = "Tap";
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
