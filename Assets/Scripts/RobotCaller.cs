using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCaller : MonoBehaviour
{
    [SerializeField] private GameObject robot;
    [SerializeField] private Interactive callButton;
    
    private void Start()
    {
        callButton.OnInteracted.AddListener(CallInRobot);
    }
    
    private void CallInRobot()
    {
        Animator anim = robot.GetComponent<Animator>();
        anim.SetTrigger("Enter");
    }
}
