using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SizeAdjuster : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int id;
    float cMaxHeight,this_y;
    GameObject parent,line;
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        line = gameObject;
        cMaxHeight = 0;
        for(int i = 0; i < transform.childCount; i++){
            var child = transform.GetChild(i);
            if(cMaxHeight < child.GetComponent<RectTransform>().sizeDelta.y){
                cMaxHeight = child.GetComponent<RectTransform>().sizeDelta.y;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("UAnswer").GetComponent<TextMeshProUGUI>().text = parent.GetComponent<LineManager>().GetUAnswer(id);
        //子要素の中で最も高さが高いオブジェクトを取得
        for(int i = 0; i < transform.childCount; i++){
            var child = transform.GetChild(i);
            if(cMaxHeight < child.GetComponent<RectTransform>().sizeDelta.y){
                cMaxHeight = child.GetComponent<RectTransform>().sizeDelta.y;
            }
        }
        //Lineの高さを変更
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, cMaxHeight);
        parent.GetComponent<LineManager>().SetHeight(id,cMaxHeight);
        //高さに合わせて、位置を変更
        //localPotionを使うことに注意
        Vector3 _pos = gameObject.transform.localPosition;
        this_y = parent.GetComponent<LineManager>().GetHeight(id);
        _pos.y = this_y;
        gameObject.transform.localPosition = _pos;
    }
}
