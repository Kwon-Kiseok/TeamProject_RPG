using HOGUS.Scripts.DP;
using UnityEngine;

namespace HOGUS.Scripts.Manager
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField]
        private AudioSource[] sfxSource = null;

        [SerializeField]
        private AudioSource bgmSource = null;

        [SerializeField]
        private Sound[] sfxList = { };

        [SerializeField]
        private Sound[] bgmList = { };

        public string[] playSoundNames;

        protected AudioManager() { }

        private void Start()
        {
            playSoundNames = new string[sfxSource.Length];
        }

        public void PlayBGM(string _name)
        {
            //var type = (int)bgmType;
            //Instance.bgmSource.clip = Instance.bgmList[type];
            //Instance.bgmSource.Play();
        }

        public void PlaySFX(string _name)
        {
            for(int i = 0; i < sfxList.Length; i++)
            {
                if(_name == sfxList[i].name)
                {
                    for(int j = 0; j < sfxSource.Length; j++)
                    {
                        if(!sfxSource[j].isPlaying)
                        {
                            playSoundNames[j] = sfxList[i].name;
                            sfxSource[j].clip = sfxList[i].clip;
                            sfxSource[j].Play();
                            return;
                        }
                    }
                    return;
                }
            }
        }

        public void StopBGM()
        {
            Instance.bgmSource.Stop();
            Debug.Log("StopBGM");
        }

        public void StopAllSFX()
        {
            for(int i = 0; i < sfxSource.Length; i++)
            {
                sfxSource[i].Stop();
            }
        }

        public void StopSFX(string _name)
        {
            for(int i = 0; i < sfxSource.Length; i++)
            {
                if(playSoundNames[i] == _name)
                {
                    sfxSource[i].Stop();
                    break;
                }
            }
        }

        public void PauseBGM()
        {
            Instance.bgmSource.Pause();
        }

    }
}