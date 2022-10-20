using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeContent : MonoBehaviour
{
    [SerializeField]
    int badge_count;
    GameObject Badge,director;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("SceneDirector");
        Badge = transform.Find("Badge").gameObject;
        Badge.SetActive(GameDirector.userdata.badge[badge_count]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        director.GetComponent<BadgeScene>().ViewDetail(badge_count);
    }
}
