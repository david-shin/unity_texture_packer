using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TP
{
    public class NodeInput : Node
    {
        public Texture2D texture;
        private Texture2D textureToProcess;

        public NodeInput(Vector2 position, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode)
        {
            SetStyles();

            rect = new Rect(position.x, position.y, 115, 125);

            inPoint = new ConnectionPoint[0];
            outPoint = new ConnectionPoint[4];

            for (int i = 0; i < inPoint.Length; i++)
                inPoint[i] = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint,
                                                 new Rect(0, i * 25, 20f, 20f));

            for (int i = 0; i < outPoint.Length; i++)
                outPoint[i] = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint,
                                                  new Rect(0, i * 25, 20f, 20f));

            name = "Texture";
            outPoint[0].name = "R";
            outPoint[1].name = "G";
            outPoint[2].name = "B";
            outPoint[3].name = "A";

            style = defaultNodeStyle;
            OnRemoveNode = OnClickRemoveNode;
        }

        protected override void DrawContents()
        {
            var textureInput = (Texture2D)EditorGUI.ObjectField(new Rect(rect.x + 15, rect.y + 25, 85, 85), texture, typeof(Texture2D), false);

            if (texture != textureInput)
            {
                texture = textureInput;
                textureToProcess = Utils.ReadTexture(textureInput);
                Process();
            }
        }

        public override void Process()
        {
            if (textureToProcess.width != Utils.TextureWidth || textureToProcess.height != Utils.TextureHeight)
            {
                textureToProcess.Resize(Utils.TextureWidth, Utils.TextureHeight);
            }

            outPoint[0].data = Utils.GetChannelData(textureToProcess, TextureChannel.r);
            outPoint[1].data = Utils.GetChannelData(textureToProcess, TextureChannel.g);
            outPoint[2].data = Utils.GetChannelData(textureToProcess, TextureChannel.b);
            outPoint[3].data = Utils.GetChannelData(textureToProcess, TextureChannel.a);
        }

        protected override void SetStyles()
        {
            defaultNodeStyle = new GUIStyle();
            defaultNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node0.png") as Texture2D;
            defaultNodeStyle.normal.textColor = new Color(1f, 1f, 1f);
            defaultNodeStyle.border = new RectOffset(12, 12, 12, 12);

            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node0 on.png") as Texture2D;
            selectedNodeStyle.normal.textColor = new Color(1f, 1f, 1f);
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
            outPointStyle.normal.textColor = new Color(1f, 1f, 1f);
            outPointStyle.alignment = TextAnchor.MiddleCenter;
            outPointStyle.border = new RectOffset(4, 4, 12, 12);
        }

    }
}