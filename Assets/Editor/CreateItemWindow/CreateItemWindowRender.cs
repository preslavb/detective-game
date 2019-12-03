using System;
using System.Linq;
using DesignData;
using Doozy.Engine.Extensions;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Doozy.Editor.Nody.Windows
{
    public partial class NodyWindow
    {
        public partial class CreateItemWindow
        {
            private void Rerender()
            {
                // Clear the root element
                root.Clear();

                // If there is no current item being edited, just show the create button
                if (_designData == null)
                {
                    var element = new TextField("New Data Name:");
                    element.value = _newDesignDataName;
                    
                    element.RegisterCallback<ChangeEvent<string>>(evt => _newDesignDataName = evt.newValue);
                    
                    root.Add(element);
                    
                    root.Add(new Button(() =>
                    {
                        _designData = CreateNewDesignDataObject();
                        Rerender();
                    })
                    {
                        text = "Create New Item"
                    });
                }
                else
                {
                    // Create the horizontal flex
                    horizontalGroup = new VisualElement();
                    
                    horizontalGroup.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
                    
                    root.Add(horizontalGroup);
                    
                    // Create the inspector for the asset
                    _designDataPropertyTree = PropertyTree.Create(_designData);
                    _propertyTreeContainer = new IMGUIContainer(() =>
                    {
                        _designDataPropertyTree.Draw();
                    });
                    
                    _propertyTreeContainer.AddToClassList("property-container");
                    horizontalGroup.Add(_propertyTreeContainer);
                    
                    InitializePreviews();
                    _boardRepresentationPreview.style.width = new StyleLength(Length.Percent(50));
                    horizontalGroup.Add(_boardRepresentationPreview);

                    if (_designData.DataType == DesignDataScriptableObject.DesignDataType.Event && _designData.EventGraph != null)
                    {
                        // The label for the nody window
                        var label = new Label("Nody Flow");
                        
                        label.AddToClassList("nody-window-label");
                        
                        root.Add(label);
                        
                        // Create the container for the nody window
                        _nodyWindowContainer = new IMGUIContainer(() => _nodyWindowEmbedded.OnGUI());
                        
                        _nodyWindowContainer.AddToClassList("nody-window");

                        _nodyWindowContainer.style.height = _nodyWindowEmbedded.position.height;

                        // Create and hook up the nody window
                        _nodyWindowEmbedded.LoadGraph(_designData.EventGraph);
                        
                        // Add all of the elements
                        root.Add(_nodyWindowContainer);
                    }
                }
            }


            private IMGUIContainer _boardRepresentationPreview;
            private PreviewRenderUtility _boardRepresentationPreviewRenderUtility;

            private void InitializePreviews()
            {
                if (_boardRepresentationPreviewRenderUtility != null)
                {
                    _boardRepresentationPreviewRenderUtility.Cleanup();
                }
                
                _boardRepresentationPreviewRenderUtility = new PreviewRenderUtility();
                var instantiatedPrefab = _boardRepresentationPreviewRenderUtility.InstantiatePrefabInScene(_designData.ViewProperties.ViewIdentifierScript
                    .gameObject);
                instantiatedPrefab.GetComponentInChildren<CanvasRenderer>().DisableRectClipping();

                _boardRepresentationPreviewRenderUtility.camera.transform.position = new Vector3(5, 0.5f, -1.5f);
                _boardRepresentationPreviewRenderUtility.camera.transform.LookAt(instantiatedPrefab.transform);
                
                _boardRepresentationPreview = new IMGUIContainer(() =>
                {

                    var rect = new Rect(0, 0, 200, 200);

                    _boardRepresentationPreviewRenderUtility.BeginPreview(rect, "popup");
                    _boardRepresentationPreviewRenderUtility.camera.clearFlags = CameraClearFlags.Skybox;
                    

                    
                    _boardRepresentationPreviewRenderUtility.Render(true, false);
                    var endRender = _boardRepresentationPreviewRenderUtility.EndPreview();
                    
                    GUI.DrawTexture(rect, endRender);
                    //previewRenderUtility.camera.scene
                    //_boardRepresentationEditor.OnInteractivePreviewGUI(new Rect(0, 0, 200, 200), "popup");
                });
            }
        }
    }
}