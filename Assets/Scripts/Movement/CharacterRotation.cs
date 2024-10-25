using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    void Update()
    {
        float cameraYRotation = _camera.transform.eulerAngles.y;

        // ��������� �������� �������
        Vector3 newRotation = new Vector3(transform.eulerAngles.x, cameraYRotation, transform.eulerAngles.z);
        transform.eulerAngles = newRotation;
    }
}
