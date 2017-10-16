using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class NodeStyle
{
    public GUIStyle inputStyle;
    public GUIStyle outputStyle;
    public GUIStyle selectedNodeStyle;
    public GUIStyle inPointStyle;
    public GUIStyle outPointStyle;

    private NodeBasedEditor m_editor;

    public NodeStyle(NodeBasedEditor editor)
    {
        m_editor = editor;

        inputStyle = new GUIStyle();
        inputStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        inputStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);

        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);


    }
}
