using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class MapGraph
    {
        private List<Node<MapElement>> nodeSet;
        private int index;

        public MapGraph()
        {
            this.index = 0;
            this.NodeSet = new List<Node<MapElement>>();
        }


        public void AddNode(Node<MapElement> element)
        {
            element.Index = index;
            NodeSet.Add(element);
            index++;
        }

        public void ConnectNode(Node<MapElement> from, Node<MapElement> to)
        {
            from.Childs.Add(to);
        }

        public List<Node<MapElement>> NodeSet
        {
            get
            {
                return nodeSet;
            }

            set
            {
                nodeSet = value;
            }
        }

        public class Node<MapElement>
        {
            private List<Node<MapElement>> childs;
            private int index;
            private MapElement value;
            private int weight;

            public Node(MapElement value, int weight, int index = -1)
            {
                this.Index = index;
                this.value = value;
                this.weight = weight;
                this.childs = new List<Node<MapElement>>();
            }

            public MapElement Value
            {
                get
                {
                    return value;
                }

                set
                {
                    this.value = value;
                }
            }

            public List<Node<MapElement>> Childs
            {
                get
                {
                    return childs;
                }

                set
                {
                    childs = value;
                }
            }

            public int Weight
            {
                get
                {
                    return weight;
                }

                set
                {
                    weight = value;
                }
            }

            public int Index
            {
                get
                {
                    return index;
                }

                set
                {
                    index = value;
                }
            }
        }
    }
}
