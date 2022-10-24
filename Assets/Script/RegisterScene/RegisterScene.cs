using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegisterScene : MonoBehaviour
{
    GameObject director;
    //InputField _namefield,_passfield;
    GameObject _namefield,_idfield,_school_numfield;
    string uuid;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("GameDirector");
        _namefield = GameObject.Find("UserName");
        _idfield = GameObject.Find("ID");
        _school_numfield = GameObject.Find("School_Num");
        uuid = System.Guid.NewGuid().ToString();
        PlayerPrefs.SetString("UUID",uuid);
    }
    public void OnClick(){
        //you need fix
        string id = _idfield.GetComponent<TMP_InputField>().text;
        string username = _namefield.GetComponent<TMP_InputField>().text;
        int school_num = Int32.Parse(_school_numfield.GetComponent<TMP_InputField>().text);
        // string username = null;
        // string password = null;

        Data _data = new Data(uuid, id,username, school_num);
        StartCoroutine(SignUp(_data));
        PlayerPrefs.SetString("UUID",uuid);
    }

    IEnumerator SignUp(Data _d){
        yield return StartCoroutine(GameDirector.WebReqPost("index/signup",Data.Serialize(_d)));
        GameDirector.userdata = new UserData(_d.school_num);
        GameDirector.config = Config.Deserialize(GameDirector.GetResponse());
        PlayerPrefs.SetString("Version",GameDirector.config.version);
        director.GetComponent<GameDirector>().MoveScene("RegisterPage","HomePage");
    }

    class Data{
        public string device,id,username;
        public int school_num;

        public Data(string _d,string _id,string _u,int _num){
            device = _d;id = _id;username = _u;school_num = _num;
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


