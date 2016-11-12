using System;
using UnityEngine;

public class InWorldButton : MonoBehaviour
{
    public bool Active;
    public string NameID;
    public GameObject HoverEffect;
    public GameObject DisabledEffect;

    public float Width { get { return _RectTrans.rect.width; } }
    public float Height { get { return _RectTrans.rect.height; } }

    private bool _Hover;
    private bool _Disabled;

    private RectTransform _RectTrans;

    void Start()
    {
        _RectTrans = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (HoverEffect != null)
            Shared.SetGOActive(HoverEffect, _Hover);

        if (DisabledEffect != null)
            Shared.SetGOActive(DisabledEffect, _Disabled);
    }
    void LateUpdate()
    {
        _Hover = false;
        _Disabled = false;
    }
    public void Press()
    {
        //Debug.Log("Press: " + NameID);
    }
    public void Hover()
    {
        _Hover = true;
        //Debug.Log("Hover: " + NameID);
    }
    public void Disabled()
    {
        _Disabled = true;
    }
}