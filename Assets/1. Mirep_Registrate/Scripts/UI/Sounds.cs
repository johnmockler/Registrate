using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds: MonoBehaviour
{
    private AudioSource audioSource;


    //game + menu sounds
    [SerializeField]
    public AudioClip enterPressed;
    [SerializeField]
    public AudioClip enterFailed;
    [SerializeField]
    public AudioClip targetPlaced;

    //game sounds
    [SerializeField]
    public AudioClip backPressed;
    [SerializeField]
    public AudioClip toggleSettings;
    [SerializeField]
    public AudioClip toggleSettingsFailed;

    //menu sounds
    [SerializeField]
    public AudioClip openMenu;
    [SerializeField]
    public AudioClip closeMenu;
    [SerializeField]
    public AudioClip navigateMenu;

    private Dictionary<string, AudioClip> clipDict;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //this.enterPressed = Resources.Load<AudioClip>("/Audio/MRTK_ButtonPress");
        /*this.enterFailed = MRTK.Load<AudioClip>("SDK/StandardAssets/Audio/MRTK_ButtonPress.wav");
        this.backPressed = MRTK.Load<AudioClip>("SDK/StandardAssets/Audio/MRTK_ButtonPress.wav");
        this.toggleMode = MRTK.Load<AudioClip>("SDK/StandardAssets/Audio/MRTK_ButtonPress.wav");


        this.toggleSensitivity = MRTK.Load<AudioClip>("SDK/StandardAssets/Audio/MRTK_Slider_Pass_Notch.wav");
        this.openMenu = MRTK.Load<AudioClip>("SDK/StandardAssets/Audio/MRTK_Select_Main.wav");
        this.closeMenu = MRTK.Load<AudioClip>("SDK/StandardAssets/Audio/MRTK_Select_Main.wav");
        this.navigateMenu = MRTK.Load<AudioClip>("SDK/StandardAssets/Audio/MRTK_Select_Main.wav");
        */
        this.clipDict = new Dictionary<string, AudioClip> { { "enter", enterPressed }, {"target placed", targetPlaced }, {"enter failed", enterFailed },
            { "back", backPressed }, {"toggle settings", toggleSettings }, {"toggle settings failed", toggleSettingsFailed },
            { "open menu", openMenu }, {"close menu", closeMenu },{ "navigate menu", navigateMenu} };
    }

    public void PlayClip(string clip)
    {
        Debug.Log(clipDict[clip]);
        audioSource.clip = clipDict[clip];
        audioSource.Play();
    }
}
