using System;
using UnityEditor;
using UnityEngine;

namespace TP
{
    public abstract class Node
    {
        public int id;
        public string name;
        public Rect rect;
        public string title;
        public bool isDragged;
        public bool isSelected;

        public ConnectionPoint[] inPoint;
        public ConnectionPoint[] outPoint;

        public GUIStyle style;
        public GUIStyle inPointStyle;
        public GUIStyle outPointStyle;
        public GUIStyle defaultNodeStyle;
        public GUIStyle selectedNodeStyle;

        public Action<Node> OnRemoveNode;

        protected abstract void SetStyles();

        public void Draw()
        {
            for (int i = 0; i < inPoint.Length; i++)
                inPoint[i].Draw();

            for (int i = 0; i < outPoint.Length; i++)
                outPoint[i].Draw();

            // Node background
            GUI.Box(rect, title, style);

            // Node title
            GUIStyle textStyle = new GUIStyle();
            textStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(rect.x + 15, rect.y + 7, 100, 20), name, textStyle);

            DrawContents();
        }

        protected abstract void DrawContents();

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        public abstract void Process();

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            GUI.changed = true;
                            isSelected = true;
                            style = selectedNodeStyle;
                        }
                        else
                        {
                            GUI.changed = true;
                            isSelected = false;
                            style = defaultNodeStyle;
                        }
                    }

                    if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
                    }
                    break;

                case EventType.MouseUp:
                    isDragged = false;
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }

            return false;
        }

        protected void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
            genericMenu.ShowAsContext();
        }

        protected void OnClickRemoveNode()
        {
            if (OnRemoveNode != null)
            {
                OnRemoveNode(this);
            }
        }
    }
}