using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    // public SpriteRenderer noteRenderer;
    public Image image;

    public LifeGameManager lifeGameManager;
    public AudioSource audioSource;
    public LifeGameManager.SOUND_TYPE soundType;

    [System.NonSerialized]
    public bool isPlayed = false;
    [System.NonSerialized]
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        // noteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCellClicked()
    {
        if (isPlayed)
        {
            image.color = Color.black;
            // noteRenderer.color = Color.black;            
            isPlayed = false;
        }
        else
        {
            image.color = Color.white;
            // noteRenderer.color = Color.white;            
            isPlayed = true;
        }
    }

    public void PlayNote()
    {
        if (isPlayed)
        {
            // 音を鳴らす
            switch (soundType)
            {
                case LifeGameManager.SOUND_TYPE.CLAVE:
                    audioSource.PlayOneShot(lifeGameManager.clave);
                    break;
                case LifeGameManager.SOUND_TYPE.RIDE_IN:
                    audioSource.PlayOneShot(lifeGameManager.ride_in);
                    break;
                case LifeGameManager.SOUND_TYPE.CRASH_RIGHT:
                    audioSource.PlayOneShot(lifeGameManager.crash_right);
                    break;
                case LifeGameManager.SOUND_TYPE.HI_TOM:
                    audioSource.PlayOneShot(lifeGameManager.hi_tom);
                    break;
                case LifeGameManager.SOUND_TYPE.LOW_TOM:
                    audioSource.PlayOneShot(lifeGameManager.low_tom);
                    break;
                case LifeGameManager.SOUND_TYPE.HI_HAT_OPEN:
                    audioSource.PlayOneShot(lifeGameManager.hi_hat_open);
                    break;
                case LifeGameManager.SOUND_TYPE.HI_HAT_CLOSED:
                    audioSource.PlayOneShot(lifeGameManager.hi_hat_closed);
                    break;
                case LifeGameManager.SOUND_TYPE.SNARE:
                    audioSource.PlayOneShot(lifeGameManager.snare);
                    break;
                case LifeGameManager.SOUND_TYPE.KICK:
                    audioSource.PlayOneShot(lifeGameManager.kick);
                    break;
            }
            // 色を変える
            // noteRenderer.color = Color.cyan;
            Color _colorPlayed;
            if (ColorUtility.TryParseHtmlString(lifeGameManager.colorPlayed, out _colorPlayed))
                image.color = _colorPlayed;
                // noteRenderer.color = _colorPlayed;
        }
        else
        {
            // 色を変える
            // noteRenderer.color = Color.blue;
            Color _colorNotPlayed;
            if (ColorUtility.TryParseHtmlString(lifeGameManager.colorNotPlayed, out _colorNotPlayed))
                image.color = _colorNotPlayed;
                // noteRenderer.color = _colorNotPlayed;
        }
        Invoke("ReturnColor", lifeGameManager.waitSec);
    }

    private void ReturnColor()
    {
        if (isPlayed)
        {
            image.color = Color.white;
            //noteRenderer.color = Color.white;
        }
        else
        {
            image.color = Color.black;
            // noteRenderer.color = Color.black;   
        }
    }
}
