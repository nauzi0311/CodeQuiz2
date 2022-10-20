using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    GameObject director;
    int[] UAnswer;
    float[] Height_list = {0,0,0,0,0,0,0};
    float result_area_y,content_area_y,last_y = 0;
    //float padding = 150f;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("GameDirector");
        UnityEngine.Vector2 _size = GetComponent<RectTransform>().sizeDelta;
        result_area_y = GameObject.Find("Result").GetComponent<RectTransform>().sizeDelta.y;
        result_area_y += 1920;
        content_area_y = GameObject.Find("Result").transform.Find("Header").gameObject.GetComponent<RectTransform>().sizeDelta.y;
        content_area_y += 1920;
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector2 _size = GetComponent<RectTransform>().sizeDelta;
        //Headの高さ
        float _sum_hegiht = 190f;
        string _debug = "HeightList: ";
        for(int i = 0; i < Height_list.Length; i++){
            _sum_hegiht += Height_list[i];
            _debug += Height_list[i] +  " ";
        }
        //_size.y = _sum_hegiht - content_area_y;
        _size.y = -last_y;
        Debug.Log(_size.x + " " + _size.y + " " + last_y);
        gameObject.GetComponent<RectTransform>().sizeDelta = _size;
        Debug.Log(_debug);
    }

    public void SetHeight(int num,float height){
        Height_list[num] = height;
    }

    public float GetHeight(int num){
        //Headの下端の位置
        float ans = 0.0f;
        int i;
        //指定オブジェクトの下端までのY
        for (i = 0; i < num + 1; i++){
            ans -= Height_list[i];
        }
        //オブジェクトの中心のYをしていするため
        ans += Height_list[i-1]/2;
        return ans;
    }

    public void SetLastHeight(float _h){
        last_y = _h;
    }
}
