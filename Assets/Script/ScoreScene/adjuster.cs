using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjuster : MonoBehaviour
{
    public void SizeAdjuster(int count){
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x,190*count);
    }
}
