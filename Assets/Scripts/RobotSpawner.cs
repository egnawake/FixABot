using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    // [SerializeField] private Robot[] robots;
    // [SerializeField] private Interactive callButton;

    // private bool readyToSpawn = true;
    // private int nextRobot = 0;

    // public void RobotFixed()
    // {
    //     readyToSpawn = true;
    // }

    // private void Start()
    // {
    //     callButton.OnInteracted.AddListener(SpawnRobot);
    // }

    // private void SpawnRobot()
    // {
    //     if (!readyToSpawn)
    //         return;

    //     Robot robot = robots[nextRobot++];
    //     robot.GetComponent<Animator>().SetTrigger("Enter");
    //     robot.OnFixed.AddListener(RobotFixed);

    //     readyToSpawn = false;
    // }
}
