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

    public void SwitchFloors(int targetFloor)
    {
        prevFloor = Globals.selectedFloor;
        nextFloorIndex = targetFloor;
      
        switch (prevFloor) 
        {
            case Globals.floorType.history: prevFloorIndex = 0; break;
            case Globals.floorType.technology: prevFloorIndex = 1; break;
            case Globals.floorType.literature: prevFloorIndex = 2; break;
            case Globals.floorType.art: prevFloorIndex = 3; break;
        }
        switch (nextFloorIndex) 
        {
            case 0: Globals.selectedFloor = Globals.floorType.history; break;
            case 1: Globals.selectedFloor = Globals.floorType.technology; break;
            case 2: Globals.selectedFloor = Globals.floorType.literature; break;
            case 3: Globals.selectedFloor = Globals.floorType.art; break;
        }

        if (canSwitch) // don't allow user to switch floors during a switch
            StartCoroutine(FadeOut(floors[prevFloorIndex]));
    }


    IEnumerator FadeOut(Transform floor)
    {
        float elapsedTime = 0f;
        float waitTime = 5f;

        List<Transform> children = new List<Transform>();
        AddChildrenToList(floor, children);

        while (elapsedTime < waitTime)
        {
            foreach (Transform item in children)
            {
                if (item.gameObject.GetComponent<Renderer>() != null)
                {
                    item.gameObject.GetComponent<Renderer>().material.SetFloat("_Mode", 2);
                    var matColor = item.gameObject.GetComponent<Renderer>().material.color;
                    matColor.a = Mathf.Lerp(matColor.a, 0, elapsedTime / waitTime);
                    item.gameObject.GetComponent<Renderer>().material.color = matColor;
                }
            }
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        foreach (Transform item in children)
            item.gameObject.SetActive(false);
        floor.gameObject.SetActive(false);

        StartCoroutine(FadeIn(floors[nextFloorIndex]));
        yield return null;
    }


    IEnumerator FadeIn(Transform floor)
    {
        canSwitch = false;
        float elapsedTime = 0f;
        float waitTime = 5f;

        List<Transform> children = new List<Transform>();
        AddChildrenToList(floor, children);
        
        while (elapsedTime < waitTime)
        {
            foreach (Transform item in children)
            {
                if (item.gameObject.GetComponent<Renderer>() != null)
                {
                    item.gameObject.GetComponent<Renderer>().material.SetFloat("_Mode", 2);
                    var matColor = item.gameObject.GetComponent<Renderer>().material.color;
                    matColor.a = Mathf.Lerp(matColor.a, 1, elapsedTime / waitTime);
                    item.gameObject.GetComponent<Renderer>().material.color = matColor;
                }
            }
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        foreach (Transform item in children)
            item.gameObject.SetActive(true);
        floor.gameObject.SetActive(true);

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
