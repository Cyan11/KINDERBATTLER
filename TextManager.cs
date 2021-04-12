using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{

    public TextMeshProUGUI unitType;
    public TextMeshProUGUI details;
    bool Selected = false;
    string r = "Recon";
    string t = "Tank";
    string i = "Shield";
    private void Awake()
    {
        unitType.SetText("Nothing   Selected");
        details.SetText("");
    }
    public void DisplayTile(Node n)
    {
        Selected = true;
        string s;
        
        if (n.HasUnit)
        {
            if (n.unit.Blue) { s = "(P1)"; } else { s = "(P2)"; }
            string k;
            if (n.unit.Maintained) { k = "Not Bored"; } else { k = "Bored"; }
            unitType.SetText("Unit: " + n.unit.cl.Type + "    " + s);
            details.SetText("Attack : " + n.unit.atk + "   " + "    HP: "+n.unit.tempHp + "/"+n.unit.hp + "   "+"  Def: " +n.unit.def + "          " + k );
        }
        else if (n.HasBuild)
        {
            unitType.SetText("Build: " + n.build.name);
            details.SetText("HP: " + n.build.Hp + "/"+n.build.MaxHp);
        }
        else if (n.Exists)
        {
            unitType.SetText("Unit: " + "None");
            details.SetText("");
        }
    }
    public void Hide() {
        Selected = false;
        unitType.SetText("Nothing   Selected"); details.SetText("");
    }
}
