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
    GameObject Version,VersionCanvas;
    void Start()
    {
        director = GameObject.Find("GameDirector");
        TextCode = GameObject.Find("Code");
        TextQuiz = GameObject.Find("Quiz");
        TextTap = GameObject.Find("Tap");
        Version = GameObject.Find("Version");
        VersionCanvas = GameObject.Find("VersionCanvas");
        VersionCanvas.SetActive(false);
        TextTap.GetComponent<TextMeshProUGUI>().text = "";
        PlayerPrefs.SetString("Version","1-0-1");
        StartCoroutine(CheckVersion());
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
    public IEnumerator AleartTap(){
        for(int i = 0; i < 6; i++){
            if(TextTap.activeSelf){
                TextTap.SetActive(false);
            }else{
                TextTap.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator CheckVersion(){
        string _version = PlayerPrefs.GetString("Version");
        if(_version != null){
            string json = "{\"version\":\"" + _version + "\"}";
            yield return GameDirector.WebReqPost("index/version",json);
            VersionData _tmp = VersionData.Deserialize(GameDirector.GetResponse());
            if(_version != _tmp.version){
                TopCanvas.is_update = true;
                VersionCanvas.SetActive(true);
                Version.GetComponent<TextMeshProUGUI>().text = "Version:\n" + _tmp.version + "\nis released.\n Please update from\n " + _tmp.url;
                //yield return GameDirector.Upgrade(_tmp.version,_tmp.url);
            }
        }
    }

    class VersionData{
        public string version;
        public string url;

        public static VersionData Deserialize(string json){
            return JsonUtility.FromJson<VersionData>(json);
        }
    }
}
