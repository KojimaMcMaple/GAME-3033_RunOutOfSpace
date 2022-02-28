using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timer_txt_;
    [SerializeField] private float victory_timer_ = 300f;
    [SerializeField] private Animator door_anim_;
    [SerializeField] private Light door_light_;
    private float timer_ ;
    private bool is_times_up_ = false;
    private bool is_victory_ = false;
    private Player.ThirdPersonController player_;

    private void Awake()
    {
        timer_ = victory_timer_;
        player_ = GameObject.FindObjectOfType<Player.ThirdPersonController>();
    }

    private void FixedUpdate()
    {
        if (!is_times_up_ && !is_victory_)
        {
            if (timer_ > 0)
            {
                timer_ -= Time.deltaTime;
            }
            else
            {
                timer_ = 0;
                door_light_.color = Color.green;
                door_anim_.SetTrigger("IsOpen");
                is_times_up_ = true;
            }
            timer_txt_.text = timer_.ToString();
        }
        if (player_.is_victory)
        {
            timer_txt_.text = "YOU WIN!";
        }
    }

}
