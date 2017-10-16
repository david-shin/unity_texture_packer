using System;
using UnityEngine;

public enum ConnectionPointType { In, Out }

public class ConnectionPoint
{
    public string name;
    public Rect rect;
    public ConnectionPointType type;
    public Node node;
    public GUIStyle style;
    public Action<ConnectionPoint> OnClickConnectionPoint;

    private Rect rectOrigin;

    public ConnectionPoint(Node node, ConnectionPointType type, GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint, Rect inputRect)
    {
        this.node = node;
        this.type = type;
        this.style = style;
        this.OnClickConnectionPoint = OnClickConnectionPoint;
        rectOrigin = inputRect;
        rect = rectOrigin;
    }

    public void Draw()
    {
        rect.y = node.rect.y + rectOrigin.y + 10f;

        switch (type)
        {
            case ConnectionPointType.In:
                rect.x = node.rect.x - rect.width + 8;
                break;

            case ConnectionPointType.Out:
                rect.x = node.rect.x + node.rect.width - 8;
                break;
        }

        if (GUI.Button(rect, name, style))
        {
            if (OnClickConnectionPoint != null)
            {
                OnClickConnectionPoint(this);
            }
        }
    }
}