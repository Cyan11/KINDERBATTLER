using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlManager : MonoBehaviour
{
   public float Control1 = 7;
    public float ControlTurn1 = 4;
    public float Control2 =6;
    public float CurrentControl;
    private float ControlTurn2 = 5;
    public TurnManager TM;
    public TextMeshProUGUI ControlText;
    public BuildManager BM;
    private void _ControlUpdate()
    {
        if (TM.gturn == 0) {
            CurrentControl = Control1;
            ControlText.SetText(Control1.ToString()+" (+"+ControlTurn1.ToString()+")");
        }
        else
        {
            CurrentControl = Control2;
            ControlText.SetText(Control2.ToString() + " (+"+ ControlTurn2.ToString()+")");
        }
    }
    public void NewTurn()
    {
        if (TM.gturn == 0)
        {
            Control1 += ControlTurn1;
            ControlUpdate();
        }
        else
        {
            Control2 += ControlTurn2;
            ControlUpdate();
        }
    }
    public void ControlUpdate()
    {
        _ControlUpdate();
    }
    private void Awake()
    {
        _ControlUpdate();
    }
    public void ControlChange(int n)
    {
        if(TM.gturn == 0)
        {
            Control1 -= n;
            ControlUpdate();
        }
        else
        {
            Control2 -= n;
            ControlUpdate();
        }
    }
    /*
    public void TurnUpdate()
    {
        ControlTurn1 = 3 + BM.city1.Count;
        ControlTurn2 = 4 + BM.city2.Count;
    }*/
    public void PermanentControlChange(int n)
    {
        if (TM.gturn == 0)
        {
            ControlTurn1 += n;
        }
        else
        {
            ControlTurn2 += n;
        }
    }
    public void OhNoABuildJustDied()
    {
        if (TM.gturn != 0)
        {
            ControlTurn1 -= 1;
        }
        else
        {
            ControlTurn2 -= 1;
        }
    }
}
