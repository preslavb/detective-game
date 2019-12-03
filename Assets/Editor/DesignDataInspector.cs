using System;
using DesignData;
using Doozy.Editor.Nody.Windows;
using Packages.Rider.Editor.PostProcessors;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor
{
    [CustomEditor(typeof(DesignDataScriptableObject))]
    public class DesignDataInspector: UnityEditor.Editor
    {
        private DesignDataScriptableObject _designData;
        private VisualElement _rootElement;

        public void OnEnable()
        {
            _designData = target as DesignDataScriptableObject;

            _rootElement = new VisualElement();
        }

        public override VisualElement CreateInspectorGUI()
        {
            _rootElement.Clear();

            _rootElement.Add(new Button(() =>
            {
                var window = EditorWindow.GetWindow<NodyWindow.CreateItemWindow>(typeof(SceneView));
                window.LoadDesignData(_designData);
            })
            {
                text = "Open in editor window"
            });
            
            return _rootElement;
        }
    }
}