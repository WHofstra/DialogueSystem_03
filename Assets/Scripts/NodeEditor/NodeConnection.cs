using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Xml.Serialization;

public class NodeConnection
{
    [XmlIgnore] public Action<NodeConnection> RemoveConnection;

    private NodeConnectionPoint inPoint;
    private NodeConnectionPoint outPoint;

    [XmlIgnore] public NodeConnectionPoint InPoint { get { return inPoint; } set { value = inPoint; } }
    [XmlIgnore] public NodeConnectionPoint OutPoint { get { return outPoint; } set { value = outPoint; } }

    public NodeConnection()
    {

    }

    public NodeConnection(NodeConnectionPoint inPoint, NodeConnectionPoint outPoint,
                          Action<NodeConnection> RemoveConnection)
    {
        this.inPoint = inPoint;
        this.outPoint = outPoint;
        this.RemoveConnection = RemoveConnection;
    }

    public void DrawConnection()
    {
        Handles.DrawBezier(inPoint.Rectangle.center, outPoint.Rectangle.center, inPoint.Rectangle.center + Vector2.left * 50f,
                           outPoint.Rectangle.center - Vector2.left * 50f, Color.white, null, 2f);

        if (Handles.Button((inPoint.Rectangle.center + outPoint.Rectangle.center) * 0.5f, Quaternion.identity, 4, 8,
                            Handles.RectangleHandleCap))
        {
            //Remove if the Point is Already Connected
            if (RemoveConnection != null) {
                RemoveConnection(this);
            }
        }
    }
}
