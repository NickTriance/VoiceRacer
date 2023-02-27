using UnityEngine;

public class CarBodyBrakelightController : MonoBehaviour
{
    private Material bl_mat;

    [SerializeField] private bool hasLights = true;

    private void Start() {
        if (!hasLights)
            return;
        bl_mat = GetComponent<Renderer>().materials[0];
        DisableBL();
    }

    public void DisableBL() {
        if (!hasLights)
            return;
        if (bl_mat == null) {
            bl_mat = GetComponent<Renderer>().materials[0];
        }
        bl_mat.DisableKeyword("_EMISSION");
    }

    public void EnableBL() {
        if (!hasLights)
            return;
        if (bl_mat == null) {
            bl_mat = GetComponent<Renderer>().materials[0];
        }
        bl_mat.EnableKeyword("_EMISSION");
    }
}
