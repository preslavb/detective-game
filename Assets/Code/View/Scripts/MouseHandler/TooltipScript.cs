using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipScript : MonoBehaviour
{
    public static TooltipScript Instance { get; private set; }

    private RectTransform _parentRectTransform;
    [SerializeField] private Camera _camera;

    [SerializeField] private TextMeshProUGUI _textMeshPro;

    public void ShowTooltip(string tooltipText)
    {
        gameObject.SetActive(true);
        _textMeshPro.text = tooltipText;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        _parentRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, Input.mousePosition, _camera,
            out var newPoint);

        transform.localPosition = newPoint + new Vector2(25, -25);
    }
}
