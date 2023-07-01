using Articy.Unity.Interfaces;
using PointClick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ButtonManager
{
    public ButtonManager instance;
    private string BUTTON_NAME_FORMAT = "Button from {0} to {1}";
    private string IMAGE_BUTTON_NAME_FORMAT = "Item {0}";

    //    public void Awake() { instance = this; }

    public GameObject SquareButton(string from, string to, Transform panel, HotSpot spot)
    {
        Quaternion rot = Quaternion.identity;
        GameObject buttonPrefab = Resources.Load<GameObject>("Prefabs\\SquareButton");
        if (buttonPrefab == null)
            Debug.LogError($"Cannot find button prefab");
        GameObject gameObject = GameObject.Instantiate(buttonPrefab, Vector3.zero, rot);
        gameObject.name = string.Format(BUTTON_NAME_FORMAT, from, to);
        gameObject.transform.SetParent(panel);

        Button but = gameObject.GetComponent<Button>();

        but.onClick.AddListener(() => { GameBehavior.Instance.ChangeScene(to); });

        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;

        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = Vector2.zero;
        rect.offsetMin = new Vector2(spot.xmin, -spot.ymin   -spot.ysize);
        rect.offsetMax = new Vector2(spot.xmin + spot.xsize, -spot.ymin);

        return gameObject;
    }
    public GameObject ImageButton(Texture2D image, string name, Transform panel, float x, float y)
    {
        Quaternion rot = Quaternion.identity;
        GameObject buttonPrefab = Resources.Load<GameObject>("Prefabs\\ImageButton");
        if (buttonPrefab == null)
            Debug.LogError($"Cannot find button prefab");
        GameObject gameObject = GameObject.Instantiate(buttonPrefab, Vector3.zero, rot);
        gameObject.name = string.Format(IMAGE_BUTTON_NAME_FORMAT, name);
        gameObject.transform.SetParent(panel);
        

        Button but = gameObject.GetComponent<Button>();
        Sprite sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(x, -y));
        but.image.sprite = sprite;
//        but.onClick.AddListener(() => { GameBehavior.Instance.ChangeScene(to); });

        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;

        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = Vector2.zero;
        rect.offsetMin = new Vector2(x, -y - image.height);
        rect.offsetMax = new Vector2(x + image.width, y);
        but.image.SetNativeSize();

        return gameObject;
    }
}
