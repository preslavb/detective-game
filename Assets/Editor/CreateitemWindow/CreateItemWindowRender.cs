using DesignData;
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
                rootVisualElement.Clear();
                
                // If there is no current item being edited, just show the create button
                if (_designData == null)
                {
                    rootVisualElement.Add(new Button(() =>
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
                    // Create the inspector for the asset
                    VisualElement designDataInspector = new InspectorElement(_designData);
                    rootVisualElement.Add(designDataInspector);
                    
                    // Create the container for the nody window
                    _nodyWindowContainer = new IMGUIContainer(() => _nodyWindowEmbedded.OnGUI());

                    if (_designData.Test != null)
                    {
                        // Create and hook up the nody window
                        _nodyWindowEmbedded = CreateInstance<NodyWindow>();
                        _nodyWindowEmbedded.m_isEmbeddedWindow = true;
                    }
                    
                    // Add all of the elements
                    rootVisualElement.Add(_nodyWindowContainer);
                }
            }
        }
    }
}