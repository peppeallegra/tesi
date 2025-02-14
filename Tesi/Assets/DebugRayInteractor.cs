using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DebugRayInteractor : MonoBehaviour
{
    public XRRayInteractor rayInteractor;

    void Update()
    {
        if (rayInteractor != null)
        {
            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                Debug.Log("🔵 Il raggio sta colpendo: " + hit.collider.gameObject.name);
            }
            else
            {
                Debug.Log("❌ Il raggio NON sta colpendo nulla!");
            }
        }
    }
}
