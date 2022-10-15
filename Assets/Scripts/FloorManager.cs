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

    //Grab the Audio from the record player.
    AudioSource recordPlayer = GameObject.Find("/AllObjects/Floors/RecordPlayer").GetComponent<AudioSource>();

    //recordPlayer.clip = 


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

        if (canSwitch)// don't allow user to switch floors during a switch
        {
            canSwitch = false;
            StartCoroutine(FadeOut(floors[prevFloorIndex]));
        }      
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
                if (item.gameObject.GetComponent<MeshRenderer>() != null)
                {
                    Material mat = item.gameObject.GetComponent<MeshRenderer>().material;

                    mat.SetFloat("_Mode", 2); // set object rendering to fade mode, the rest is because of a known bug in Unity
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.EnableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = 3000;

                    Color matColor = mat.color;
                    matColor.a = Mathf.Lerp(matColor.a, 0, elapsedTime / waitTime);
                    mat.color = matColor;
                }
            }
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        foreach (Transform item in children) // make all objects inactive since they're not visible
            item.gameObject.SetActive(false);
        floor.gameObject.SetActive(false);

        StartCoroutine(FadeIn(floors[nextFloorIndex]));
        yield return null;
    }


    IEnumerator FadeIn(Transform floor)
    {
        float elapsedTime = 0f;
        float waitTime = 5f;

        List<Transform> children = new List<Transform>();
        AddChildrenToList(floor, children);

        foreach (Transform item in children) 
        {
            item.gameObject.SetActive(true);// reactivate all objects to make them visible during fade in

            if (item.gameObject.GetComponent<Renderer>() != null)
            {
                Material mat = item.gameObject.GetComponent<MeshRenderer>().material; // reset alpha to 0
                Color matColor = mat.color;
                matColor.a = 0;
                mat.color = matColor;
            }            
        }
            
        floor.gameObject.SetActive(true);

        while (elapsedTime < waitTime)
        {
            foreach (Transform item in children)
            {
                if (item.gameObject.GetComponent<Renderer>() != null)
                {
                    Material mat = item.gameObject.GetComponent<MeshRenderer>().material;

                    mat.SetFloat("_Mode", 2);
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.EnableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = 3000;

                    Color matColor = mat.color;
                    matColor.a = Mathf.Lerp(matColor.a, 1, elapsedTime / waitTime);
                    mat.color = matColor;
                }
            }
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        foreach (Transform item in children) // set objects back to opaque after fading in
        {
            if (item.gameObject.GetComponent<MeshRenderer>() != null) 
            {
                Material mat = item.gameObject.GetComponent<MeshRenderer>().material;
                mat.SetFloat("_Mode", 0);
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
            }
        }

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
