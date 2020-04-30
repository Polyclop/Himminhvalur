using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class caughtJauge : MonoBehaviour
{
    public bool seen = false;
    [Range(0, 1)]
    public float increaseRate = 0.2f;
    [Range(0, 1)]
    public float decreaseRate = 0.4f;

    Slider slider;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (seen)
            {
                if (slider.value < 1)
                    slider.value += increaseRate * Time.deltaTime;
                else
                    HandleDeath();
            }
            else
            {
                if (slider.value > 0)
                {
                    slider.value -= decreaseRate * Time.deltaTime;
                }
            }
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
        slider.value = 0;
        seen = false;
        isDead = false;
    }
}
