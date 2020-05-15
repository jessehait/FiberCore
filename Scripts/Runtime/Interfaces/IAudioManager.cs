using RHGameCore.AudioManagement;
using UnityEngine;

namespace RHGameCore.Api
{
    public interface IAudioManager
    {
        AudioSource Play(AudioClip audioClip);
        AudioSource Play(AudioClip audioClip, AudioClipConfig playConfig);
        AudioSource Play(AudioClip audioClip, Vector3 position, AudioClipConfig playConfig);
        AudioSource Play(AudioClip audioClip, AudioSource source, AudioClipConfig playConfig);
        void SetListener(AudioListener audioListener);
    }
}