using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [Header("Preferences")]
    public float gold;
    public float emeralds;
    private static Wallet wallet;
    public static Wallet Instance => wallet;
    public Action OnChange;

    private void Awake()
    {
        wallet = this;
    }

    public void Refresh()
    {
        OnChange?.Invoke();
    }

    public void AddGold(float value)
    {
        gold += value;
        OnChange?.Invoke();
    }

    public void SubstractGold(float value)
    {
        gold -= value;
        Data_Manipulator.Instance.Save();
        OnChange?.Invoke();
    }
    public void AddEmeralds(float value)
    {
        emeralds += value;
        OnChange?.Invoke();
    }

    public void SubstractEmeralds(float value)
    {
        emeralds -= value;
        Data_Manipulator.Instance.Save();
        OnChange?.Invoke();
    }
}
