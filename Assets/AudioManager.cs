using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AudioManager : MonoBehaviour
{
    static AudioSource bgmInstance;
    static AudioSource sfxInstance;
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfxBall;
    [SerializeField] AudioSource sfxTurret;
    [SerializeField] GameObject bgmSlider;
    [SerializeField] GameObject sfxSlider;
    [SerializeField] TMP_Text bgmValueSliderText;
    [SerializeField] TMP_Text sfxValueSliderText;
    [SerializeField] GameObject toggleMute;


    private void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            bgm.volume = PlayerPrefs.GetFloat("BGMVolume");
            bgmSlider.GetComponent<UnityEngine.UI.Slider>().value = bgm.volume;
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxBall.volume = PlayerPrefs.GetFloat("SFXVolume");
            if (sfxTurret != null)
            {
                sfxTurret.volume = PlayerPrefs.GetFloat("SFXVolume");
            }
            sfxSlider.GetComponent<UnityEngine.UI.Slider>().value = sfxBall.volume;
        }
        if (PlayerPrefs.HasKey("Mute"))
        {
            if (PlayerPrefs.GetInt("Mute") == 1)
            {
                toggleMute.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                bgm.mute = true;
                sfxBall.mute = true;
                if (sfxTurret != null)
                {
                    sfxTurret.mute = true;
                }
            }
            else
            {
                toggleMute.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                bgm.mute = false;
                sfxBall.mute = false;
                if (sfxTurret != null)
                {
                    sfxTurret.mute = false;
                }
            }
        }
        else
        {
            toggleMute.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            bgm.mute = false;
            sfxBall.mute = false;
            if (sfxTurret != null)
            {
                sfxTurret.mute = false;
            }
        }

        if (bgmInstance != null)
        {
            Destroy(bgm.gameObject);
            bgm = bgmInstance;
        }
        else
        {
            bgmInstance = bgm;
            bgm.transform.SetParent(null);
            DontDestroyOnLoad(bgm.gameObject);
        }


        // if (sfxInstance != null)
        // {
        //     Destroy(sfxBall.gameObject);
        //     sfxBall = sfxInstance;
        // }
        // else
        // {
        //     sfxInstance = sfxBall;
        //     sfxBall.transform.SetParent(null);
        //     DontDestroyOnLoad(sfxBall.gameObject);
        // }

    }

    public void SliderBGM()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            bgm.volume = PlayerPrefs.GetFloat("BGMVolume");
        }
        bgm.volume = bgmSlider.GetComponent<UnityEngine.UI.Slider>().value;
        var volume100 = (int)(bgm.volume * 100);
        bgmValueSliderText.text = volume100.ToString();
        PlayerPrefs.SetFloat("BGMVolume", bgm.volume);
    }

    public void SliderSFX()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxBall.volume = PlayerPrefs.GetFloat("SFXVolume");
        }
        sfxBall.volume = sfxSlider.GetComponent<UnityEngine.UI.Slider>().value;
        if (sfxTurret != null)
        {
            sfxTurret.volume = sfxSlider.GetComponent<UnityEngine.UI.Slider>().value;
        }
        var volume100 = (int)(sfxBall.volume * 100);
        sfxValueSliderText.text = volume100.ToString();
        PlayerPrefs.SetFloat("SFXVolume", sfxBall.volume);

    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (bgm.isPlaying)
        {
            bgm.Stop();
        }
        bgm.clip = clip;
        bgm.loop = loop;
        bgm.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxBall.isPlaying)
        {
            sfxBall.Stop();
        }
        sfxBall.clip = clip;
        sfxBall.Play();
    }

    public void Mute()
    {
        if (toggleMute.GetComponent<UnityEngine.UI.Toggle>().isOn)
        {
            bgm.mute = true;
            sfxBall.mute = true;
            if (sfxTurret != null)
            {
                sfxTurret.mute = true;
            }
            PlayerPrefs.SetInt("Mute", 1);
        }
        else
        {
            bgm.mute = false;
            sfxBall.mute = false;
            if (sfxTurret != null)
            {
                sfxTurret.mute = false;
            }
            PlayerPrefs.SetInt("Mute", 0);
        }
    }

}
