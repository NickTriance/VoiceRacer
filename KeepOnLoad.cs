using UnityEngine;

public class KeepOnLoad : MonoBehaviour
{
    public static KeepOnLoad instance;
    void Start()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }
}
