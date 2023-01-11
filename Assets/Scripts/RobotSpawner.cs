using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    [SerializeField] private Interactive[] robots;
    [SerializeField] private Interactive callButton;

    private bool readyToSpawn = true;
    private int currentRobot = 0;

    public void RobotFixed()
    {
        robots[currentRobot].GetComponent<Animator>().SetTrigger("Exit");
        currentRobot++;
        readyToSpawn = true;
    }

    private void Start()
    {
        callButton.OnInteracted.AddListener(SendInRobot);
    }

    private void SendInRobot()
    {
        Debug.Log($"{readyToSpawn}");
        if (!readyToSpawn)
            return;

        Interactive robot = robots[currentRobot];
        robot.OnInteracted.AddListener(RobotFixed);
        robot.GetComponent<Animator>().SetTrigger("Enter");

        readyToSpawn = false;
    }
}
