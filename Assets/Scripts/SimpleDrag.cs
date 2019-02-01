using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDrag : MonoBehaviour
{
    public bool DragParent;

    private Camera _camera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private Vector3 _prevPosition;

    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.main;
        if (_camera == null)
            enabled = false;

        _myTransform = transform;
    }

    private void FixedUpdate()
    {
        DragWithTouch();
    }

    /// <summary>
    /// Cannot test until have a touch monitor
    /// </summary>
    private void DragWithTouch()
    {
        if (Input.touchCount == 0) return;

        // get the first touch
        var touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Stationary && touch.phase != TouchPhase.Moved) return; // Canceled, began and ended are not relevant and should be ignored

        // get the touch position from the screen touch to world point
        var position = transform.position; // cache for performance. GC will take care of it
        var touchedPos = _mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, position.z));
        // lerp and set the position of the current object to that of the touch, but smoothly over time.
        position = Vector3.Lerp(position, touchedPos, Time.deltaTime);

        if (DragParent)
            transform.parent.position = position;
        else
            transform.position = position;
    }

    private Vector3 _screenPoint;
    private Vector3 _offset;
    private Camera _mainCamera;
    private Transform _myTransform;

    private void OnMouseDown()
    {
        var position = DragParent ? _myTransform.parent.position : _myTransform.position;
        _screenPoint = _camera.WorldToScreenPoint(position);
        _offset = position - _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
    }

    private void OnMouseDrag()
    {
        var cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
        var cursorPosition = _camera.ScreenToWorldPoint(cursorPoint) + _offset;

        if (DragParent)
            _myTransform.parent.position = cursorPosition;
        else
            transform.position = cursorPosition;
    }
}
