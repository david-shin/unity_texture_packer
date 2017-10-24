using UnityEngine;
using UnityEditor;
using UnityEditor.Graphs;
using System;
using System.Collections.Generic;

namespace TP
{
    [Serializable]
    public class Editor : EditorWindow
    {
        [SerializeField]
        public List<Node> nodes;
        [SerializeField]
        public List<Connection> connections;

        public Vector2 offset;
        public Vector2 drag;

        private EventHandlers m_eEventHandlers;

        bool groupEnabled;
        bool myBool = true;
        float myFloat = 1.23f;

        [MenuItem("Window/Node Based Editor")]
        private static void OpenWindow()
        {
            Editor window = GetWindow<Editor>();
            window.titleContent = new GUIContent("Node based Texture Packer");
        }

        private void OnEnable()
        {
            m_eEventHandlers = new EventHandlers(this);
        }

        private void OnGUI()
        {
            DrawGrid(20, 0.2f, Color.gray);
            DrawGrid(100, 0.4f, Color.gray);

            DrawNodes();
            DrawConnections();

            DrawConnectionLine(Event.current);

            DrawSidebar();

            m_eEventHandlers.ProcessNodeEvents(Event.current);
            m_eEventHandlers.ProcessEvents(Event.current);

            if (GUI.changed) Repaint();
        }

        private void DrawSidebar()
        {
            GUI.color = Color.gray;
            GUI.Box(new Rect(5, position.height - 55, 350, 50), "");
            GUI.color = Color.white;
            GUI.Label(new Rect(10, position.height - 50, 50, 20), "Width");
            GUI.TextArea(new Rect(10, position.height - 30, 50, 20), "2048");
            GUI.Label(new Rect(65, position.height - 30, 10, 20), "x");
            GUI.Label(new Rect(80, position.height - 50, 50, 20), "Height");
            GUI.TextArea(new Rect(80, position.height - 30, 50, 20), "2048");

            GUI.Button(new Rect(180, position.height - 30, 80, 20), "Load");
            GUI.Button(new Rect(270, position.height - 30, 80, 20), "Save");
        }

        private void DrawNodes()
        {
            if (nodes != null)
                for (int i = 0; i < nodes.Count; i++)
                    nodes[i].Draw();
        }

        private void DrawConnections()
        {
            if (connections != null)
                for (int i = 0; i < connections.Count; i++)
                    connections[i].Draw();
        }

        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width - 200 / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }

        private void DrawConnectionLine(Event e)
        {
            if (m_eEventHandlers.selectedInPoint != null && m_eEventHandlers.selectedOutPoint == null)
            {
                Handles.DrawBezier(
                    m_eEventHandlers.selectedInPoint.rect.center,
                    e.mousePosition,
                    m_eEventHandlers.selectedInPoint.rect.center + Vector2.left * 50f,
                    e.mousePosition - Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }

            if (m_eEventHandlers.selectedOutPoint != null && m_eEventHandlers.selectedInPoint == null)
            {
                Handles.DrawBezier(
                    m_eEventHandlers.selectedOutPoint.rect.center,
                    e.mousePosition,
                    m_eEventHandlers.selectedOutPoint.rect.center - Vector2.left * 50f,
                    e.mousePosition + Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }
        }

    }
}