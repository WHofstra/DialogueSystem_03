using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeConnectionPoint
{
    public Action<NodeConnectionPoint> ConnectionPoint;

    private Constants.ConnectionPoint.Type type;
    private Node node;
    private GUIStyle style;
    private Rect rect;

    public Constants.ConnectionPoint.Type Type { get { return type; } set { value = type; } }
    public Node Node { get { return node; } set { value = node; } }
    public GUIStyle Style { get { return style; } set { value = style; } }
    public Rect Rectangle { get { return rect; } set { value = rect; } }

    public NodeConnectionPoint(Node node, Constants.ConnectionPoint.Type type, GUIStyle style,
                               Action<NodeConnectionPoint> ConnectionPoint)
    {
        //Assign Node to NodeConnectionPoint
        this.node = node;

        //Input or Output
        this.type = type;

        this.style = style;
        this.ConnectionPoint = ConnectionPoint;
        rect = new Rect(0, 0, 10f, 20f);
    }

    public void Draw()
    {
        rect.y = node.Rectangle.y + (node.Rectangle.height * 0.5f) - rect.height * 0.5f;

        switch (type)
        {
            case Constants.ConnectionPoint.Type.In:
                rect.x = node.Rectangle.x - rect.width + 8f;
                break;

            case Constants.ConnectionPoint.Type.Out:
                rect.x = node.Rectangle.x + node.Rectangle.width - 8f;
                break;
        }

        if (GUI.Button(rect, "", style))
        {
            if (ConnectionPoint != null) {
                ConnectionPoint(this);
            }
        }
    }
}
