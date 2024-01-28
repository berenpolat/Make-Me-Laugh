using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Camera Options
    [Header("Camera Options")]
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float panSpeed = 50f;
    #endregion

    public Camera _camera;
    public GameObject player;

    #region Unity Methods
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        _camera = gameObject.GetComponent<Camera>();
    }

    private void F1Pressed()
    {
        F1Position();
    }

    private void FixedUpdate()
    {
        // Get the player's movement input
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Calculate the new position based on player's movement
        Vector3 newPosition = transform.position;
        newPosition += new Vector3(horizontalMovement, 0, verticalMovement) * panSpeed * Time.deltaTime;

        // Apply the new position
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            Cursor.lockState = CursorLockMode.Confined;
            F1Position();
        }
        if (Input.GetKey(KeyCode.F2))
        {
            F2Position();
        }
        if (Input.GetKey(KeyCode.F3))
        {
            F3Position();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _camera.transform.DORotate(new Vector3(_camera.transform.eulerAngles.x + 1, _camera.transform.eulerAngles.y, _camera.transform.eulerAngles.z), .2f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _camera.transform.DORotate(new Vector3(_camera.transform.eulerAngles.x - 1, _camera.transform.eulerAngles.y, _camera.transform.eulerAngles.z), .2f);
        }
    }
    #endregion

    #region Utils
    private void F1Position()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        _camera.transform.DORotate(new Vector3(45, _camera.transform.rotation.y, _camera.transform.rotation.z), .4f);
        _camera.transform.DOMove(new Vector3(desiredPosition.x, 150, desiredPosition.z - 150), .4f);
    }

    private void F2Position()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        _camera.transform.DORotate(new Vector3(9, _camera.transform.rotation.y, _camera.transform.rotation.z), .4f);
        _camera.transform.DOMove(new Vector3(desiredPosition.x, desiredPosition.y + 8, desiredPosition.z - 50f), .4f);
    }

    private void F3Position()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        _camera.transform.DORotate(new Vector3(9, _camera.transform.rotation.y - 180, _camera.transform.rotation.z), .4f);
        _camera.transform.DOMove(new Vector3(desiredPosition.x, desiredPosition.y + 8, desiredPosition.z + 50f), .4f);
    }
    #endregion
}
