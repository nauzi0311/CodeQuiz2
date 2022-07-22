using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] float first_x;
    [SerializeField] float first_y;
    [SerializeField] float move_x;
    [SerializeField] float move_y;
    public float stop_x;
    public float stop_y;
    public RectTransform tf;
    private float durationx;
    private float durationy;
    // Start is called before the first frame update
    void Start()
    {
        durationx = stop_x - first_x;
        durationy = stop_y - first_y;
        tf.localPosition = new Vector3(first_x, first_y,0);
    }

    // Update is called once per frame
    void Update()
    {
        float deltax = stop_x - tf.localPosition.x;
        float deltay = stop_y - tf.localPosition.y;
        if((tf.localPosition.x >= stop_x) && (durationx != 0)) tf.localPosition += new Vector3(move_x*(power(deltax/durationx,3)),0,0);
        if((tf.localPosition.y >= stop_y) && (durationy != 0)) tf.localPosition += new Vector3(0,move_y*(power(deltay/durationy,3)),0);
    }

    float power(float x,int times){
        float temp = 1;
        for(int i = 0;i < times;i++){
            temp += temp * x;
        }
        return temp;
    }
}
