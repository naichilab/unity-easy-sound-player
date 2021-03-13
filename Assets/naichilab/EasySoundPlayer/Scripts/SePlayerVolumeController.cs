using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace naichilab.EasySoundPlayer.Scripts
{
    public class SePlayerVolumeController : MonoBehaviour
    {
        [SerializeField] private Slider Slider;
        [SerializeField] private bool PlaySeOnVolumeChanged = true;
        public FloatReactiveProperty CurrentVolume = new FloatReactiveProperty(0.5f);

        void Start()
        {
            var sePlayer = SePlayer.Instance;

            if (sePlayer == null)
            {
                Debug.LogWarning("SePlayerが見つかりませんでした。");
                return;
            }

            CurrentVolume
                .Subscribe(newVolume => sePlayer.Volume = newVolume)
                .AddTo(this);

            if (Slider == null) return;

            CurrentVolume
                .Subscribe(newVolume => Slider.value = Mathf.Clamp01(newVolume))
                .AddTo(this);

            Slider.OnValueChangedAsObservable()
                .Subscribe(newVolume => CurrentVolume.Value = newVolume)
                .AddTo(this);

            Slider.OnValueChangedAsObservable()
                .Skip(1)
                .Where(_ => PlaySeOnVolumeChanged)
                .Throttle(TimeSpan.FromSeconds(.3f))
                .Subscribe(newVolume => sePlayer.Play(0))
                .AddTo(this);
            Slider.value = sePlayer.Volume;
        }
    }
}