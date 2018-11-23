using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class MapElement
    {
        private Vector3 position;
        private int height;
        private int tileSize;
        private bool hasDecor;

        public MapElement(Vector3 position, int height, int tileSize, bool hasDecor = false)
        {
            this.Position = position;
            this.Height = height;
            this.TileSize = tileSize;
            this.hasDecor = hasDecor;

        }

        public Vector3 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public int TileSize
        {
            get
            {
                return tileSize;
            }

            set
            {
                tileSize = value;
            }
        }

        public bool HasDecor
        {
            get
            {
                return hasDecor;
            }

            set
            {
                hasDecor = value;
            }
        }
    }
}
