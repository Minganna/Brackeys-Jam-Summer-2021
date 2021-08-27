using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitanimationStates : MonoBehaviour
{
    public GameObject punchBox;
 public void PunchAttack()
    {
        punchBox.SetActive(true);
    }
    public void ExitPunch()
    {
        punchBox.SetActive(false);
        PlayerController.instance.canMove = true;
        PlayerController.instance.canPunch = true;
    }

    public void ExitWinning()
    {
        Debug.Log("Here");
        PlayerController.instance.canMove = true;
        PlayerController.instance.StopWinningAnim();
    }
}
