using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public BoardManager BM;
    public BuildManager BiM;
    public ControlManager CM;
    public TurnManager TM;
    public UnitManager UM;
    public GameObject Atks;
    private int times = 0;
    private void Awake()
    {
        Atks.SetActive(false);
    }
    public void Maintain()
    {
        try{
            if (BM.selected.HasUnit)
            {
                if (BM.selected.unit.Maintaine != 4 && CM.CurrentControl>0)
                {
                    if (TM.gturn == 0) { Debug.Log("d's"); CM.Control1--; CM.ControlUpdate(); } else { Debug.Log("d'ssf"); CM.Control2--; CM.ControlUpdate(); }
                    BM.selected.unit.Maintain();
                }
            }
        }
        catch { }
    }
    public void Build()
    {
        try
        {
           
            if (BM.selected.Revealed && !BM.selected.HasUnit && !BM.selected.HasBuild && CM.CurrentControl>2)
            {
               
                CM.ControlChange(3);
                if (TM.gturn == 0) {
                    CM.PermanentControlChange(1);
                    BiM.CreateBuilding(BM.selected,true,false); }
                else
                {
                    CM.PermanentControlChange(1);
                    BiM.CreateBuilding(BM.selected, false, false);
                }
            }
            
        }
        catch { }
        CM.ControlUpdate();

    }
    public void Upgrade()
    {
        try
        {
            if (BM.selected.HasUnit && CM.CurrentControl > 2)
            {
                    if (TM.gturn == 0) { Debug.Log("d's"); CM.Control1-=2; CM.ControlUpdate(); } else { Debug.Log("d'ssf"); CM.Control2-=2; CM.ControlUpdate(); }
                    BM.selected.unit.Upgrade();
              
            }
        }
        catch { }
    }
    public void Steal()
    {
        try
        {
            if (BM.selected.HasUnit && CM.CurrentControl>3)
        {
            if (TM.gturn == 0)
            {
                if (!BM.selected.unit.Maintained && !BM.selected.unit.Blue)
                {
                    BM.selected.unit.Blue = true;
                    UM.army2.Remove(BM.selected.unit);
                    UM.army1.Add(BM.selected.unit);
                    BM.selected.unit.unitSprite.GetComponent<SpriteRenderer>().color = UM.blue;

                }
            }
            else
            {
                if (!BM.selected.unit.Maintained && BM.selected.unit.Blue)
                {
                    Debug.Log("yayssad");
                    BM.selected.unit.Blue = false;                    
                    UM.army1.Remove(BM.selected.unit);
                    UM.army2.Add(BM.selected.unit);
                    BM.selected.unit.unitSprite.GetComponent<SpriteRenderer>().color = UM.red;
                }
            }
            CM.ControlChange(3);
            }
        }
        catch { }
    }
    public void Recruit()
    {
        if (times % 2 == 0)
        {
            Atks.SetActive(true);
        }
        else
        {
            Atks.SetActive(false);
        }
        times++;
        
    }
    public Node _N;
    public bool blue;
    public void RecruitScout()
    {
        try
        {
            if (CM.CurrentControl > 1&& !BM.selected.HasUnit && !BM.selected.HasBuild)
            {
                if (TM.gturn == 0) { blue = true; } else { blue = false; }
                if (BM.selected.Revealed)
                {
                    UM.CreateUnit(BM.selected, blue, BM.Types[1]);
                    CM.ControlChange(2);
                }
            }
            
        }
        catch { }

        Atks.SetActive(false);
        times++;

    }
    public void Tank()
    {
        try
        {
            if (CM.CurrentControl > 2 && !BM.selected.HasUnit && !BM.selected.HasBuild)
            {
                if (TM.gturn == 0) { blue = true; } else { blue = false; }
                if (BM.selected.Revealed)
                {
                    UM.CreateUnit(BM.selected, blue, BM.Types[2]);
                    CM.ControlChange(3);
                }
            }
        }
        catch { }

        Atks.SetActive(false);
        times++;
    }
    public void RecruitShield()
    {
        try
        {
            if (CM.CurrentControl > 1&& !BM.selected.HasUnit && !BM.selected.HasBuild)
            {
                if (TM.gturn == 0) { blue = true; } else { blue = false; }
                if (BM.selected.Revealed)
                {
                    UM.CreateUnit(BM.selected, blue, BM.Types[0]);
                    CM.ControlChange(2);
                }
            }
        }
        catch { }

        Atks.SetActive(false);
        times++;
    }
}
