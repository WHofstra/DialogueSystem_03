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
    private GUIStyle selectedStyle;
    private GUIStyle inPointStyle;
    private GUIStyle outPointStyle;

    private Rect menuBar;
    private Vector2 drag;
    private Vector2 offset;

    private string[] buttons = { "Save Dialogue", "Load Dialogue" };

    [MenuItem("Window/Custom Dialogue")]
    private static void OpenWindow()
    {
        //Generates a 'Custom Dialogue'-Window
        DialogueEditor window = GetWindow<DialogueEditor>();
        window.titleContent = new GUIContent("Custom Dialogue");
    }

    private void OnEnable()
    {
        //Generates a New Style for Node
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node3.png") as Texture2D;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        //Generates a Skin for a Selected Node
        selectedStyle = new GUIStyle();
        selectedStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node3 on.png") as Texture2D;
        selectedStyle.border = new RectOffset(12, 12, 12, 12);

        //Generate Input Display
        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);

        //Generate Output Display
        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);
    }

    private void OnGUI()
    {
        //Background (Grid)
        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);
        DrawMenuBar();

        //Draw
        DrawNodes();
        DrawConnections();
        DrawConnectionLine(Event.current);

        //Check for Input
        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.changed) {
            Repaint();
        }
    }

    private void DrawConnectionLine(Event anEvent)
    {
        if (selectedInPoint != null && selectedOutPoint == null)
        {
            Handles.DrawBezier(selectedInPoint.Rectangle.center, anEvent.mousePosition,
                selectedInPoint.Rectangle.center + Vector2.left * 50f, anEvent.mousePosition - Vector2.left * 50f,
                Color.white, null, 2f);

            GUI.changed = true;
        }

        if (selectedInPoint == null && selectedOutPoint != null)
        {
            Handles.DrawBezier(selectedOutPoint.Rectangle.center, anEvent.mousePosition,
                selectedOutPoint.Rectangle.center - Vector2.left * 50f, anEvent.mousePosition + Vector2.left * 50f,
                Color.white, null, 2f);

            GUI.changed = true;
        }
    }

    private void DrawGrid(float space, float opacity, Color gridColor)
    {
        int width = Mathf.CeilToInt(position.width / space);
        int height = Mathf.CeilToInt(position.height / space);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, opacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % space, offset.y % space, 0);

        //Draw Vertical Lines
        for (int i = 0; i < width; i++) {
            Handles.DrawLine(new Vector3(space * i, -space, 0) + newOffset,
                             new Vector3(space * i, position.height, 0f) + newOffset);
        }

        //Draw Horizontal Lines
        for (int j = 0; j < height; j++) {
            Handles.DrawLine(new Vector3(-space, space * j, 0) + newOffset,
                             new Vector3(position.width, space * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
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

            case EventType.MouseDrag:
                if (anEvent.button == 0) {
                    OnDrag(anEvent.delta);
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
        Node aNode = new Node(mousePosition, 200, 50, nodeStyle, selectedStyle, inPointStyle, outPointStyle,
                              OnClickInPoint, OnClickOutPoint, RemoveNode);
        nodes.Add(aNode);
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

    private void OnDrag(Vector2 delta)
    {
        drag = delta;
        if (nodes != null)
        {
            //Move Nodes
            for (int i = 0; i < nodes.Count; i++) {
                nodes[i].Drag(delta);
            }
        }
        GUI.changed = true;
    }

    private void RemoveConnection(NodeConnection connection)
    {
        connections.Remove(connection);
    }

    private void CreateConnection()
    {
        connections.Add(new NodeConnection(selectedInPoint, selectedOutPoint, RemoveConnection));
    }

    private void RemoveNode(Node node)
    {
        //Before the Node Will be Removed, the Connections Need to be Removed First
        if (connections != null)
        {
            //Initialize List of Node Connections to Remove
            List<NodeConnection> connectionsToRemove = new List<NodeConnection>();

            //Put Node Connections in Said List
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].InPoint == node.InPoint || connections[i].OutPoint == node.OutPoint) {
                    connectionsToRemove.Add(connections[i]);
                }
            }

            //Delete Those Node Connections
            for (int i = 0; i < connectionsToRemove.Count; i++) {
                connections.Remove(connectionsToRemove[i]);
            }

            //Clear List of Node Connections to Remove
            connectionsToRemove = null;
        }

        //Remove Node
        nodes.Remove(node);
    }

    private void ClearConnectionSelection()
    {
        //Clear
        selectedInPoint = null;
        selectedOutPoint = null;
    }

    private void DrawMenuBar()
    {
        menuBar = new Rect(0, 0, position.width, Constants.Menu.BAR_HEIGHT);

        GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button(new GUIContent(buttons[0]), EditorStyles.toolbarButton,
            GUILayout.Width(buttons[0].Length * Constants.Menu.LETTER_LENGTH)))
        {
            SaveDialogue();
        }
        GUILayout.Button(new GUIContent(buttons[1]), EditorStyles.toolbarButton,
            GUILayout.Width(buttons[1].Length * Constants.Menu.LETTER_LENGTH));

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void SaveDialogue()
    {
        XMLConverter.Serialize(nodes, "Assets/Dialogue", "NewFile.xml");
        XMLConverter.Serialize(connections, "Assets/Dialogue", "NewConnections.xml");
    }

    private void LoadDialogue()
    {
        List<Node> deserializedNodes                 = XMLConverter.Deserialize<List<Node>>("Assets/Dialogue", "NewFile.xml");
        List<NodeConnection> deserializedConnections = XMLConverter.Deserialize<List<NodeConnection>>("Assets/Dialogue", "NewConnections.xml");

        nodes       = new List<Node>();
        connections = new List<NodeConnection>();
    }
}
