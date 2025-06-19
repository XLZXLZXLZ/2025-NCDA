using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource BgmAudio;
    [SerializeField] AudioSource SfxAudio;

    public AudioClip bgm;
    public AudioClip wood_break;


    private void Start()
    {
        //�ӱ������֣������������
/*        BgmAudio.clip=bgm;
        BgmAudio.Play();*/
    }
    public void PlaySfx(AudioClip clip)
    {
        SfxAudio.PlayOneShot(clip);
    }



}
