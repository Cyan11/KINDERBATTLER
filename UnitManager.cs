using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System;
using System.Linq;
using UnityEngine.SocialPlatforms;

public class UnitManager : MonoBehaviour
{
    public Color red;
    public BuildManager BiM;
    public Color blue;
    public Color purple;
    public UnitType tyTest;
    public BoardManager BM;
    public GameObject unit;
    public ControlManager CM;
    public bool NodeSelected;
    public Unit A;
    public Unit Selected;
    private Node SelectedNode;
    public TurnManager TM;
    public Node n;public Node d;
    public bool UnitSelected = false;
    public List<Unit> army1;
    public List<Unit> army2;
    public void NewTurn()
    {
        for (int i = 0; i < army1.Count; i++)
        {
            army1.ElementAt(i).NewTurn();
        }
        for (int i = 0; i < army2.Count; i++)
        {
            army2.ElementAt(i).NewTurn();
        }

    }
    public void CreateUnit(Node test, bool blu,UnitType t)
    {
        if (blu)
        { A = new Unit(test, t, blue, unit,true,TM); army1.Add(A); }
        else
        { A = new Unit(test, t, red, unit,false,TM); army2.Add(A); }
        Hide();
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            n = BM.NodeFromCursor();
            if (n.Exists && BM.Suc)
            {
                if (UnitSelected)
                {
                    bool can = false;
                  
                    if (ThereIsDifference()&&CM.CurrentControl>0  ) { 
                        MoveUnit();
                        if (TM.gturn == 0)
                        {
                            //Debug.Log("d");
                            CM.Control1 -= 1.0f;
                            CM.ControlUpdate();
                        }
                        else {  CM.Control2 -= 1.0f; CM.ControlUpdate(); }
                    }
                    else { DeSelectUnit();  }
                    Hide();
                    can = false;
                }
                else if(n.HasUnit)
                {
                  //  Debug.Log("y");
                     SelectUnit();
                    if (UnitSelected)
                    {
                        NewMovement(Selected.cl.Spd, Selected.pos);
                        ShowMoves();
                    }
                }
                
            }
            else
            {
                DeSelectUnit();
            }
        }
    }
    private void ShowMoves()
    {

        BM.SM(SelectedNode, Selected);

        
    }
    public void Hide()
    {
        BM.HM();
        if (TM.gturn == 0)
        {
            for (int i = 0; i < army1.Count; i++)
            {

                BM.US(army1.ElementAt(i));

            }
            for (int i = 0; i < BiM.city1.Count; i++)
            {

                BM.GS(BiM.city1.ElementAt(i));

            }
        }
        else
        {

            for (int i = 0; i < army2.Count; i++)
            {

                BM.US(army2.ElementAt(i));

            }
            for (int i = 0; i < BiM.city2.Count; i++)
            {

                BM.GS(BiM.city2.ElementAt(i));

            }
        }
        BM.FlipAll();
    }
    private bool ThereIsDifference()
    {

        if (SelectedNode.posX!= n.posX || SelectedNode.posY != n .posY)
        {
            if (Contains(Moveable,n)||Contains(Attackable,n))
            return true;
        }
        return false;

    }
    public float Absa(float a)
    {
        if (a > 0) { return a; } else { return -a; }
    }
    private void SelectUnit() 
    {
        if (TM.gturn == 0 && n.unit.Blue)
        {
            UnitSelected = true;
            Selected = n.unit;
            SelectedNode = n;
            NodeSelected = true;
        }
        else if(TM.gturn == 1 && !n.unit.Blue)
        {
            UnitSelected = true;
            Selected = n.unit;
            SelectedNode = n;
            NodeSelected = true;
        }
        else
        {
            DeSelectUnit();
        }
    }
    private void DeSelectUnit()
    {
        UnitSelected = false;
        NodeSelected = false;
        Hide();
    }
    private void MoveUnit()
    {
        UnitSelected = false;
        NodeSelected = false;
        if (!n.HasBuild)
        {
            if (!n.HasUnit)
            {
                Selected.Move(n);
            }
            else if (!n.unit.Blue && TM.gturn == 0)
            {
               if( Attack(Selected, n.unit))
                {
                    Selected.Move(n);
                }
            }
            else if (n.unit.Blue && TM.gturn == 1)
            {
                if (Attack(Selected, n.unit))
                {
                    Selected.Move(n);
                }
            }
        }
        else if(!n.build.Blue && TM.gturn == 0)
        {
            AttackBuild(Selected,n.build);
        }
        else if (n.build.Blue && TM.gturn == 1)
        {
            AttackBuild(Selected, n.build);
        }
        BM.LightAllDown();
    }
    public bool Attack(Unit A,Unit B)
    {
        int dmg = Mathf.RoundToInt((A.atk - B.def / 1.5f) + UnityEngine.Random.Range(0, 1.0f));
        B.tempHp -= Mathf.RoundToInt(Absa(dmg));
        A.tempHp -= Mathf.RoundToInt(Absa(Mathf.RoundToInt(dmg / 2.0f)));
        if (A.tempHp <= 0) { A.KillSelf(); if (A.Blue) { army1.Remove(A); Hide(); } else { army2.Remove(A); Hide(); } } 
        if (B.tempHp <= 0) { B.KillSelf();if (B.Blue) { army1.Remove(B); Hide(); } else { army2.Remove(B); Hide(); } return true; } else { return false; }
        
    }
    public void AttackBuild(Unit A, Building B)
    {
        int dmg = Mathf.RoundToInt((A.atk/2));
        B.Hp -= Mathf.RoundToInt(Absa(dmg));
        A.tempHp -= Mathf.RoundToInt(Absa(dmg)/3);
        if (B.Hp <= 0)
        {
            if (B.IsBase) {TM.Win(TM.gturn); } else { B.Die(); CM.OhNoABuildJustDied(); }
        }
        if (A.tempHp <= 0) { A.KillSelf(); if (A.Blue) { army1.Remove(A); Hide(); } else { army2.Remove(A); Hide(); } }

        CM.ControlUpdate();

    }

    List<Node> ToBeEvaluated = new List<Node>();
    public List<Node> Moveable = new List<Node>();
    List<Node> MoveableA = new List<Node>();
    public bool Contains(List<Node> ListA, Node a)
    {
        foreach (Node n in ListA )
        {
           if(a.posX == n.posX && a.posY == n.posY)
            {
                return true;
            }
        }
        return false;
    }
    public List<Node> Attackable;/*
    public bool IsAttackable(Node n )
    {
        if(TM.gturn == 0) {
            if(n.HasUnit || n.HasBuild){ }
        
        
        }
        else { }    
    }*/
    public void NewMovement(int s, Node b)
    {
        ToBeEvaluated = new List<Node>();
        Moveable = new List<Node>();
        Attackable = new List<Node>();
        MoveableA = new List<Node>();
        Moveable.Clear(); MoveableA.Clear(); ToBeEvaluated.Clear();
        ToBeEvaluated.Add(b);
  
        Debug.Log("A");
        for (int i = 0; i <= s; i++)
        {
            Debug.Log("B");
            foreach (Node n in ToBeEvaluated)
            {
                Debug.Log("C");
                if (Passable(n) && !Contains(Moveable,n)) { Debug.Log("D"); Moveable.Add(n); MoveableA.Add(n); }
          
                Debug.Log("E");
            }
            foreach (Node n in MoveableA)
            {
                Debug.Log("N");
                if (Contains(ToBeEvaluated, n))
                {
                    Debug.Log("P");
                    ToBeEvaluated.Remove(n);
                }
            }
            foreach(Node n in MoveableA) 
            {
                Debug.Log("F");
                try { ToBeEvaluated.Add(BM.board[n.posX + 1, n.posY]); } catch { Debug.Log("No"); }
                try { ToBeEvaluated.Add(BM.board[n.posX - 1, n.posY]); } catch { Debug.Log("No"); }
                try { ToBeEvaluated.Add(BM.board[n.posX , n.posY-1]); } catch { Debug.Log("No"); }
                try { ToBeEvaluated.Add(BM.board[n.posX , n.posY+1]); } catch { Debug.Log("No"); }
                Debug.Log("K");
            }
            Debug.Log("L");
            foreach (Node n in Moveable)
            {
                Debug.Log("W");
                if (Contains(MoveableA, n))
                {
                    Debug.Log("S");
                    MoveableA.Remove(n);
                }
            }
            Debug.Log("I");
        }
        Debug.Log("J");
    }
   
    public bool IsAdjacent(Node a, Node b)
    {
        return BM.DistX(a, b) + BM.DistY(a, b) < 1.1f;
    }
    public bool Passable(Node n)
    {
        
        if( n.Exists && !n.HasBuild) 
        {
            if (!n.HasUnit)
            {
                return true;
            }
            else
            {
                if(TM.gturn == 0)
                {
                    if (n.unit.Blue) 
                    { return true; }
                    else { Attackable.Add(n); }
                }
                if (TM.gturn == 1) 
                {
                    if (!n.unit.Blue)
                    { return true; }
                    else { Attackable.Add(n); }
                }
            }
            return false;
        }
        
        else {
            if (n.HasBuild)
            {
                if (TM.gturn == 0)
                {
                    if (n.build.Blue)
                    { return true; }
                    else { Attackable.Add(n); }
                }
                if (TM.gturn == 1)
                {
                    if (!n.build.Blue)
                    { return true; }
                    else { Attackable.Add(n); }
                }
            }



            return false; }

    }

}
