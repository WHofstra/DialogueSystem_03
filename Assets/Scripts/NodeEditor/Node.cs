using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Node
{
    public Action Dragging;
    public Action<Node> RemoveNode;

    private NodeConnectionPoint inPoint;
    private NodeConnectionPoint outPoint;
    private Rect rect;

    private GUIStyle style;
    private GUIStyle defaultStyle;
    private GUIStyle selectedStyle;

    private string title;
    private bool isDragging;
    private bool isSelected;

    public NodeConnectionPoint InPoint { get { return inPoint; } set { value = inPoint; } }
    public NodeConnectionPoint OutPoint { get { return outPoint; } set { value = outPoint; } }
    public Rect Rectangle { get { return rect; } set { value = rect; } }
    public GUIStyle Style { get { return style; } set { value = style; } }
    public GUIStyle DefaultStyle { get { return defaultStyle; } set { value = defaultStyle; } }
    public GUIStyle SelectedStyle { get { return selectedStyle; } set { value = selectedStyle; } }
    public string Title { get { return title; } set { value = title; } }
    public bool IsDragging { get { return isDragging; } set { value = isDragging; } }
    public bool IsSelected { get { return isSelected; } set { value = isSelected; } }

    public Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle,
                GUIStyle outPointStyle, Action<NodeConnectionPoint> OnClickInPoint, Action<NodeConnectionPoint> OnClickOutPoint,
                Action<Node> OnClickRemoveNode)
    {
        rect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;

        inPoint = new NodeConnectionPoint(this, Constants.ConnectionPoint.Type.In, inPointStyle, OnClickInPoint);
        outPoint = new NodeConnectionPoint(this, Constants.ConnectionPoint.Type.Out, outPointStyle, OnClickOutPoint);

        defaultStyle = nodeStyle;
        this.selectedStyle = selectedStyle;
        RemoveNode = OnClickRemoveNode;
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
                    if (rect.Contains(anEvent.mousePosition))
                    {
                        //Drag When Clicking
                        isDragging = true;
                        Dragging();

                        GUI.changed = true;
                        isSelected = true;
                        style = selectedStyle;
                    }
                    else {
                        GUI.changed = true;
                        isSelected = false;
                        style = defaultStyle;
                    }
                }
                else if (anEvent.button == 1 && isSelected && rect.Contains(anEvent.mousePosition)) {
                    ProcessContextMenu();
                    anEvent.Use();
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

    private void ProcessContextMenu()
    {
        //Show Menu
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    private void OnClickRemoveNode()
    {
        if (RemoveNode != null) {
            RemoveNode(this);
        }
    }
}
