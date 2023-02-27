using UnityEngine;

public class CheckpointManager : MonoBehaviour {
    
    public static CheckpointManager instance;

    [SerializeField] private Timer timer;
    [SerializeField] private TipManager tipManager;
    [SerializeField] private Checkpoint currentCheckpoint;


    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }
    }

    public void passCheckpoint(int _id) {
        
        if (!(_id == currentCheckpoint.info.id)) {
            return;
        }

        if (currentCheckpoint.info.isFirst) {
            if (timer.GetRunning()) {
                timer.Lap();
            } else {
                timer.StartTimer();
                tipManager.HideTip();
            }
        }

        currentCheckpoint = currentCheckpoint.info.next;
    }

}