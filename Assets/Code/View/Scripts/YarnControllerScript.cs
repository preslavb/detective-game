using System;
using System.Linq;
using UnityEngine;

namespace View.Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    public class YarnControllerScript: MonoBehaviour
    {
        private Transform[] _transforms = new Transform[2];

        private LineRenderer _lineRenderer;

        public LineRenderer LineRenderer => _lineRenderer;

        public void SetItem(int index, Transform value)
        {
            if (index < _transforms.Length && _transforms[index] == null)
            {
                _transforms[index] = value;
            }
            
            else throw new Exception("Items should be set only once in the yarn controller");
        }

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (_transforms.All(transformToCheck => transformToCheck != null))
            {
                _lineRenderer.SetPosition(0, _transforms[0].position);
                _lineRenderer.SetPosition(1, _transforms[1].position);
            }
        }
    }
}