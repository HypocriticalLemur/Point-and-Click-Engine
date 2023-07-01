using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VNENGINE;

public class SquareButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Globals globals;
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        globals = Globals.instance;
    }
    
    // Update is called once per frame
    void Update()
    {/*
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log($"{name} update selected");
        }*/
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log($"{name} pointer enter");
        globals.mouseMode = Globals.MouseMode.hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        globals.mouseMode = Globals.MouseMode.basic;
        //Debug.Log($"{name} pointer exit");
    }

}
