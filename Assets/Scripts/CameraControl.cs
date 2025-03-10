using Unity.Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    CinemachineBasicMultiChannelPerlin noise;
    void Awake() {
         noise = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void ShakeCamera() {
        noise.AmplitudeGain = 5f; 
        noise.FrequencyGain = 1f;

    }

    public void StopCameraShake() {
        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
    }
   
}