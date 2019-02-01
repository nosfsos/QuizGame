using UnityEngine;

namespace DefaultNamespace
{
    public class RotateTargetWithMouse : MonoBehaviour
    {
        private Camera _camera;
        private bool _getPrevPosition;
        private Vector3 _prevMousePosition;
        private Transform _target;

        public bool RotateParent;
        public float RotationSpeed = 25f;
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(1))
                SetTarget();
            if (Input.GetMouseButtonUp(1))
                _target = null;

            if (Input.GetMouseButton(1))
                RotateWithMouse();
        }

        private void SetTarget()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, float.PositiveInfinity) == false) return; // hit nothing

            if (hit.transform.CompareTag("Movable") == false) // hit not movable
                return;

            _target = RotateParent ? hit.transform.parent : hit.transform;
        }


        private void RotateWithMouse()
        {
            if (_target == null) return;

            _target.Rotate((-Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime), (Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime), 0, Space.Self);

            //Input.mousePosition = new Vector3(Screen.currentResolution.width / 2f, Screen.currentResolution.height / 2f, 0);

            //_getPrevPosition = _getPrevPosition == false;
            //if (_getPrevPosition)
            //{
            //    _prevMousePosition = new Vector3(Input.mousePosition.y, -Input.mousePosition.x, 0);
            //    return;
            //}

            //var currentPos = new Vector3(Input.mousePosition.y, -Input.mousePosition.x, 0);
            //var delta = currentPos - _prevMousePosition;



            //_target.rotation = Quaternion.Lerp(_target.rotation, Quaternion.Euler(delta), Time.deltaTime);

        }
    }
}