using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SoundObject
{
    private GameObject gameObject;
    public AudioSource audioSource;
    public string name { get; }
}


/// <summary>
/// 全サウンドを管理するコンポーネント
/// </summary>
public class AudioManagerBehaviour : MonoBehaviour
{
    public GameObject hit;
    public GameObject glassSEObject;
    public GameObject clickSEObject;
    public GameObject clearSEObject;
    public GameObject overSEObject;
    public GameObject mutekiSEObject;

    public AudioSource hitSE;
    public AudioSource glassSE;
    public AudioSource clickSE;
    public AudioSource clearSE;
    public AudioSource overSE;
    public AudioSource mutekiSE;

    // 全サウンドを配列で管理
    [SerializeField]
    private List<SoundObject> soundObjects;

    // Start is called before the first frame update
    void Start()
    {
        hitSE = hit.GetComponent<AudioSource>();
        glassSE = glassSEObject.GetComponent<AudioSource>();
        clickSE = clickSEObject.GetComponent<AudioSource>();
        clearSE = clearSEObject.GetComponent<AudioSource>();
        overSE = overSEObject.GetComponent<AudioSource>();
        mutekiSE = mutekiSEObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 引数の名前をもとに配列を走査し一致しているサウンドを再生する
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(string name)
    {
        foreach (var soundObject in soundObjects)
        {
            if (soundObject.name == name)
            {
                soundObject.audioSource.Play();
            }
        }
    }
}
