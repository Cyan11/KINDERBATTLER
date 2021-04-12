using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{


    public GameObject cursor;
    public BuildManager BM;
    public GameObject NodeSprite;
    public Sprite white;
    public Sprite reg;
    public GameObject Light;
    public GameObject A;
    public GameObject U;
    public GameObject Mountain;
    public GameObject Water;
    public GameObject Moveable;
    public GameObject Hidden;
    public GameObject City;
    public GameObject Radio;
    public SpriteRenderer Dark;
    public TextManager TM;
    public UnitManager UM;
    public UnitType[] Types;
    public GameObject h;
    int SizeX;
    int SizeY;
    public Node[,] board;
    // public GameObject DL;public GameObject DR;public GameObject TR;public GameObject TL;
    // public GameObject Cursor;


    Node[,] CreateBoard(int _SizeX, int _SizeY)
    {
        if (UnityEngine.Random.Range(0, 2) == 1) { h = Mountain; } else { h = Water; }
        // SizeX++;SizeY++;
        int[,] _board = new int[_SizeX, _SizeY];
        Node[,] board = new Node[_SizeX, _SizeY];
        for (int x = 0; x < _SizeX; x++)
        {
            for (int y = 0; y < _SizeY; y++)
            {
                board[x, y] = new Node(x, y, NodeSprite, Light,h, Hidden, Moveable);
            }
        }
        SizeX = _SizeX; SizeY = _SizeY;
        return board;
    }
    public bool Suc = false;
    public bool UnitSuc = false;
    public Node selected;
    public void SelectNode()
    {
        Node n = NodeFromCursor();
        if (Suc && n.Exists)
        {
            selected = n;
            if (!n.IsLitUp)
            {
              
                if (n.Revealed)
                {
                    LightNode(n);
                    TM.DisplayTile(n);
                }
            }
            else
            {
                LightNodeDown(n);
                TM.Hide();
                UnitSuc = false;
            }
        }
        else
        {
            LightAllDown();
            TM.Hide();
        }

    }
    private void RemoveNode(int x, int y)
    {
        board[x -1, y -1].DeleteSelf();

    }

    private void Awake()
    {
        U.SetActive(true); A.SetActive(true);
        Screen.SetResolution(1080, 480, true);
        board = CreateBoard(7, 6);
        int i = UnityEngine.Random.Range(0, 7);
        Debug.Log(i);
        switch (i)
         {
            
            default:
                RemoveNode(3, 2); RemoveNode(2, 3); RemoveNode(2, 4); RemoveNode(3, 4); RemoveNode(3, 5); UM.CreateUnit(board[0, 0], true, Types[UnityEngine.Random.Range(0, 3)]);
             
                UM.CreateUnit(board[0, 1], true, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[6, 5], false, Types[UnityEngine.Random.Range(0, 3)]);
                BM.CreateBuilding(board[1, 1], true, true);
                BM.CreateBuilding(board[5, 5], false, true);
                UM.CreateUnit(board[6, 4], false, Types[UnityEngine.Random.Range(0, 3)]); return;
            case 1:
                 RemoveNode(4, 4); RemoveNode(4, 5); RemoveNode(4, 3); RemoveNode(4, 6); RemoveNode(3, 4); UM.CreateUnit(board[0, 0], true, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[0, 1], true, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[6, 5], false, Types[UnityEngine.Random.Range(0, 3)]);
                BM.CreateBuilding(board[1, 5], true, true);
                BM.CreateBuilding(board[5, 5], false, true);
                UM.CreateUnit(board[6, 4], false, Types[UnityEngine.Random.Range(0, 3)]); return;


             case 2:
                RemoveNode(3, 3); RemoveNode(3, 4); RemoveNode(4, 3); RemoveNode(4, 5); RemoveNode(3, 5); UM.CreateUnit(board[0, 0], true, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[0, 1], true, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[6, 5], false, Types[UnityEngine.Random.Range(0, 3)]);
                BM.CreateBuilding(board[1, 1], true, true);
                BM.CreateBuilding(board[5, 5], false, true);
                UM.CreateUnit(board[6, 4], false, Types[UnityEngine.Random.Range(0, 3)]); return;

            case 3:
                 RemoveNode(7, 1); RemoveNode(6, 2); RemoveNode(6, 1); RemoveNode(6, 1); RemoveNode(2, 1); UM.CreateUnit(board[0, 0], true, Types[UnityEngine.Random.Range(0, 3)]);
                BM.CreateBuilding(board[0, 1], true, true);
                BM.CreateBuilding(board[5, 5], false, true);

                UM.CreateUnit(board[1, 1], true, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[6, 5], false, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[6, 4], false, Types[UnityEngine.Random.Range(0, 3)]); ; return;

             case 4:
                 RemoveNode(4, 2); RemoveNode(5, 2); RemoveNode(4, 4); RemoveNode(4, 5); RemoveNode(4, 1); RemoveNode(4, 6); RemoveNode(3, 4); UM.CreateUnit(board[0, 0], true, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[0, 1], true, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[6, 5], false, Types[UnityEngine.Random.Range(0, 3)]);
                BM.CreateBuilding(board[1, 1], true, true);
                BM.CreateBuilding(board[5, 5], false, true);
                UM.CreateUnit(board[6, 4], false, Types[UnityEngine.Random.Range(0, 3)]); return;

             case 5:
                 RemoveNode(2, 1); RemoveNode(2, 2); RemoveNode(2, 3); RemoveNode(7, 2); RemoveNode(4, 2); UM.CreateUnit(board[1,3], true, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[0, 1], true, Types[UnityEngine.Random.Range(0, 3)]);
                BM.CreateBuilding(board[0, 0], true, true);
                BM.CreateBuilding(board[5, 5], false, true);
                UM.CreateUnit(board[6, 5], false, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[6, 4], false, Types[UnityEngine.Random.Range(0, 3)]);
                return;

             case 6:
                 RemoveNode(4, 4); RemoveNode(4, 5); RemoveNode(4, 3); RemoveNode(4, 6); RemoveNode(4, 4); RemoveNode(4, 2); UM.CreateUnit(board[0, 0], true, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[0, 1], true, Types[UnityEngine.Random.Range(0, 3)]);
                BM.CreateBuilding(board[1, 1], true, true);
                BM.CreateBuilding(board[5, 5], false, true);
                UM.CreateUnit(board[6, 5], false, Types[UnityEngine.Random.Range(0, 3)]);
                UM.CreateUnit(board[6, 4], false, Types[UnityEngine.Random.Range(0, 3)]); return;
         }
        
    }

    private void Update()
    {
        // Debug.Log(board[0, 0].Right); Debug.Log(board[0, 0].Left); Debug.Log(board[0, 0].Down); Debug.Log(board[0, 0].Up);
        if (Input.GetMouseButtonDown(1)) { U.SetActive(false); A.SetActive(false); }
        if (Input.GetMouseButtonUp(1)) { U.SetActive(true); A.SetActive(true); }
        Node n = NodeFromCursor();
            if (n.Revealed) {
            TM.DisplayTile(n); }
    }
    public Node NodeFromCursor()
    {
        Transform Pcursor = cursor.transform;
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                if (board[x, y].Exists)
                {
                    if (Pcursor.position.y > board[x, y].Down && Pcursor.position.y < board[x, y].Up && Pcursor.position.x > board[x, y].Left && Pcursor.position.x < board[x, y].Right)
                    { Suc = true; return board[x, y]; }
                }
            }
        }
        Suc = false;
        return board[0, 0];
    }
    public void ShowGrid()
    {
        Debug.Log("Grid here!");
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                board[x, y].ShowGrid();
            }
        }
    }
    public void AlternateGrid()
    {
       // Debug.Log("Grid here!");
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                board[x, y].AlternateGrid();
            }
        }
    }
    public void LightAllUp()
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                board[x, y].LightUp();
            }
        }
    }
    public void LightNode(Node n)
    {

        LightAllDown();
        n.LightUp();

    }
    public void LightNodeDown(Node n)
    {
        n.LightDown();
        TM.Hide();
    }

    public void LightAllDown()
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                board[x, y].LightDown();
            }
        }
    }
    public void LightUp(int x, int y)
    {
        LightAllDown();
        board[x, y].LightUp();
    }
    public void LightDown(int x, int y)
    {
        board[x, y].LightDown();
    }

    public void HideGrid()
    {
        Debug.Log("Grid where?");
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                board[x, y].HideGrid();
            }
        }
    }
    public float DistX(Node a, Node b)
    {
        return UM.Absa(a.posX - b.posX);
    }
    public float DistY(Node a, Node b)
    {
        return UM.Absa(a.posY - b.posY);
    }
    public void SM(Node b, Unit Selectedd)
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                for (int i = 0; i < UM.Moveable.Count; i++)
                {
                    if(board[x,y].posX == UM.Moveable.ElementAt(i).posX && board[x, y].posY == UM.Moveable.ElementAt(i).posY) 
                    { board[x, y].ShowMove(); }
                    else if (UM.Contains(UM.Attackable, board[x, y])) { board[x, y].ShowMove(); }
                }
                    
               
            
            }
        }

    }
   
    public void HM()
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                board[x, y].HideMove();
            }
        }

    }
    public void US(Unit A)
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                if (DistX(board[x, y], A.pos) + DistY(board[x, y], A.pos) > A.cl.Sight + 0.5f && !board[x, y].Flipped)
                {
                    board[x, y].Hide();
                }
                else
                {
                    board[x, y].Reveal();
                }
            }
        }
    }
    public void GS(Building A)
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                if (DistX(board[x, y], A.pos) + DistY(board[x, y], A.pos) > 1 && !board[x, y].Flipped)
                {
                    board[x, y].Hide();
                }
                else
                {
                    board[x, y].Reveal();
                }
            }
        }
    }
    public void FlipAll()
    {

        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                board[x, y].Flipped = false;
            }
        }
    }
}
