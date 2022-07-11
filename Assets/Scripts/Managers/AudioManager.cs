using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.DP;

namespace HOGUS.Scripts.Manager
{
    // 효과음
    public enum SFX
    {
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

        private void Awake()
        {
            this.sfxSource.loop = false;
            this.bgmSource.loop = true;
        }

        public static void PlayBGM(BGM bgmType)
        {
            var type = (int)bgmType;
            Instance.bgmSource.clip = Instance.bgmList[type];
            Instance.bgmSource.Play();
        }

        public static void PlaySFX(SFX sfxType)
        {
            var sfxIndex = (int)sfxType;
            Instance.sfxSource.PlayOneShot(Instance.sfxList[sfxIndex], Instance.sfxSource.volume);
        }

        public static void StopBGM()
        {
            Instance.bgmSource.Stop();
        }

        public static void StopSFX()
        {
            Instance.sfxSource.Stop();
        }

        public static void PauseBGM()
        {
            Instance.bgmSource.Pause();
        }

        public static void ReplayBGM()
        {
            Instance.bgmSource.Play();
        }

        public static void SetMuteSFX(bool mute)
        {
            Instance.sfxSource.mute = mute;
        }

        public static void SetMuteBGM(bool mute)
        {
            Instance.bgmSource.mute = mute;
        }

        public static void SetVolumeSFX(float value)
        {
            Instance.sfxSource.volume = value;
        }

        public static void SetVolumeBGM(float value)
        {
            Instance.bgmSource.volume = value;
        }

        public static float GetVolumeSFX()
        {
            return Instance.sfxSource.volume;
        }

        public static float GetVolumeBGM()
        {
            return Instance.bgmSource.volume;
        }

        public static bool GetMuteSFX()
        {
            return Instance.sfxSource.mute;
        }

        public static bool GetMuteBGM()
        {
            return Instance.bgmSource.mute;
        }
    }
}