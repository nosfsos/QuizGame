using Structs;
using UnityEngine;
using UnityEngine.UI;

public static class SelectionManager
{
    private static Answer  _lastSelected;

    private static bool _somethingIsSelected;

    public static Answer LastSelected
    {
        get
        {
            if (_somethingIsSelected == false)
                _lastSelected = GameObject.Find("DummyAnswer").GetComponent<Answer>();

            _somethingIsSelected = true;

            return _lastSelected;
        }
        set
        {
            _lastSelected = value;

            if (value != null) return;

            _somethingIsSelected = false;
            GameObject.FindGameObjectWithTag("AnswerLabel").GetComponent<Text>().text = "";

            //Debug.Log("Selected: " + value.name);
        }
    }
}