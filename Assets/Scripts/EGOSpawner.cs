using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EGOSpawner : MonoBehaviour
{
    public GameObject spawnPoint;
    public List<GameObject> historyEGO;
    public List<GameObject> technologyEGO;
    public List<GameObject> literatureEGO;
    public List<GameObject> artEGO;


    // spawns a copy of the selected item
    public void Dispense(int index)
    {
        GameObject item;
        switch(Globals.selectedFloor)
        {
            case Globals.floorType.history:
                item = Instantiate(historyEGO[index], spawnPoint.transform.position, Quaternion.identity);
                break;
            case Globals.floorType.technology:
                item = Instantiate(historyEGO[index], spawnPoint.transform.position, Quaternion.identity);
                break;
            case Globals.floorType.literature:
                item = Instantiate(historyEGO[index], spawnPoint.transform.position, Quaternion.identity);
                break;
            case Globals.floorType.art:
                item = Instantiate(historyEGO[index], spawnPoint.transform.position, Quaternion.identity);
                break;
        }
    }
}
