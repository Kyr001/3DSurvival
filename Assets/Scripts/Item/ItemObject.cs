using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;
    private PlayerCondition condition;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str ;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data; // 아이템 데이터를 전달한다
        //CharacterManager.Instance.Player.addItem?.Invoke(); // addItem 이벤트를 발생시킨다

        condition = CharacterManager.Instance.Player.condition;

        if (data.type == ItemType.Consumable)
        {
            Debug.Log(data); //Interactable Object의 데이터
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
        Destroy(gameObject);
    }
}
