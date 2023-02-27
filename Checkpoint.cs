using UnityEngine;

public class Checkpoint : MonoBehaviour {
    public CheckpointData info;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
            CheckpointManager.instance.passCheckpoint(info.id);
    }
}

[System.Serializable]
public class CheckpointData {
    public int id;
    public Checkpoint next;
    public bool isFirst;
}