using HOGUS.Scripts.DP;
using UnityEngine;

namespace HOGUS.Scripts.Manager
{
    // 효과음
    public enum SFX
    {
        ATTACK,
        BUTTON,
    }
    // 배경음
    public enum BGM
    {
        MAINMENU,
    }

    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField]
        private AudioSource sfxSource = null;

        [SerializeField]
        private AudioSource bgmSource = null;

        [SerializeField]
        private AudioClip[] sfxList = { };

        [SerializeField]
        private AudioClip[] bgmList = { };

        protected AudioManager() { }

        private void Start()
        {
            this.sfxSource.loop = false;
            this.bgmSource.loop = true;
        }

        public void PlayBGM(BGM bgmType)
        {
            var type = (int)bgmType;
            Instance.bgmSource.clip = Instance.bgmList[type];
            Instance.bgmSource.Play();
        }

        public void PlaySFX(SFX sfxType)
        {
            var sfxIndex = (int)sfxType;
            Instance.sfxSource.PlayOneShot(Instance.sfxList[sfxIndex], Instance.sfxSource.volume);
        }

        public void StopBGM()
        {
            Instance.bgmSource.Stop();
            Debug.Log("StopBGM");
        }

        public void StopSFX()
        {
            Instance.sfxSource.Stop();
        }

        public void PauseBGM()
        {
            Instance.bgmSource.Pause();
        }

        public void ReplayBGM()
        {
            Instance.bgmSource.Play();
        }

        public void SetMuteSFX(bool mute)
        {
            Instance.sfxSource.mute = mute;
        }

        public void SetMuteBGM(bool mute)
        {
            Instance.bgmSource.mute = mute;
        }

        public void SetVolumeSFX(float value)
        {
            Instance.sfxSource.volume = value;
        }

        public void SetVolumeBGM(float value)
        {
            Instance.bgmSource.volume = value;
        }

        public float GetVolumeSFX()
        {
            return Instance.sfxSource.volume;
        }

        public float GetVolumeBGM()
        {
            return Instance.bgmSource.volume;
        }

        public bool GetMuteSFX()
        {
            return Instance.sfxSource.mute;
        }

        public bool GetMuteBGM()
        {
            return Instance.bgmSource.mute;
        }
    }
}