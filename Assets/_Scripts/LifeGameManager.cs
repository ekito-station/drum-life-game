using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGameManager : MonoBehaviour
{
    public float waitSec;

    public string colorPlayed;
    public string colorNotPlayed;

    public GameObject playButton;
    public GameObject stopButton;
    public GameObject panelOverBeats;
    public GameObject explanation1;
    public GameObject explanation2;

    public BeatController[] beats;

    public AudioClip clave;
    public AudioClip ride_in;
    public AudioClip crash_right;
    public AudioClip hi_tom;
    public AudioClip low_tom;
    public AudioClip hi_hat_open;
    public AudioClip hi_hat_closed;
    public AudioClip snare;
    public AudioClip kick;

    public enum SOUND_TYPE
    {
        CLAVE,
        RIDE_IN,
        CRASH_RIGHT,
        HI_TOM,
        LOW_TOM,
        HI_HAT_OPEN,
        HI_HAT_CLOSED,
        SNARE,
        KICK
    }

    private Coroutine playBeats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayButtonClicked()
    {
        playBeats = StartCoroutine("PlayBeats");
        panelOverBeats.SetActive(true);
        explanation1.SetActive(false);
        explanation2.SetActive(false);
        stopButton.SetActive(true);
        playButton.SetActive(false);
    }
    public void OnStopButonClicked()
    {
        StopCoroutine(playBeats);
        panelOverBeats.SetActive(false);
        explanation1.SetActive(true);
        explanation2.SetActive(true);
        playButton.SetActive(true);
        stopButton.SetActive(false);
    }

    private IEnumerator PlayBeats()
    {
        while (true)
        {
            foreach (BeatController beat in beats)
            {
                beat.PlayNotes();
                yield return new WaitForSeconds(waitSec);
            }
            NextLife();
        }
    }

    public void NextLife()
    {
        CountLife();
        ChangeLife();
    }

    private void CountLife()
    {
        for (int x = 0; x < beats.Length; x++)
        {
            if (x == 0)
            {
                NoteController[] _notes = beats[x].notes;
                NoteController[] _nextNotes = beats[x+1].notes;

                for (int y = 0; y < _notes.Length; y++)
                {
                    // ???????????????????????????
                    if (_nextNotes[y].isPlayed) _notes[y].count++; 

                    if (y != 0)
                    { 
                        if (_notes[y-1].isPlayed) _notes[y].count++;  
                        if (_nextNotes[y-1].isPlayed) _notes[y].count++;                                                                    
                    }
                    if (y != _notes.Length - 1)
                    { 
                        if (_notes[y+1].isPlayed) _notes[y].count++;
                        if (_nextNotes[y+1].isPlayed) _notes[y].count++;                                                                      
                    }                                       
                }                
            }
            else if (x == beats.Length - 1)
            {
                NoteController[] _prevNotes = beats[x-1].notes;
                NoteController[] _notes = beats[x].notes;

                for (int y = 0; y < _notes.Length; y++)
                {
                    // ???????????????????????????
                    if (_prevNotes[y].isPlayed) _notes[y].count++;
                    
                    if (y != 0)
                    {
                        if (_prevNotes[y-1].isPlayed) _notes[y].count++;  
                        if (_notes[y-1].isPlayed) _notes[y].count++;                                                                    
                    }
                    if (y != _notes.Length - 1)
                    {
                        if (_prevNotes[y+1].isPlayed) _notes[y].count++;  
                        if (_notes[y+1].isPlayed) _notes[y].count++;                                                                     
                    }                                       
                }                
            }
            else
            {
                NoteController[] _prevNotes = beats[x-1].notes;
                NoteController[] _notes = beats[x].notes;
                NoteController[] _nextNotes = beats[x+1].notes;

                for (int y = 0; y < _notes.Length; y++)
                {
                    // ???????????????????????????
                    if (_prevNotes[y].isPlayed) _notes[y].count++;
                    if (_nextNotes[y].isPlayed) _notes[y].count++; 

                    if (y != 0)
                    {
                        if (_prevNotes[y-1].isPlayed) _notes[y].count++;  
                        if (_notes[y-1].isPlayed) _notes[y].count++;  
                        if (_nextNotes[y-1].isPlayed) _notes[y].count++;                                                                    
                    }
                    if (y != _notes.Length - 1)
                    {
                        if (_prevNotes[y+1].isPlayed) _notes[y].count++;  
                        if (_notes[y+1].isPlayed) _notes[y].count++;
                        if (_nextNotes[y+1].isPlayed) _notes[y].count++;                                                                      
                    }                                       
                }
            }
        }
    }

    private void ChangeLife()
    {
        // foreach (BeatController _beat in beats)
        // {
        //     foreach (NoteController _note in _beat.notes)
        //     {
        //         if (!_note.isPlayed && _note.count == 3)
        //         {
        //             _note.isPlayed =true;
        //             _note.noteRenderer.color = Color.white;
        //         }
        //         else if (_note.isPlayed && _note.count !=2 && _note.count != 3)
        //         {
        //             _note.isPlayed = false;
        //             _note.noteRenderer.color = Color.black;
        //         }
        //     }
        // }

        for (int i = 0; i < beats.Length; i++)
        {
            NoteController[] _notes = beats[i].notes;

            for (int j = 0; j < _notes.Length; j++)
            {
                NoteController _note = _notes[j];

                if (!_note.isPlayed && _note.count == 3)
                {
                    _note.isPlayed =true;
                    _note.image.color = Color.white;
                    // _note.noteRenderer.color = Color.white;
                }
                else if (_note.isPlayed && _note.count !=2 && _note.count != 3)
                {
                    _note.isPlayed = false;
                    _note.image.color = Color.black;
                    // _note.noteRenderer.color = Color.black;
                }

                Debug.Log("Beat" + i + " Note" + j + ": " + _note.count + ", " + _note.isPlayed);
                _note.count = 0;
            }
        }
    }
}
