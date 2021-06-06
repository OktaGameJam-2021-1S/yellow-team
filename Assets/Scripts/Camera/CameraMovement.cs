using UnityEngine;
using ThirteenPixels.Soda;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    GlobalTransform player;
    [SerializeField]
    float cameraCatchUpDistance = 2f;
    [SerializeField]
    float cameraSpeed = 0.01f;
    [SerializeField]
    float cameraOffsetZ = -10f;

    Vector3? cameraDestination;

    private void Update()
    {
        cameraDestination = new Vector3(transform.position.x, transform.position.y, player.componentCache.position.z + cameraOffsetZ);

        if (cameraDestination != null && Mathf.Abs(player.componentCache.position.z - transform.position.z + cameraOffsetZ) < cameraCatchUpDistance / 2)
        {
            cameraDestination = null;
        }

        if (cameraDestination != null)
        {
            Vector3 value = cameraDestination.Value;
            value.z += cameraOffsetZ;
            transform.position = Vector3.Lerp(transform.position, value, cameraSpeed);
        }
    }
}
