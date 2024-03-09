using UnityEngine;

public class CamScript : MonoBehaviour {
    [SerializeField] private int PanSpeed;
    [SerializeField] private int MoveSpeed;

    private Vector2 _rotation;

    private void Update() {
        MouseLook();
        Move();
    }

    private void MouseLook() {
        float mouseX = Input.GetAxis("Mouse X") * PanSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * PanSpeed * Time.deltaTime;

        _rotation.y += mouseX;
        _rotation.x -= mouseY;

        _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);

        transform.eulerAngles = new Vector3(_rotation.x, _rotation.y, 0);
    }

    private void Move() {
        float horizontal = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime;

        //vertical *= Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? _sprint : _moveSpeed;
        transform.Translate(horizontal, 0, vertical);
    }
}