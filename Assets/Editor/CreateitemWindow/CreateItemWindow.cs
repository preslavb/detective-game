using System;
using System.Linq;
using DesignData;
using Doozy.Editor.Internal;
using Doozy.Editor.Nody.Windows;
using Doozy.Engine.Extensions;
using Doozy.Engine.Nody.Models;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Doozy.Editor.Nody.Windows
{
    public partial class NodyWindow : BaseEditorWindow
    {
        public partial class CreateItemWindow : EditorWindow
        {
            private DesignDataScriptableObject _designData;
            
            
            [SerializeField] private NodyWindow _nodyWindowEmbedded;
            [SerializeField] private IMGUIContainer _nodyWindowContainer;

            [MenuItem("Tools/Detective Game/Create Items", priority = 10000)]
            public static void ShowExample()
            {
                CreateItemWindow wnd = GetWindow<CreateItemWindow>(typeof(SceneView));
                wnd.titleContent = new GUIContent("Create Item");
            }

            public void OnEnable()
            {
                // Create the root visual element
                VisualElement root = rootVisualElement;

                _designData = null;

                Rerender();
            }

            private void Update()
            {
                if (_designData?.Test != null)
                {
                    _nodyWindowEmbedded.CurrentMousePosition = _nodyWindowEmbedded.CurrentMousePosition - (Vector2.up * 50);
                    _nodyWindowEmbedded.Update(); 
                    _nodyWindowContainer.MarkDirtyRepaint(); 
                    _nodyWindowEmbedded.position = _nodyWindowEmbedded.position.WithWidth(position.width);
                }
            }
            
            private void OnDisable() { if (_nodyWindowEmbedded != null) _nodyWindowEmbedded.OnDisable(); }
            private void OnInspectorUpdate() { if (_nodyWindowEmbedded != null) _nodyWindowEmbedded.OnInspectorUpdate(); }
            private void OnFocus() { if (_nodyWindowEmbedded != null) _nodyWindowEmbedded.OnFocus(); }
            private void OnLostFocus() { if (_nodyWindowEmbedded != null) _nodyWindowEmbedded.OnLostFocus(); }

            private void OnSelectionChange()
            {
                DesignDataScriptableObject selectedDesignData = null;

                if (Selection.objects.Any(obj => (obj as DesignDataScriptableObject) != null && (selectedDesignData = (DesignDataScriptableObject) obj)))
                {
                    LoadDesignData(selectedDesignData);
                }
            }
            
            public void LoadDesignData(DesignDataScriptableObject designData)
            {
                _designData = designData;
                Rerender();
            }
        }
    }
}

/*
    // VisualElements objects can contain other VisualElement following a tree hierarchy.
    VisualElement label = new Label("Hello World! From C#");
    root.Add(label);

    // Import UXML
    var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/CreateItemWindow.uxml");
    VisualElement labelFromUXML = visualTree.CloneTree();
    root.Add(labelFromUXML);
    
    // A stylesheet can be added to a VisualElement.
    // The style will be applied to the VisualElement and all of its children.
    var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/CreateItemWindow.uss");
    VisualElement labelWithStyle = new Label("Hello World! With Style");
    labelWithStyle.styleSheets.Add(styleSheet);
    root.Add(labelWithStyle);
 */