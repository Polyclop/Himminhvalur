using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class caughtJauge : MonoBehaviour
{
    public bool seen = false;
    [Range(0, 1)]
    public float baseIncreaseRate = 0.35f;
    [Range(0, 1)]
    public float baseDecreaseRate = 0.7f;
    [Range(0, 1)]
    public float slowIncreaseRate = 0.3f;
    [Range(0, 1)]
    public float slowDecreaseRate = 0.7f;
    float increaseRate, decreaseRate;
    Slider slider;

    public Image _aiguille;
    public RectTransform barometreContainer;

    public float _healthValue = 0;
    [Range(0, 2)]
    public float baseValueOffset = 0;

    //Death Delegate
    public delegate void OnDeathDelegate();
    public static OnDeathDelegate deathDelegate;


    bool isDead;



    public void OnDeath()
    {
        deathDelegate();
    }

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        GameEvents.current.onBeingSeen += OnGettingCaughtSlider;
        GameEvents.current.onDying += InitBackSlider;
        GameEvents.current.onChangingRoom += AdaptJaugeSpeed;
        increaseRate = baseIncreaseRate;
        decreaseRate = baseDecreaseRate;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (seen)
            {
                if (_healthValue < 1)
                    _healthValue += increaseRate * Time.deltaTime;
                else
                    HandleDeath();
            }
            else
            {
                if (_healthValue > 0)
                {
                    _healthValue -= decreaseRate * Time.deltaTime;
                }
            }
            HealthChange(_healthValue);
        }
        
    }

    void OnGettingCaughtSlider(bool caught)
    {
        seen = caught && !isDead;
    }

    void HandleDeath()
    {
        seen = false;
        isDead = true;

        OnDeath();
        GameEvents.current.BlockPlayerMove(false);
    }

    void InitBackSlider(float currentRoom, float deathDuration)
    {
        _healthValue = 0;
        HealthChange(_healthValue);
        seen = false;
        isDead = false;
    }

    void HealthChange(float healthValue)
    {
        float amount = ((1 + baseValueOffset + healthValue) * 260.0f / 360);
        _aiguille.fillAmount = amount;
        float buttonAngle = amount * 360;
        barometreContainer.localEulerAngles = new Vector3(0, 0, -buttonAngle);
    }

    private void AdaptJaugeSpeed(float room)
    {
        switch (room)
        {
            case 4:
                increaseRate = slowIncreaseRate;
                decreaseRate = slowDecreaseRate;
                break;
            default:
                increaseRate = baseIncreaseRate;
                decreaseRate = baseDecreaseRate;
                break;
        }
    }
}
