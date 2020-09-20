using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node
{
    private NodeConnectionPoint inPoint;
    private NodeConnectionPoint outPoint;
    private Rect rect;
    private GUIStyle style;

    private string title;
    private bool isDragging;

    public NodeConnectionPoint InPoint { get { return inPoint; } set { value = inPoint; } }
    public NodeConnectionPoint OutPoint { get { return outPoint; } set { value = outPoint; } }
    public Rect Rectangle { get { return rect; } set { value = rect; } }
    public GUIStyle Style { get { return style; } set { value = style; } }
    public string Title { get { return title; } set { value = title; } }
    public bool IsDragging { get { return isDragging; } set { value = isDragging; } }

    public Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle inPointStyle,
                GUIStyle outPointStyle, Action<NodeConnectionPoint> OnClickInPoint, Action<NodeConnectionPoint> OnClickOutPoint)
    {
        rect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;

        inPoint = new NodeConnectionPoint(this, Constants.ConnectionPoint.Type.In, inPointStyle, OnClickInPoint);
        outPoint = new NodeConnectionPoint(this, Constants.ConnectionPoint.Type.Out, outPointStyle, OnClickOutPoint);
    }

    public void Drag(Vector2 delta)
    {
        rect.position += delta;
    }

    public void Draw()
    {
        GUI.Box(rect, title, style);
        inPoint.Draw();
        outPoint.Draw();
    }

    public bool ProcessEvents(Event anEvent)
    {
        switch (anEvent.type)
        {
            case EventType.MouseDown:
                if (anEvent.button == 0)
                {
                    if (rect.Contains(anEvent.mousePosition)) {
                        //Drag When Clicking
                        isDragging = true;
                        GUI.changed = true;
                    }
                    else {
                        GUI.changed = true;
                    }
                }
                break;

            case EventType.MouseUp:
                //Stop Dragging When Not Clicking
                isDragging = false;
                break;

            case EventType.MouseDrag:
                if (anEvent.button == 0 && isDragging) {
                    //Use the Mouse Position to Determine the Node Position
                    Drag(anEvent.delta);
                    anEvent.Use();
                    return true;
                }
                break;
        }

        return false;
    }
}
