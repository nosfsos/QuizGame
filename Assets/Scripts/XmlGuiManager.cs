using System;
using UnityEngine;

[RequireComponent(typeof(CodeName))]
public class XmlGuiManager : MonoBehaviour
{
    private string _code;
    public Types MyType;

    private void Awake()
    {
        _code = GetComponent<CodeName>().Code;

        UpdateGui();
    }

    private void UpdateGui()
    {
        switch (MyType)
        {
            case Types.GuiText:
                break;
            case Types.Question:
                break;
            case Types.Answer:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }
}