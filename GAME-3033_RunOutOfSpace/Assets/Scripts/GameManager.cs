using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string next_level_;
    [SerializeField] private string prev_level_;
    [SerializeField] private GameObject overlay_panel_;
    [SerializeField] private GameObject credits_panel_;
    [SerializeField] private AudioClip click_sfx_;
    private AudioSource audio_source_;
    
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
        audio_source_ = GetComponent<AudioSource>();
        timer_ = victory_timer_;
        player_ = GameObject.FindObjectOfType<Player.ThirdPersonController>();
    }

    private void FixedUpdate()
    {
        if (player_ == null)
        {
            return;
        }
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
            DoShowOverlayPanel();
        }
    }
    
    /// <summary>
    /// Loads next level
    /// </summary>
    public void DoLoadNextLevel()
    {
        audio_source_.PlayOneShot(click_sfx_);
        StartCoroutine(Delay());
        SceneManager.LoadScene(next_level_);
    }

    /// <summary>
    /// Loads prev level
    /// </summary>
    public void DoLoadPrevLevel()
    {
        audio_source_.PlayOneShot(click_sfx_);
        StartCoroutine(Delay());
        SceneManager.LoadScene(prev_level_);
    }

    /// <summary>
    /// Closes app
    /// </summary>
    public void DoQuitApp()
    {
        audio_source_.PlayOneShot(click_sfx_);
        StartCoroutine(Delay());
        Application.Quit();
    }

    /// <summary>
    /// Shows hidden panel
    /// </summary>
    public void DoShowOverlayPanel()
    {
        audio_source_.PlayOneShot(click_sfx_);
        overlay_panel_.SetActive(true);
    }

    /// <summary>
    /// Hides overlay panel
    /// </summary>
    public void DoHideOverlayPanel()
    {
        audio_source_.PlayOneShot(click_sfx_);
        overlay_panel_.SetActive(false);
    }

    /// <summary>
    /// Shows hidden panel
    /// </summary>
    public void DoShowCreditsPanel()
    {
        audio_source_.PlayOneShot(click_sfx_);
        credits_panel_.SetActive(true);
    }

    /// <summary>
    /// Hides overlay panel
    /// </summary>
    public void DoHideCreditsPanel()
    {
        audio_source_.PlayOneShot(click_sfx_);
        credits_panel_.SetActive(false);
    }

    /// <summary>
    /// General delay function for level loading, show explosion before game over, etc.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.0f);
    }

}
