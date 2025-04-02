using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    public List<Transform> checkpoints;
    private Stack<Transform> checkpointStack = new Stack<Transform>();
    private Transform currentTarget;
    public float raceTime = 60f; // in-game countdown timer
    public Text timerText;
    public Text messageText;
    public Text bonusTimeText; // shows the time added when checkpoint crossed

    void Start()
    {
        messageText.text = "";
        bonusTimeText.enabled = true;

        foreach (Transform checkpoint in checkpoints)
        {
            checkpoint.gameObject.SetActive(false);
        }

        // Push checkpoints in reverse order so the first one is at the top
        for (int i = checkpoints.Count - 1; i >= 0; i--)
        {
            checkpointStack.Push(checkpoints[i]);
        }
        SetNextCheckpoint();
        StartCoroutine(CountdownTimer());
      
    }

    void Update()
    {
        if (currentTarget != null)
        {
            Debug.DrawLine(transform.position, currentTarget.position, Color.red);
        }
    }

    void SetNextCheckpoint()
    {
        if (checkpointStack.Count > 0)
        {
            currentTarget = checkpointStack.Pop();
            currentTarget.gameObject.SetActive(true); // Enable the next checkpoint
            currentTarget.GetComponent<Renderer>().material.color = Color.red; // Target checkpoint is made a different colour
        }
        else
        {
            WinGame();
        }
    }

    public void CheckpointReached(Checkpoint checkpoint)
    {
        if (checkpoint.transform == currentTarget)
        {
            raceTime += 5f;
            StartCoroutine(ShowBonusTime()); // Shows time Added
            SetNextCheckpoint();
           
        }
    }

    IEnumerator CountdownTimer()
    {
        while (raceTime > -1)
        {
            timerText.text = "Time: " + raceTime.ToString("F0");
            yield return new WaitForSeconds(1f);
            raceTime--;

            if (raceTime == 0)
            {
                LoseGame();
            }
        }
    }

    IEnumerator ShowBonusTime()
    {
        bonusTimeText.text = "+5 seconds!";
        bonusTimeText.color = Color.green; 
        yield return new WaitForSeconds(1.5f);
        bonusTimeText.text = "";
    }

    void WinGame()
    {
        messageText.text = "You Win!";
        bonusTimeText.enabled = false;// disbled so it doesnt show the added time after the race
        StopAllCoroutines();
    }

    void LoseGame()
    {
        messageText.text = "You Lose!";
        bonusTimeText.enabled = false;
        StopAllCoroutines();
    }
}
