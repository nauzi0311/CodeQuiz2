using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayButton : MonoBehaviour
{
    GameObject SceneDirector;
    // Start is called before the first frame update
    void Start()
    {
        SceneDirector = GameObject.Find("SceneDirector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        SceneDirector.GetComponent<HomeScene>().SelectDayButton(this.GetComponent<Outline>());
        this.GetComponent<Outline>().enabled = true;
        string text = this.GetComponentInChildren<TextMeshProUGUI>().text;
    }
}
