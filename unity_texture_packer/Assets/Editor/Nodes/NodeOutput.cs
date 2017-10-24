using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TP
{
    public class NodeOutput : Node
    {
        public NodeOutput(Vector2 position, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode)
        {
            SetStyles();

            rect = new Rect(position.x, position.y, 115, 125);

            inPoint = new ConnectionPoint[4];
            outPoint = new ConnectionPoint[0];

            for (int i = 0; i < inPoint.Length; i++)
                inPoint[i] = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint,
                                                 new Rect(0, i * 25, 20f, 20f));

            for (int i = 0; i < outPoint.Length; i++)
                outPoint[i] = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint,
                                                  new Rect(0, i * 25, 20f, 20f));

            name = "Save";
            inPoint[0].name = "R";
            inPoint[1].name = "G";
            inPoint[2].name = "B";
            inPoint[3].name = "A";

            style = defaultNodeStyle;
            OnRemoveNode = OnClickRemoveNode;
        }

        public override void Process()
        {
            Texture2D tex = new Texture2D(200, 200);
            Color[] colors = new Color[tex.GetPixels().Length];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i].r = inPoint[0].data[i];
                colors[i].g = inPoint[1].data[i];
                colors[i].b = inPoint[2].data[i];
                colors[i].a = inPoint[3].data[i];
            }

            tex.SetPixels(colors);
        }

        protected override void DrawContents()
        {

        }

        protected override void SetStyles()
        {
            defaultNodeStyle = new GUIStyle();
            defaultNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node0.png") as Texture2D;
            defaultNodeStyle.border = new RectOffset(12, 12, 12, 12);

            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node0 on.png") as Texture2D;
            selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

            inPointStyle = new GUIStyle();
            inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
            inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
            inPointStyle.normal.textColor = new Color(1f, 1f, 1f);
            inPointStyle.alignment = TextAnchor.MiddleCenter;
            inPointStyle.border = new RectOffset(4, 4, 12, 12);

            outPointStyle = new GUIStyle();
            outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
            outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
            outPointStyle.border = new RectOffset(4, 4, 12, 12);
        }
    }

}