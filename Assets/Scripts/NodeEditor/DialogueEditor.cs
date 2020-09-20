using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogueEditor : EditorWindow
{
    private List<Node> nodes                 = new List<Node>();
    private List<NodeConnection> connections = new List<NodeConnection>();

    private NodeConnectionPoint selectedInPoint;
    private NodeConnectionPoint selectedOutPoint;

    private GUIStyle nodeStyle;
    private GUIStyle inPointStyle;
    private GUIStyle outPointStyle;

    [MenuItem("Window/Custom Dialogue")]
    private static void OpenWindow()
    {
        //Generates a 'Custom Dialogue'-Window
        DialogueEditor window = GetWindow<DialogueEditor>();
        window.titleContent = new GUIContent("Custom Dialogue");
    }

    private void OnEnable()
    {
        //Generates a New Node
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node3.png") as Texture2D;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        //Generate Input
        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);

        //Generate Output
        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);
    }

    private void OnGUI()
    {
        //Draw
        DrawNodes();
        DrawConnections();

        //Check for Input
        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.changed) {
            Repaint();
        }
    }

    private void DrawNodes()
    {
        if (nodes != null) {
            for (int i = 0; i < nodes.Count; i++) {
                nodes[i].Draw();
            }
        }
    }

    private void DrawConnections()
    {
        if (connections != null)
        {
            for (int i = 0; i < connections.Count; i++) {
                connections[i].DrawConnection();
            }
        }
    }

    private void ProcessNodeEvents(Event anEvent)
    {
        if (nodes != null) {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                bool guiChanged = nodes[i].ProcessEvents(anEvent);

                if (guiChanged) {
                    GUI.changed = true;
                }
            }
        }
    }

    private void ProcessEvents(Event anEvent)
    {
        switch (anEvent.type)
        {
            //When the Right Mouse-button is Down, Activate Context Menu
            case EventType.MouseDown:
                if (anEvent.button == 1) {
                    ProcessContextMenu(anEvent.mousePosition);
                }
                break;
        }
    }

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        //Show Menu with Options
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add Node"), false, () => AddNode(mousePosition));
        genericMenu.ShowAsContext();
    }

    private void AddNode(Vector2 mousePosition)
    {
        nodes.Add(new Node(mousePosition, 200, 50, nodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint));
    }

    private void OnClickInPoint(NodeConnectionPoint inPoint)
    {
        selectedInPoint = inPoint;

        if (selectedOutPoint != null)
        {
            if (selectedOutPoint.Node != selectedInPoint.Node) {
                CreateConnection();
                ClearConnectionSelection();
            }
            else {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickOutPoint(NodeConnectionPoint outPoint)
    {
        selectedOutPoint = outPoint;

        if (selectedInPoint != null)
        {
            if (selectedOutPoint.Node != selectedInPoint.Node) {
                CreateConnection();
                ClearConnectionSelection();
            }
            else {
                ClearConnectionSelection();
            }
        }
    }

    private void RemoveConnection(NodeConnection connection)
    {
        connections.Remove(connection);
    }

    private void CreateConnection()
    {
        connections.Add(new NodeConnection(selectedInPoint, selectedOutPoint, RemoveConnection));
    }

    private void ClearConnectionSelection()
    {
        //Clear
        selectedInPoint = null;
        selectedOutPoint = null;
    }
}
