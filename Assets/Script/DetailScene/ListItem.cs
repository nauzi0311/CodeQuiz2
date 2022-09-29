using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        Transform ID = gameObject.transform.Find("Id");
        Debug.Log(ID.gameObject.GetComponent<TextMeshProUGUI>().text);
    }
}
