using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using VNENGINE;
using System.Linq;

namespace PointClick
{
    [System.Serializable]
    public struct HotSpot
    {
        public string name, destination;
        public float xmin, ymin, xsize, ysize;
    }

    [System.Serializable]
    public struct _Item
    {
        public string name;
        public float x, y;
    }
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Place", order = 1)]
    public class SceneLayoutScriptable : ScriptableObject
    {
        public string name = string.Empty;
        public string description = string.Empty;
        public string label = string.Empty;
        public Texture image;
        bool hasBack = false, hasLeft = false, hasRight = false, hasForward = false;
        public List<HotSpot> hotSpots;
        public List<_Item> items;

        public SceneLayoutScriptable(string name, Texture image, string description = "", List<string> places = null, float[][] hotSpots = null)
        {
            this.name = name;
            this.description = description;
            this.image = image;
            this.hasBack = false;
            this.hasLeft = false;
            this.hasRight = false;
            this.hasForward = false;

            if (places != null && hotSpots != null)
            {
                if (places.Count != hotSpots.Length)
                    throw new System.ArgumentException($"Error! places list length '{places.Count}' != hotspots array length '{hotSpots.Length}'");
            }
            for (var i = 0;i < places.Count; i++)
            {
                var place = places[i];
                var hotspot = hotSpots[i];

                //this.hotSpots.Add(place, hotspot);
            }
        }

    }
}