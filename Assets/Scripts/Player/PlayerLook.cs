using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSesnsitivity = 30f;
    public float ySesnsitivity = 30f;

    public void LookProcess(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -= (mouseY * Time.deltaTime) * ySesnsitivity;//calculate camera rotation
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);//Clamp the rotation to avoid over rotation
        // apply rotation to camera
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // rotate the player body
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSesnsitivity);
    }
}
