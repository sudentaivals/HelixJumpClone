using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : SingletonInstance<ScoreManager>
{
    [SerializeField] int _basePlatformScore;
    [SerializeField] int _comboBonus;
    [SerializeField] AudioClip _increaseComboSfx;
    [SerializeField] [Range(0f, 1f)] float _increaseComboSfxVolume;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] ComboTextAnimation _comboText;

    private int _currentScore;
    private int _currentComboCount;

    private float GetPitch => Mathf.Clamp(1f + _currentComboCount * 0.05f, 1f, 1.5f);

    private void Start()
    {
        BreakCombo();
        CurrentScore = 0;
    }

    private int CurrentComboCounter
    {
        get
        {
            return _currentComboCount;
        }
        set
        {
            _currentComboCount = value;
            _comboText.SetNewComboText(_currentComboCount);
        }
    }

    private int CurrentScore
    {
        get
        {
            return _currentScore;
        }
        set
        {
            _currentScore = value;
            _scoreText.text = _currentScore.ToString();
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe(EventBusEvent.PlatformDestroyed, OnPlatformDestroyed);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventBusEvent.PlatformDestroyed, OnPlatformDestroyed);
    }
    public void BreakCombo()
    {
        CurrentComboCounter = 0;
    }

    private void OnPlatformDestroyed(UnityEngine.Object sender, EventArgs args)
    {
        AddComboPoint();
        AddScore();
    }

    public void AddScore()
    {
        var scoreToAdd = _basePlatformScore + _comboBonus * CurrentComboCounter;
        CurrentScore += scoreToAdd;
    }

    public void AddComboPoint()
    {
        CurrentComboCounter++;
        EventBus.Publish(EventBusEvent.PlaySound, this, new PlaySoundEventArgs(_increaseComboSfxVolume, _increaseComboSfx, GetPitch));
    }

}
