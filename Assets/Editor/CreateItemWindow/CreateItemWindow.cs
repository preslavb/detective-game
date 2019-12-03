using System;
using System.Linq;
using DesignData;
using Doozy.Editor.Internal;
using Doozy.Editor.Nody.Windows;
using Doozy.Engine.Extensions;
using Doozy.Engine.Nody.Models;
using Sirenix.OdinInspector.Editor;
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
            private PropertyTree _designDataPropertyTree;

            private VisualElement root;

            private VisualElement horizontalGroup;
            
            [SerializeField] private NodyWindow _nodyWindowEmbedded;
            [SerializeField] private IMGUIContainer _nodyWindowContainer;
            [SerializeField] private IMGUIContainer _propertyTreeContainer;
            
            [MenuItem("Tools/Detective Game/Create Items", priority = 10000)]
            public static void ShowExample()
            {
                CreateItemWindow wnd = GetWindow<CreateItemWindow>(typeof(SceneView));
                wnd.titleContent = new GUIContent("Create Item");

                wnd.InitializePreviews();
            }

            public void OnEnable()
            {
                // Create the root visual element
                var scrollview = new ScrollView(ScrollViewMode.Vertical);
                
                _nodyWindowEmbedded = ScriptableObject.CreateInstance<NodyWindow>();
                _nodyWindowEmbedded.m_isEmbeddedWindow = true;
                
                rootVisualElement.Add(scrollview);
                
                root = scrollview;
                
                root.Add(new ScrollView());
                
                // Load the stylesheet
                var stylesheet =
                    AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/CreateItemWindow/CreateItemWindow.uss");
                
                // Add the stylesheet to the root and all of the necessary classes
                root.AddToClassList("root");
                rootVisualElement.styleSheets.Add(stylesheet);

                _designData = null;

                CheckSelection();
                UpdateData();

                Rerender();
            }

            private void UpdateData()
            {
                if (_designData != null)
                {
                    _propertyTreeContainer.MarkDirtyRepaint();
                    _designDataPropertyTree.UpdateTree();
                    
                    if (_designData.DataType == DesignDataScriptableObject.DesignDataType.Event && _designData.EventGraph != null)
                    {
                        _nodyWindowEmbedded.CurrentMousePosition = _nodyWindowEmbedded.CurrentMousePosition - (Vector2.up * 50);
                        _nodyWindowEmbedded.Update(); 
                        _nodyWindowContainer.MarkDirtyRepaint(); 
                        _nodyWindowEmbedded.position = _nodyWindowEmbedded.position.WithWidth(position.width);
                    }
                }
            }

            private void CheckSelection()
            {
                DesignDataScriptableObject selectedDesignData = null;

                if (Selection.objects.Any(obj => (obj as DesignDataScriptableObject) != null && (selectedDesignData = (DesignDataScriptableObject) obj)))
                {
                    LoadDesignData(selectedDesignData);
                    return;
                }

                Rerender();
            }

            private void Update()
            {
                UpdateData();
            }

            private void OnDisable()
            {
                if (_nodyWindowEmbedded != null) _nodyWindowEmbedded.OnDisable();
                _boardRepresentationPreviewRenderUtility?.Cleanup();
            }
            private void OnInspectorUpdate() { if (_nodyWindowEmbedded != null) _nodyWindowEmbedded.OnInspectorUpdate(); }
            private void OnFocus() { if (_nodyWindowEmbedded != null) _nodyWindowEmbedded.OnFocus(); }
            private void OnLostFocus() { if (_nodyWindowEmbedded != null) _nodyWindowEmbedded.OnLostFocus(); }

            private void OnSelectionChange()
            {
                CheckSelection();
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