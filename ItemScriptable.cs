using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VNENGINE;

namespace PointClick
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 2)]
    public class ItemScriptable : ScriptableObject
    {
        private ButtonManager buttonManager;
        private GameBehavior gameManager = null;
        private Inventory<Item> inventory = null;
        public bool collectable;
        public string name = string.Empty;
        public string description = string.Empty;
        public string icon = string.Empty;
        public Texture2D texture;
        public Coordinate position;

        public void DrawItem(Transform panel)
        {
            buttonManager = new();
            buttonManager.ImageButton(texture, name, panel, position.x, position.y);
        }
    }
}