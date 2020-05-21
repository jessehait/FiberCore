using RHGameCore.AudioManagement;
using UnityEngine;

namespace RHGameCore.Api
{
    public interface IAudioManager
    {
        /// <summary>
        /// Plays clip at main listener with default config
        /// </summary>
        /// <param name="audioClip">Clip</param>
        /// <returns>Source</returns>
        AudioSource Play(AudioClip audioClip);

        /// <summary>
        /// Plays clip at main listener with your config
        /// </summary>
        /// <param name="audioClip">Clip</param>
        /// <param name="playConfig">Config</param>
        /// <returns>Source</returns>
        AudioSource Play(AudioClip audioClip, AudioClipConfig playConfig);

        /// <summary>
        /// Plays clip at main specific position with yor config
        /// </summary>
        /// <param name="audioClip">Clip</param>
        /// <param name="position">Position</param>
        /// <param name="playConfig">Config</param>
        /// <returns>Source</returns>
        AudioSource Play(AudioClip audioClip, Vector3 position, AudioClipConfig playConfig);

        /// <summary>
        /// Plays clip via existing source with your config
        /// </summary>
        /// <param name="audioClip">Clip</param>
        /// <param name="source">Source</param>
        /// <param name="playConfig">Config</param>
        /// <returns>Source</returns>
        AudioSource Play(AudioClip audioClip, AudioSource source, AudioClipConfig playConfig);

        /// <summary>
        /// Register main listener
        /// </summary>
        /// <param name="audioListener">Listener</param>
        void SetListener(AudioListener audioListener);
    }
}