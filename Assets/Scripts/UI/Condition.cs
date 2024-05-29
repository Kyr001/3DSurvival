using UnityEngine;
using UnityEngine.UI;


public class Condition : MonoBehaviour
{

    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;


    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        // ui 업데이트
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue; 
    }

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue); // 100 초과되지 않게 한다
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0); // 0보다 작아지지 않게 한다
    }
}
