using OutlineEffect.Demo;
using Structs;
using UnityEngine;
using UnityEngine.UI;
using Outline = OutlineEffect.Outline;

//[RequireComponent(typeof(Outline))]
public class MouseOver : MonoBehaviour
{
    /// <summary>
    /// store cache for performance
    /// </summary>
    private Outline _myOutline;
    /// <summary>
    /// store cache for performance
    /// </summary>
    private Camera _camera;
    /// <summary>
    /// store cache for performance
    /// </summary>
    private Answer _answer;

    private Text _selectedLabel;

    private void Awake()
    {
        _camera = Camera.main;
        if (_camera == null)
            enabled = false;

        _myOutline = GetComponent<Outline>();
        _answer = GetComponent<Answer>();
        _selectedLabel = GameObject.FindGameObjectWithTag("AnswerLabel").GetComponent<Text>();
    }

    private void Start()
    {
        _myOutline.enabled = false;
    }
    
	// Update is called once per frame
	private void FixedUpdate ()
    {
        ToggleOverlay();
    }

    private void ToggleOverlay()
    {
        if (SelectionManager.LastSelected == _answer)
            return;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, float.PositiveInfinity))
            _myOutline.enabled = hit.transform == transform;
        else
            _myOutline.enabled = false;
    }


    private void OnMouseDown()
    {
        SelectionManager.LastSelected = _answer;
        _selectedLabel.text = _answer.Text;
    }
}
