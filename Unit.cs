using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Unit : MonoBehaviour
{
    public UnitType cl;
    public TurnManager TM;
    public UnitManager UM;
    public int atk;
    public int def;
    public int hp;
    public bool Blue;
    public int tempHp;
    public int Maintaine = 2;
    public bool Maintained = true;
    public bool Tank = true;
    public Node pos;
    public GameObject unitSprite;
    public void UpdatePos()
    {
       // unitSprite.transform.Translate(pos.Spritee.transform.position);
       // Vector2.MoveTowards(unitSprite.transform.position, pos.Spritee.transform.position, 0.1f);
        unitSprite.transform.position = pos.Spritee.transform.position;


    }
    private void Update()
    {
       
        unitSprite.transform.position = pos.Spritee.transform.position;
        //if (pos.Hidden) { Destroy(unitSprite);Tank = false; } else if(!Tank){ Instantiate(unitSprite);Tank = true; }
    }
    public void Move(Node n)
    {
        n.AddUnit(this);
        pos.RemoveUnit();
        pos = n;
        UpdatePos();
        
    }
    public void Maintain()
    {
        Maintaine = 4;
        Maintained = true;
        unitSprite.GetComponent<SpriteRenderer>().sprite = cl.alt;
    }
    public void NewTurn()
    {
        if (tempHp < hp && tempHp>1)
        {
            tempHp++;
        }
        Maintaine -= 1;
        if(Maintaine < 0.1f && tempHp>1)
        {
            Maintained = false;
            unitSprite.GetComponent<SpriteRenderer>().sprite = cl.sprite;
        }
    }

    public void TeleportTo(Node n)
    {
        n.AddUnit(this);
        pos = n;
        UpdatePos();
        
    }
    public Unit(Node n, UnitType type,Color c, GameObject g,bool Side, TurnManager Tm) {
        if (Side) { Blue = true; } else { Blue = false; }
        TM = Tm;
        g.GetComponent<SpriteRenderer>().sprite = type.alt;
            g.GetComponent<SpriteRenderer>().color = c;
        unitSprite = Instantiate(g);
        cl = type;
        atk = cl.Atk;def = cl.Def;hp = cl.Hp;tempHp = hp;

        TeleportTo(n);
    }
    public void KillSelf()
    {
        pos.RemoveUnit();
        Destroy(unitSprite);
        Destroy(this);
    }
    public void Upgrade()
    {
        switch (Random.Range(0, 4))
        {
            case 1:atk += 3;return;
            case 2:tempHp += 3; hp += 5; return;
            case 3: def += 3; return;
               
        }
    }
}
