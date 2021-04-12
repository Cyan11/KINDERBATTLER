using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public int turn = 0;
   public int gturn = 0;
    public UnitManager UM;
    public ControlManager CM;
    public Color red;
    public Color blue;
    public TextMeshProUGUI turnText;
    public GameObject P1;public GameObject P2;
    public GameObject W1; public GameObject W2;

    public void EndTurn()
    {
       
        if (gturn == 1) { CM.NewTurn(); turnText.color = blue;turnText.SetText("P1");CM.CurrentControl = CM.Control1; } else { CM.NewTurn(); turnText.color = red; turnText.SetText("P2"); CM.CurrentControl = CM.Control1; }
        turn++;
        ShowTurn();
        gturn = turn % 2;
        
        CM.ControlUpdate();
        UM.NewTurn();
        UM.Hide();
        
        
    }
    public void Win(int n)
    {
        if (n == 0) { StartCoroutine(Wa1()); } else { StartCoroutine(Wa2()); }
    }
    private void Awake()
    {
        W1.SetActive(false); W2.SetActive(false);

        P1.SetActive(false); P2.SetActive(false);
        turnText.color = blue;
        turnText.SetText("P1");
    }
    private void ShowTurn()
    {
        if (gturn ==1) { StartCoroutine(Pa1()); } 
        else
        {
            StartCoroutine(Pa2());
        }
    }
    public IEnumerator Pa1()
    {
        Debug.Log("ta");
        P1.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        P1.SetActive(false);
    }
    public IEnumerator Pa2()
    {
        Debug.Log("ga");
        P2.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        P2.SetActive(false);
    }
    public IEnumerator Wa1()
    {
        Debug.Log("ta");
        W1.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        W1.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
    public IEnumerator Wa2()
    {
        Debug.Log("ga");
        W2.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        W2.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}
