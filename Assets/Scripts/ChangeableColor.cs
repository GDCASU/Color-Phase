using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public enum SwapColor
{
    Red,
    Green,
    Blue,
    Yellow
}

public class ChangeableColor : MonoBehaviour
{
    public static Color[] defaultColors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow };

    protected static string[] colorNames = Enum.GetNames(typeof(SwapColor));

    private List<RendererMaterialColor> materialCache;

    [SerializeField]
    private SwapColor currentColor; //other scripts should call ChangeColor only
    [SerializeField]
    private List<RendererMaterialColor> materials = new List<RendererMaterialColor>();

    public void Awake()
    {
        materialCache = new List<RendererMaterialColor>();
    }

    public void ChangeColor(SwapColor newColor)
    {
        currentColor = newColor;

        foreach (RendererMaterialColor rmc in materials)
        {
            Renderer r = rmc.renderer;
            if (r == null) continue;
            if (r.sharedMaterials.Length < rmc.index + 1)
            {
                Debug.LogWarning("ChangeableColor: Selected material index is out of bounds, ignoring.");
                continue;
            }
            if (r.sharedMaterials[rmc.index].shader.name != "Custom/ColorSwappable")
            {
                Debug.LogWarning("ChangeableColor: Selected material renderer is " + r.sharedMaterials[rmc.index].shader.name + " instead of ColorSwappable, ignoring.");
                continue;
            }

            rmc.SetColor(newColor);
        }
    }

    protected void OnValidate()
    {
        /*if (materials.Count != materialCache.Count)
        {
            //check for added elements
            for (int i = 0; i < materials.Count; i++)
                if (!materialCache.Contains(materials[i]))
                {
                    materials[i].oldColor = materials[i].GetColor();
                    materialCache.Add(RendererMaterialColor.From(materials[i])); //save original color
                }
            //check for removed elements
            bool[] remove = new bool[materialCache.Count];
            for (int i = 0; i < materialCache.Count; i++)
                if (remove[i] = !materials.Contains(materialCache[i]))
                    materialCache[i].SetColor(materialCache[i].oldColor);

            for (int i = remove.Length - 1; i >= 0; i--)
                if (remove[i]) materialCache.RemoveAt(i);
        }
        */
        //
        ChangeColor(currentColor);
    }

    [Serializable]
    protected class RendererMaterialColor
    {
        //for some reason, default values for these variables are not set when instantiated through ReorderableList
        //so don't bother setting any
        public Renderer renderer;
        public int index;
        public bool emissive;
        public Color[] color;
        public Color oldColor;

        public Color GetColor()
        {
            MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propBlock, index);
            string name = emissive ? "_EmissionColor" : "_Color";
            return propBlock.GetColor(name);
        }

        public void SetColor(SwapColor newColor)
        {
            MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propBlock, index);
            string name = emissive ? "_EmissionColor" : "_Color";
            propBlock.SetColor(name, color[(int)newColor]);
            renderer.SetPropertyBlock(propBlock, index);
        }

        public void SetColor(Color newColor)
        {
            MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propBlock, index);
            string name = emissive ? "_EmissionColor" : "_Color";
            propBlock.SetColor(name, newColor);
            renderer.SetPropertyBlock(propBlock, index);
        }

        public static RendererMaterialColor From(RendererMaterialColor rmc)
        {
            return new RendererMaterialColor()
            {
                renderer = rmc.renderer,
                index = rmc.index,
                emissive = rmc.emissive,
                color = (Color[])rmc.color.Clone(),
                oldColor = rmc.oldColor
            };
        }
    }
}

[CustomEditor(typeof(ChangeableColor))]
public class ChangeableColorEditor : Editor
{
    private ReorderableList list;
    private static string[] colorNames = Enum.GetNames(typeof(SwapColor));

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("materials"), true, true, true, true)
        {
            drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Affected Materials");
            },
            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;

                float x = rect.x + rect.width;
                float y = rect.y;
                float height = EditorGUIUtility.singleLineHeight;
                x -= 35;
                SerializedProperty emissive = element.FindPropertyRelative("emissive");
                emissive.boolValue = EditorGUI.Toggle(new Rect(x, y, 30, height), emissive.boolValue);
                x -= 65;
                EditorGUI.LabelField(new Rect(x, y, 60, height), "Emissive?");
                x -= 65;
                EditorGUI.PropertyField(new Rect(x, y, 60, height), element.FindPropertyRelative("index"), GUIContent.none);
                x -= 20;
                EditorGUI.LabelField(new Rect(x, y, 15, height), "id");
                float remain = rect.width - (rect.x + rect.width - x);
                x -= remain;
                EditorGUI.PropertyField(new Rect(x, y, remain - 5, height), element.FindPropertyRelative("renderer"), GUIContent.none);

                x = rect.x + rect.width;
                y = rect.y + height + 2;

                SerializedProperty colors = element.FindPropertyRelative("color");
                if (colors.arraySize != colorNames.Length)
                {
                    colors.ClearArray();
                    for (int i = 0; i < colorNames.Length; i++)
                    {
                        colors.InsertArrayElementAtIndex(i);
                        colors.GetArrayElementAtIndex(i).colorValue = ChangeableColor.defaultColors[i];
                    }
                }
                GUIStyle rightAlign = new GUIStyle
                {
                    alignment = TextAnchor.MiddleRight
                };
                for (int i = colors.arraySize - 1; i >= 0; i--)
                {
                    x -= 40;
                    SerializedProperty col = colors.GetArrayElementAtIndex(i);
                    col.colorValue = EditorGUI.ColorField(new Rect(x, y, 40, height), GUIContent.none, col.colorValue, true, true, emissive.boolValue);
                    x -= 20;
                    EditorGUI.LabelField(new Rect(x, y, 15, height), colorNames[i].Substring(0, 1), rightAlign);
                }
            },
            elementHeight = 37
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SerializedProperty color = serializedObject.FindProperty("currentColor");
        SwapColor curVal = (SwapColor)color.intValue;
        color.intValue = (int)(SwapColor)EditorGUILayout.EnumPopup("Current Color", curVal);

        EditorGUILayout.Space();

        list.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
}