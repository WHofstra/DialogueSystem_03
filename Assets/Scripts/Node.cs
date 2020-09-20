using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Rect rect;
    private GUIStyle style;

    private string title;
    private bool isDragging;

    public Rect Rectangle { get { return rect; } set { value = rect; } }
    public GUIStyle Style { get { return style; } set { value = style; } }
    public string Title { get { return title; } set { value = title; } }
    public bool IsDragging { get { return isDragging; } set { value = isDragging; } }

    public Node(Vector2 position, float width, float height, GUIStyle nodeStyle)
    {
        rect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;
    }

    public void Drag(Vector2 delta)
    {
        rect.position += delta;
    }

    public void Draw()
    {
        GUI.Box(rect, title, style);
    }

    public bool ProcessEvents(Event anEvent)
    {
        return false;
    }
}
