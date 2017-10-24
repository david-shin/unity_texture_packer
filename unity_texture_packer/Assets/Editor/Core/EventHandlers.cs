using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TP
{
    public class EventHandlers
    {
        public ConnectionPoint selectedInPoint;
        public ConnectionPoint selectedOutPoint;
        private Editor m_editor;

        public EventHandlers(Editor editor)
        {
            m_editor = editor;
        }

        public void ProcessEvents(Event e)
        {
            m_editor.drag = Vector2.zero;

            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                        ClearConnectionSelection();

                    if (e.button == 1)
                        ProcessContextMenu(e.mousePosition);
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 || e.button == 2)
                        OnDrag(e.delta);
                    break;
            }
        }

        public void ProcessNodeEvents(Event e)
        {
            if (m_editor.nodes != null)
                for (int i = m_editor.nodes.Count - 1; i >= 0; i--)
                {
                    bool guiChanged = m_editor.nodes[i].ProcessEvents(e);
                    if (guiChanged)
                        GUI.changed = true;
                }
        }
        public void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Textures/Texture"), false, () => OnClickAddNodeInput(mousePosition));
            genericMenu.AddItem(new GUIContent("Textures/Save"), false, () => OnClickAddNodeOutput(mousePosition));
            genericMenu.AddItem(new GUIContent("Modifiers/Additive"), false, () => OnClickAddNodeOutput(mousePosition));
            genericMenu.AddItem(new GUIContent("Modifiers/Multiply"), false, () => OnClickAddNodeOutput(mousePosition));
            genericMenu.AddItem(new GUIContent("Modifiers/Blend"), false, () => OnClickAddNodeOutput(mousePosition));
            genericMenu.ShowAsContext();
        }

        private void OnDrag(Vector2 delta)
        {
            m_editor.drag = delta;

            if (m_editor.nodes != null)
                for (int i = 0; i < m_editor.nodes.Count; i++)
                    m_editor.nodes[i].Drag(delta);

            GUI.changed = true;
        }

        private void OnClickAddNodeInput(Vector2 mousePosition)
        {
            if (m_editor.nodes == null)
                m_editor.nodes = new List<Node>();

            m_editor.nodes.Add(new NodeInput(mousePosition,
                                    OnClickInPoint,
                                    OnClickOutPoint,
                                    OnClickRemoveNode));
        }

        private void OnClickAddNodeOutput(Vector2 mousePosition)
        {
            if (m_editor.nodes == null)
                m_editor.nodes = new List<Node>();

            m_editor.nodes.Add(new NodeOutput(mousePosition,
                                    OnClickInPoint,
                                    OnClickOutPoint,
                                    OnClickRemoveNode));
        }

        private void OnClickInPoint(ConnectionPoint inPoint)
        {
            selectedInPoint = inPoint;

            if (selectedOutPoint != null)
            {
                if (selectedOutPoint.node != selectedInPoint.node)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        private void OnClickOutPoint(ConnectionPoint outPoint)
        {
            selectedOutPoint = outPoint;

            if (selectedInPoint != null)
            {
                if (selectedOutPoint.node != selectedInPoint.node)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        private void OnClickRemoveNode(Node node)
        {
            if (m_editor.connections != null)
            {
                List<Connection> connectionsToRemove = new List<Connection>();

                for (int i = 0; i < m_editor.connections.Count; i++)
                    if (m_editor.connections[i].inPoint == node.inPoint[0] || m_editor.connections[i].outPoint == node.outPoint[0])
                        connectionsToRemove.Add(m_editor.connections[i]);

                for (int i = 0; i < connectionsToRemove.Count; i++)
                    m_editor.connections.Remove(connectionsToRemove[i]);

                connectionsToRemove = null;
            }

            m_editor.nodes.Remove(node);
        }

        private void OnClickRemoveConnection(Connection connection)
        {
            m_editor.connections.Remove(connection);
        }

        private void CreateConnection()
        {
            if (m_editor.connections == null)
                m_editor.connections = new List<Connection>();

            m_editor.connections.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
        }

        private void ClearConnectionSelection()
        {
            selectedInPoint = null;
            selectedOutPoint = null;
        }
    }
}