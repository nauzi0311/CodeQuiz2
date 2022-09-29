using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegisterScene : MonoBehaviour
{
    GameObject director;
    //InputField _namefield,_passfield;
    GameObject _namefield,_passfield;
    string uuid;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("GameDirector");
        _namefield = GameObject.Find("UserName");
        _passfield = GameObject.Find("Password");
        uuid = System.Guid.NewGuid().ToString();
        PlayerPrefs.SetString("UUID",uuid);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick(){
        //you need fix
        string username = _namefield.GetComponent<TMP_InputField>().text;
        string password = _passfield.GetComponent<TMP_InputField>().text;
        // string username = null;
        // string password = null;

        Data _data = new Data(uuid, username, password);
        StartCoroutine(SignUp(_data));
    }

    IEnumerator SignUp(Data _d){
        yield return StartCoroutine(GameDirector.WebReqPost("index/signup",Data.Serialize(_d)));
        GameDirector.config = Config.Deserialize(GameDirector.GetResponse());
        //Debug.Log(GameDirector.config.level);
        director.GetComponent<GameDirector>().MoveScene("RegisterPage","HomePage");
    }

    class Data{
        public string device,username,password;

        public Data(string _d,string _u,string _p){
            device = _d;username = _u;password = _p;
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


