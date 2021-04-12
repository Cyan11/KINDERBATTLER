using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Unit unit;
    public bool HasUnit;
    public bool CanMove = true;
    public bool Flipped = false;
    public bool Exists = true;
    public bool HasBuild;
    public Building build;
    public GameObject Spritee;
    public GameObject Shiny;public GameObject Hidden; public GameObject Moveable;
    public Sprite reg;
    public Sprite white;
    public bool IsLitUp = false;
    public bool GridShown = true;
    public int posX;
    public int posY;
    public Vector2 WorldPos;
    public float Right;
    public float Left;
    public bool Revealed;
    public float Down;
    public float Up;
    private GameObject Noo;
    public BoardManager BM;

    public Node(int _posX, int _posY, GameObject NodeSprite, GameObject Light, GameObject _No, GameObject H, GameObject M) { posX = _posX;posY = _posY;
        Noo = _No; Spritee =  Instantiate(NodeSprite, new Vector2((posX-3)*1.7f, (posY-2.5f)*1.7f), Quaternion.identity);
        Shiny = Instantiate(Light, new Vector2((posX - 3) * 1.7f, (posY - 2.5f) * 1.7f), Quaternion.identity);
        Shiny.SetActive(false);
        Moveable = Instantiate(M, new Vector2((posX - 3) * 1.7f, (posY - 2.5f) * 1.7f), Quaternion.identity);
        Moveable.SetActive(false);
        if (Exists)
        {
            Hidden = Instantiate(H, new Vector2((posX - 3) * 1.7f, (posY - 2.5f) * 1.7f), Quaternion.identity);
            Hidden.SetActive(false);
        }
        WorldPos = new Vector2((posX - 3) * 1.7f, (posY - 2.5f) * 1.7f);
        Right = ((posX - 3) * 1.7f)+1.0f;Left = ((posX - 3) * 1.7f)-0.8f; Up = ((posY - 2.5f) * 1.7f)+0.6f; Down = ((posY - 2.5f) * 1.7f-1.1f);
       
    }
    
    public void HideGrid() { if (GridShown && Exists) { Spritee.SetActive(false); Shiny.SetActive(false); } GridShown = false; }public void ShowGrid() { if (!GridShown && Exists) { Spritee.SetActive(true); Shiny.SetActive(true); } GridShown = true; }
    public void AlternateGrid() { if (GridShown && Exists) { Spritee.SetActive(false); Shiny.SetActive(false); GridShown = false; } else if (!GridShown) { Spritee.SetActive(true); Shiny.SetActive(true); GridShown = true; } }
    public void DeleteSelf() { 
        Spritee.SetActive(false); Shiny.SetActive(false); CanMove = false; Exists = false;
        Instantiate(Noo, new Vector2((posX - 3) * 1.7f, (posY - 2.5f) * 1.7f), Quaternion.identity);
    }
    public void AddUnit(Unit _unit) { HasUnit = true; unit = _unit; }
    public void RemoveUnit() { HasUnit = false;  }
    public void AddBuild(Building _build) { HasBuild = true; build = _build; }
    public void RemoveBuild() { HasBuild = false; }
    public void Reveal() { if (Exists) { Revealed = true;Flipped = true; ; Hidden.SetActive(false); } }
    public void Hide() { if (Exists) { Revealed = false; Hidden.SetActive(true); } }
    public void ShowMove() { Moveable.SetActive(true); }
    public void HideMove() { Moveable.SetActive(false); }
    public void LightUp() {
        if (Exists)
        {
            IsLitUp = true;
            Spritee.SetActive(false); Shiny.SetActive(true);
        }
    } public void LightDown() {
        if (Exists)
        {
            IsLitUp = false;
            Spritee.SetActive(true); Shiny.SetActive(false);
        }
    }
 
    


}
