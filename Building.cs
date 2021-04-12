using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int Hp = 25;
    public int MaxHp = 25;
    public GameObject Build;
    public string name;
    public bool IsBase;
    public bool Blue;
    public Node pos;
    public bool Base;
    public Building(Node n, Color c, GameObject g, bool Side, bool baase)
    {
        if (baase) { name = "H.Q";MaxHp = 25;Hp = 25; } else { name = "Candy F."; MaxHp = 8;Hp = 8; }
        pos = n;
        Debug.Log("4");
        pos.AddBuild(this);
        g.GetComponent<SpriteRenderer>().color = c;
        Build = Instantiate(g);
        Debug.Log("5");
        if (Side) { Blue = true; } else { Blue = false; }
        IsBase = baase;
        Debug.Log(baase);
        GoToPosition();
    }
    public void GoToPosition()
    {
        Debug.Log("6");
        Build.transform.position = pos.Spritee.transform.position;
    }
    public void Die()
    {
      
        pos.RemoveBuild();
        Destroy(Build);
    }
}
