using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public List<GameObject> floors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SwitchFloors(Globals.floorType targetFloor)
    {
        Globals.selectedFloor = targetFloor;


    }
}
