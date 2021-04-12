using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public Building A;
    public GameObject Base;
    public GameObject RadioTower;
    public Color Blue;
    public Color Red;
    public List<Building> city1;
    public List<Building> city2;

    public void CreateBuilding(Node test, bool blu, bool IsBase) {
        //IsBase = false;
        Debug.Log("eeee");
        Debug.Log(blu); Debug.Log(IsBase);
        if (blu)
        {
            if (IsBase)
            {
                Debug.Log("2");
                A = new Building(test,Blue,Base,true,IsBase);
            }
            else
            {
                Debug.Log("llll");
                A = new Building(test, Blue, RadioTower, true, IsBase);
            }
            
            city1.Add(A);
        
        }
        else
        {
            if (IsBase)
            {
                Debug.Log("3");
                A = new Building(test, Red, Base, false, true);
            }
            else
            {
                Debug.Log("4");
                A = new Building(test, Red, RadioTower, false, false);
            }
            city2.Add(A);
        }
    }
}
