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

    private void LateUpdate()
    {
        Vector3 cameraPos = _camera.transform.position;

        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x <= 25)
        {
            cameraPos.x -= panSpeed * Time.deltaTime;
        }
        if (mousePos.x >= Screen.width - 50)
        {
            cameraPos.x += panSpeed * Time.deltaTime;
        }
        if (mousePos.y <= 25)
        {
            cameraPos.z -= panSpeed * Time.deltaTime;
        }
        if (mousePos.y >= Screen.height - 50)
        {
            cameraPos.z += panSpeed * Time.deltaTime;
        }

        transform.position = cameraPos;

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

        // if (Input.GetAxis("Mouse ScrollWheel") > 0f && _camera.fieldOfView < 115f)
        // {
        //     //  _camera.DOFieldOfView(_camera.fieldOfView + 15, .4f);
        //     gameObject.transform.DOMoveY(gameObject.transform.position.y + 15, .4f);
        //     // cameraSpeed = cameraSpeed - 2;
        // }
        //
        //
        // if (Input.GetAxis("Mouse ScrollWheel") < 0f && _camera.fieldOfView > 45)
        // {
        //     // _camera.DOFieldOfView(_camera.fieldOfView-15, .4f);
        //     gameObject.transform.DOMoveY(gameObject.transform.position.y - 15, .4f);
        //     // cameraSpeed = cameraSpeed + 2;
        // }

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