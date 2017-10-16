using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeInput : Node
{
    public NodeInput(Vector2 position, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode)
    {
        defaultNodeStyle = new GUIStyle();
        defaultNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        defaultNodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);

        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.normal.textColor = new Color(1f, 1f, 1f);
        outPointStyle.alignment = TextAnchor.MiddleCenter;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);

        rect = new Rect(position.x, position.y, 200, 115);

        inPoint = new ConnectionPoint[0];
        outPoint = new ConnectionPoint[4];

        for (int i = 0; i < inPoint.Length; i++)
        {
            inPoint[i] = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint, new Rect(0, i * 25, 20f, 20f));
        }

        for (int i = 0; i < outPoint.Length; i++)
        {
            outPoint[i] = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint, new Rect(0, i * 25, 20f, 20f));
        }

        outPoint[0].name = "R";
        outPoint[1].name = "G";
        outPoint[2].name = "B";
        outPoint[3].name = "A";

        style = defaultNodeStyle;
        OnRemoveNode = OnClickRemoveNode;
    }

    public override void Draw()
    {
        for (int i = 0; i < inPoint.Length; i++)
            inPoint[i].Draw();

        for (int i = 0; i < outPoint.Length; i++)
            outPoint[i].Draw();

        GUI.Box(rect, title, style);
    }
}
