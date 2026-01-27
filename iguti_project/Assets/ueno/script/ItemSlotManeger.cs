using System.Collections.Generic;
using UnityEngine;

public class ItemSlotManager : MonoBehaviour
{
    public static ItemSlotManager Instance { get; private set; }

    [Header("管理するスロット（複数対応）")]
    [SerializeField] private List<ItemSlot1> itemSlots = new List<ItemSlot1>();

    private List<string> storedItems = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// アイテムをスロットに登録（実体を保持）
    /// </summary>
    public void AddItem(string pictogramName, GameObject pictogramObj)
    {
        // すでに持っていなければ追加
        if (!storedItems.Contains(pictogramName))
            storedItems.Add(pictogramName);

        foreach (var slot in itemSlots)
        {
            if (!slot.HasItem)
            {
                slot.SetExistingItem(pictogramObj, pictogramName);
                Debug.Log($"ItemSlotManager: {pictogramName} をスロットに登録");
                return;
            }
        }

        Debug.LogWarning("空きスロットがありません！");
    }
}
