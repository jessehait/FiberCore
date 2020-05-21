using RHGameCore.AudioManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHGameCore.Api
{
    public sealed class RHCore_AudioManager : Manager, IAudioManager
    {
        private AudioListener     _listener;

        public void SetListener(AudioListener audioListener)
        {
            _listener = audioListener;
        }

        public AudioSource Play(AudioClip audioClip)
        {
            var config = new AudioClipConfig();
            return Play(audioClip, NewSource(audioClip.name, attachTarget: config.attachTarget), config);
        }

        public AudioSource Play(AudioClip audioClip, AudioClipConfig playConfig)
        {
            return Play(audioClip, NewSource(audioClip.name, attachTarget: playConfig.attachTarget), playConfig);
        }


        public AudioSource Play(AudioClip audioClip, Vector3 position, AudioClipConfig playConfig)
        {
            return Play(audioClip, NewSource(audioClip.name, position, playConfig.attachTarget), playConfig);
        }


        public AudioSource Play(AudioClip audioClip, AudioSource source, AudioClipConfig playConfig)
        {
            source.loop   = playConfig.loop;
            source.volume = playConfig.volume;
            source.clip   = audioClip;

            source.PlayDelayed(playConfig.delay);

            if(playConfig.destroySource)
            {
                RHCore.API.CoroutineHandler.StartCoroutine(DestroySource(source));
            }
      
            return source;
        }


        private AudioSource NewSource(string sourceName, Vector3 position = default, Transform attachTarget = null)
        {
            if(position == default)
            {
                return _listener.gameObject.AddComponent<AudioSource>();
            }
            else
            {
                var obj = new GameObject(sourceName + "_Player");
                obj.transform.position = position;

                if (attachTarget) obj.transform.SetParent(attachTarget);

                return obj.AddComponent<AudioSource>();
            }
        }

        private IEnumerator DestroySource(AudioSource source)
        {
            yield return new WaitUntil(() => !source.isPlaying);

            GameObject.Destroy(source);
        }
    }
}