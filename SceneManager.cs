using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON_VIDE;
using PointClick;

/// <summary>
/// Switching between frames. Managing hotspots.
/// </summary>
public class SceneManager : MonoBehaviour
{
    public SceneManager Instance;

    public void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        List<string> gameFile = FileManager.ReadTextAsset("game");
        Debug.Log(string.Join("\n", gameFile));
        object data = DiagJson.Deserialize(string.Join("\n",gameFile));

//        ParseGameFile((Dictionary<string, object>) data);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ParseGameFile(Dictionary<string, object> data)
    {
        List<SceneLayout> Places = new();
        List<Item> Items = new();
/*        string name, description;
        string[] items, string[] places, string*/
        foreach(var item in data.Keys)
        {
            if (item.Contains("place:"))
            {
                string label = item.Split(':')[1];
                Debug.Log($"Processing the place '{label}'\n");
                Dictionary<string, object> tmpdict = (Dictionary<string, object>)data[item];
                tmpdict.Add("label", label);

                Places.Add(new SceneLayout(tmpdict));
            }
        }
    }
}
