using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{   public bool T_Active = false;
    public int T_Page = 0;
    public GameObject bground;
    public GameObject Page1;public GameObject Page2;public GameObject Page3;public GameObject Page4;public GameObject Page5;
    public void Awake()
    {
        bground.SetActive(false); Page1.SetActive(false); Page2.SetActive(false); Page3.SetActive(false); Page4.SetActive(false);Page5.SetActive(false);
    }
    public void OnPlay() 
    { if (!T_Active) { SceneManager.LoadScene("SampleScene"); } }
    public void OnTutorialEnter()
    {
        if (!T_Active)
        {
            T_Active = true;
            T_Page = 1;
            bground.SetActive(true);
            PageUpdate(1);
        }
    }
    public void Update()
    {
        if (T_Active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                    T_Page++; PageUpdate(T_Page);
            
               
            }
        }
    }
    void PageUpdate(int n) {
        switch (T_Page)
        {
            default:
                break;
            case 1: Page1.SetActive(true); Page5.SetActive(false); break;
            case 2: Page2.SetActive(true); Page1.SetActive(false); break;
            case 3: Page3.SetActive(true); Page2.SetActive(false); break;
            case 4: Page4.SetActive(true); Page3.SetActive(false); break;
            case 5: Page5.SetActive(true); Page4.SetActive(false); break;
            case 6: SceneManager.LoadScene("SampleScene"); break;
        }

    }

}

