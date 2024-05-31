using System.Collections;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;
    private PlayerCondition condition;

    private PlayerController controller;
    private float orgMoveSpeed;

    [SerializeField]
    public float maintainEffectTime = 15.0f;
    public float itemEffect = 0.3f;

    private MeshRenderer itemRenderer;
    private Light itemLight;
    private bool isEffeted = false;

    public void Awake()
    {
        itemRenderer = GetComponentInChildren<MeshRenderer>();
        itemLight = GetComponentInChildren<Light>();
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data; // ������ �����͸� �����Ѵ�
        //CharacterManager.Instance.Player.addItem?.Invoke(); // addItem �̺�Ʈ�� �߻���Ų��

        condition = CharacterManager.Instance.Player.condition;
        controller = CharacterManager.Instance.Player.controller;
        orgMoveSpeed = controller.moveSpeed;

        if (data.type == ItemType.Consumable && !isEffeted)
        {
            Consume();
            StartCoroutine(ApplyItemEffect());
        }
        else if (data.type == ItemType.Consumable && isEffeted)
            Debug.Log("Item Already Used");
        else
            Destroy(gameObject);
    }

    public string GetInteractPrompt()
    {
        string str;

        if (!itemRenderer.enabled)
            str = "[E] ��ȣ�ۿ�";
        else
            str = $"{data.displayName}\n{data.description}";

        return str;
    }

    void Consume()
    {
        for (int i = 0; i < data.consumables.Length; i++)
        {
            switch (data.consumables[i].type)
            {
                case ConsumableType.Health:
                    condition.Eat(data.consumables[i].value); break;
                case ConsumableType.Hunger:
                    condition.Eat(data.consumables[i].value); break;
            }

        }
    }

    public IEnumerator ApplyItemEffect()
    {
        isEffeted = true;
        itemRenderer.enabled = false;
        itemLight.enabled = false;

        Debug.Log("Item Used");
        controller.moveSpeed = controller.moveSpeed * itemEffect;

        yield return new WaitForSeconds(maintainEffectTime);

        controller.moveSpeed = orgMoveSpeed;
        Destroy(gameObject);
        Debug.Log("Item Effect Finished");
        isEffeted = false;
    }
}
