using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [SerializeField]public float rotationSpeed = 1.0f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
