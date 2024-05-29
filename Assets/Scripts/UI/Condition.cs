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
        // ui ������Ʈ
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue; 
    }

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue); // 100 �ʰ����� �ʰ� �Ѵ�
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0); // 0���� �۾����� �ʰ� �Ѵ�
    }
}
