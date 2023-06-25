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
    public class SceneLayout
    {
        public string name = string.Empty;
        public string description = string.Empty;
        private string label = string.Empty;
        private string image = string.Empty;
        private bool hasBack = false, hasLeft = false, hasRight = false, hasForward = false;
        private Dictionary<string, float[]> hotSpots;
        private List<string> places;
        private List<string> items;

        public SceneLayout(string name, string description = "", string image = "", List<string> places = null, float[][] hotSpots = null)
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

                this.hotSpots.Add(place, hotspot);
            }
        }

        public SceneLayout(Dictionary<string, object> data)
        {
            foreach (var item in data.Keys)
            {
                var prop = this.GetType().GetField(item, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (prop == null)
                {
                    Debug.LogWarning($"Cannot find field {item}");
                }
                else
                {
                    Type type = prop.FieldType;
                    //prop = new type();
                    /*if (type.IsGenericType)
                    {
                        object genProp = Activator.CreateInstance(type);

                        prop.SetValue(this, genProp);
                        Debug.Log($"Generic {prop.GetValue(this)}");
                    }
                    else*/
                    if (item == "places" || item == "items")
                    {
                        List<object> tmpobj = (List<object>)data[item];
                        List<string> tmpstr = tmpobj.Select(i => i.ToString()).ToList();
                        prop.SetValue(this, tmpstr);
                    }
                    else if (item == "hotSpots")
                        prop.SetValue(this, (Dictionary<string, float[]>)data[item]);
                    else
                        prop.SetValue(this, data[item]);
                }
            }
            if (name == string.Empty)
                throw new System.ArgumentException($"Error! Place {data["label"]} does not contain 'name' field!");
        }

        public T GetInstance<T>(Type type)
        {
            return (T)Activator.CreateInstance(type);
        }
    }
}