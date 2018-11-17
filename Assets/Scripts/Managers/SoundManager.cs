using UnityEngine;
using System.Collections;

public class SoundManager : SingeltonBase<SoundManager>
{
    /* -------------- 	Game specific sounds --------------- */
    public AudioClip levelUpSound;
    // Done
    public AudioClip buttonClickSound;
    // with menus
    public AudioClip menuBGSound;
    // Done
    public AudioClip gamePlayBGSound;
    // Done
    public AudioClip levelCompleteSound;
    // Done
    public AudioClip levelFailSound;
    // Done

    /* -------------- 	Nature specific sounds --------------- */

    /* -------------- 	Player specific sounds --------------- */
    public AudioClip playerRunSound;
    public AudioClip playerAttackSound;
   
    /* -------------- 	Humans  --------------- */
 
    /* -------------- 	Collectibles  --------------- */
	
    /* -------------- 	Breakable Items  --------------- */

    /* -------------- 	Non-Living Objects  --------------- */

    /* -------------- 	Animals --------------- */

    //////////////////////////////////////////////////////////
    /* -------------- 	Others  --------------- */
    public AudioClip rifleGunShot;
	public AudioClip Alien_FireBall;
    public AudioClip Player_FireBall;
	public AudioClip hitSound;

    /* Audio Source */
    public AudioSource gamePlayEffectsSource;
    public AudioSource BackgroundSoundSource;
    public AudioSource CarEnvSound;
    public AudioSource runSoundSource;
    public AudioSource healthSoundSource;
	


    public bool isDualSound = false;
    private bool isGamePlaySound = false;

    void Start()
    {
		
        DontDestroyOnLoad(this);
        if (isDualSound)
        {
            this.GetComponent<AudioSource>().clip = menuBGSound;
            isGamePlaySound = false;
        }
        else
        {
            this.GetComponent<AudioSource>().clip = gamePlayBGSound;
            isGamePlaySound = true;
        }
        if (!UserPrefs.isSound)
        {
            this.GetComponent<AudioSource>().mute = true;
            gamePlayEffectsSource.mute = true;
            BackgroundSoundSource.mute = true;
            CarEnvSound.mute = true;
            runSoundSource.mute = true;
            healthSoundSource.mute = true;
        }
        if (!this.GetComponent<AudioSource>().isPlaying)
        {
            this.GetComponent<AudioSource>().Play();
        }		
    }

  
	#region PlaySound Function
    public void PlaySound(GameManager.SoundState state)
    {
        if (!UserPrefs.isSound)
            return;
        
        switch (state)
        {
           
            
            case GameManager.SoundState.BUTTONCLICKSOUND:
                this.ButtonClickSound();
                break;
            case GameManager.SoundState.MUTESOUND:
                this.MuteSound();
                break;
            case GameManager.SoundState.UNMUTESOUND:
                this.UnMuteSound();
                break;

            case GameManager.SoundState.MUTEMUSIC:
                this.MuteMusic();
                break;
            case GameManager.SoundState.UNMUTEMUSIC:
                this.UnMuteMusic();
                break;

            case GameManager.SoundState.LEVELUPSOUND:
                gamePlayEffectsSource.PlayOneShot(levelUpSound);
                break;

            case GameManager.SoundState.PLAYERRUNSOUND:
                if (!gamePlayEffectsSource.isPlaying)
                    gamePlayEffectsSource.PlayOneShot(playerRunSound);
                break;
	
            case GameManager.SoundState.PLAYERATTACKSOUND:
                gamePlayEffectsSource.PlayOneShot(playerAttackSound);
                break;
   			case GameManager.SoundState.HITSOUND:
				gamePlayEffectsSource.PlayOneShot(hitSound);
				break;
            case GameManager.SoundState.LEVELCOMPLETIONSOUND:
                gamePlayEffectsSource.PlayOneShot(levelCompleteSound);
                break;
            case GameManager.SoundState.LEVELFAIL:
                gamePlayEffectsSource.PlayOneShot(levelFailSound);
                break;
            case GameManager.SoundState.GUNSHOT:
                gamePlayEffectsSource.PlayOneShot(rifleGunShot);
                break;
          
            case GameManager.SoundState.PLAYER_FIREBALL:
                gamePlayEffectsSource.PlayOneShot(Player_FireBall);
                break;
            case GameManager.SoundState.ENEMY_FIREBALL:
                gamePlayEffectsSource.PlayOneShot(Alien_FireBall);
                break;
        }
    }
	#endregion
    public void setGameLevelSounds(GameManager.SoundState soundState)
    {
        if (soundState == GameManager.SoundState.MENUSOUND)
        {
            this.PlayMenuBGSound();
        }
        else if (soundState == GameManager.SoundState.GAMEPLAYSOUND)
        {
            this.PlayGamePlaySound();
        }
    }

    private void ButtonClickSound()
    {
        gamePlayEffectsSource.PlayOneShot(buttonClickSound);
    }

	#region Mute/UnMute_Handling
    public void MuteSound()
    {
        gamePlayEffectsSource.mute = true;
        runSoundSource.mute = true;
        healthSoundSource.mute = true;
    }

    public void UnMuteSound()
    {
        gamePlayEffectsSource.mute = false;
        runSoundSource.mute = false;
        healthSoundSource.mute = false;
    }

    public void MuteMusic()
    {
        GetComponent<AudioSource>().mute = true;
    }

    public void UnMuteMusic()
    {
        GetComponent<AudioSource>().mute = false;
    }
	#endregion
    private void PlayMenuBGSound()
    {
        this.GetComponent<AudioSource>().clip = menuBGSound;
        this.GetComponent<AudioSource>().Play();
//		this.GetComponent<AudioSource>().volume = 0.1f;
        CarEnvSound.Play();
    }

    private void PlayGamePlaySound()
    {
        this.GetComponent<AudioSource>().clip = gamePlayBGSound;
        this.GetComponent<AudioSource>().Play();
//		this.GetComponent<AudioSource>().volume = 0.4f;
        CarEnvSound.Play();
    }

	public void MuteUnMuteChecker()
	{
		if (!UserPrefs.isSound)
		{
			SoundManager.Instance.MuteSound();
		}
		else
		{
			SoundManager.Instance.UnMuteSound();
		}

		if (!UserPrefs.isMusic)
		{
			SoundManager.Instance.MuteMusic();
		}
		else
		{
			SoundManager.Instance.UnMuteMusic();
		}
	}
}