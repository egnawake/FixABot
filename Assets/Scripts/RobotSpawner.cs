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
        if (!readyToSpawn)
            return;

        if (currentRobot >= robots.Length)
            return;

        Interactive robot = robots[currentRobot];
        robot.OnInteracted.AddListener(RobotFixed);
        robot.GetComponent<Animator>().SetTrigger("Enter");

        readyToSpawn = false;
    }
}
