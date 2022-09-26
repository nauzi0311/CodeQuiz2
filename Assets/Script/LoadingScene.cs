using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject director;
    string uuid;
    void Start()
    {
        uuid = PlayerPrefs.GetString("UUID");
        Debug.Log(uuid);
        StartCoroutine(WaitLoading());
    }

    IEnumerator WaitLoading(){
        if(!string.IsNullOrWhiteSpace(uuid)){
            Data _data = new Data(uuid);
            yield return StartCoroutine(GameDirector.WebReqPost("index",Data.Serialize(_data)));
            Debug.Log(GameDirector.GetResponse());
            if(GameDirector.GetResponse().ToString() == "new"){
                SceneManager.LoadScene("RegisterPage");
            }else{
                GameDirector.userdata = UserData.Deserialize(GameDirector.GetResponse());
                SceneManager.LoadScene("HomePage");
            }
        }else{
            SceneManager.LoadScene("RegisterPage");
        }
        yield return null;
    }

    class Data{
        public string device;
        public Data(string _d){
            device = _d;
        }
        public static Data Deserialize(string json){
            Data _data = JsonUtility.FromJson<Data>(json);
            return _data;
        }

        public static string Serialize(Data _data){
            string json = JsonUtility.ToJson(_data);
            Debug.Log("json:" + json);
            return json;
        }
    }
}




