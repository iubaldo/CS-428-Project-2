using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    bool canSwitch = true;
    public List<Transform> floors;
    Globals.floorType prevFloor;

    int prevFloorIndex = 0;
    int nextFloorIndex = 0;

    void SwitchFloors(Globals.floorType targetFloor)
    {
        prevFloor = Globals.selectedFloor;
        Globals.selectedFloor = targetFloor;
      
        switch (prevFloor) 
        {
            case Globals.floorType.history: prevFloorIndex = 0; break;
            case Globals.floorType.technology: prevFloorIndex = 1; break;
            case Globals.floorType.literature: prevFloorIndex = 2; break;
            case Globals.floorType.art: prevFloorIndex = 3; break;
        }
        switch (Globals.selectedFloor) 
        {
            case Globals.floorType.history: nextFloorIndex = 0; break;
            case Globals.floorType.technology: nextFloorIndex = 1; break;
            case Globals.floorType.literature: nextFloorIndex = 2; break;
            case Globals.floorType.art: nextFloorIndex = 3; break;
        }

        if (canSwitch) // don't allow user to switch floors during a switch
            StartCoroutine(FadeOut(floors[prevFloorIndex]));
    }


    IEnumerator FadeIn(Transform floor)
    {
        canSwitch = false;
        float elapsedTime = 0f;
        float waitTime = 8f;

        List<Transform> children = new List<Transform>();
        AddChildrenToList(floor, children);
        foreach (Transform item in children)
            item.gameObject.SetActive(true);
        //while (elapsedTime < waitTime)
        //{
        //    foreach(Transform item in children)
        //    {
        //        if (item.gameObject.GetComponent<Renderer>() != null)
        //        {
        //            var color = item.gameObject.GetComponent<Renderer>().material.color;
        //            color.a = Mathf.Lerp(color.a, 1, elapsedTime / waitTime);
        //        }
        //    }
        //    elapsedTime += Time.deltaTime;

        //    yield return null;
        //}

        StartCoroutine(FadeIn(floors[nextFloorIndex]));
        yield return null;
    }


    IEnumerator FadeOut(Transform floor)
    {
        float elapsedTime = 0f;
        float waitTime = 5f;

        List<Transform> children = new List<Transform>();
        AddChildrenToList(floor, children);
        foreach (Transform item in children)
            item.gameObject.SetActive(false);

        //while (elapsedTime < waitTime)
        //{
        //    foreach (Transform item in children)
        //    {
        //        if (item.gameObject.GetComponent<Renderer>() != null)
        //        {
        //            var color = item.gameObject.GetComponent<Renderer>().material.color;
        //            color.a = Mathf.Lerp(color.a, 0, elapsedTime / waitTime);
        //        }
        //    }
        //    elapsedTime += Time.deltaTime;

        //    yield return null;
        //}

        canSwitch = true;
        yield return null;
    }


    void AddChildrenToList(Transform parent, List<Transform> children)
    {
        foreach (Transform child in parent)
        {
            children.Add(child);
            AddChildrenToList(child, children);
        }         
    }
}
