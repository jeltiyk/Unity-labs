using System;
using System.Collections.Generic;
using Items.ScriptableObject;
using TMPro;
using UnityEngine;


//temporary solution
public class PlayerStatController : MonoBehaviour
{
    private int _attack;
    private int _defence;
    private int _criticalHitChance;
    private int _criticalHitPower;

    [Header("UI elements")]
    [SerializeField] private TMP_Text playerNicknameText;
    [SerializeField] private TMP_Text playerLvlText;
    [SerializeField] private TMP_Text playerAttackText;
    [SerializeField] private TMP_Text playerDefenceText;
    [SerializeField] private TMP_Text playerCriticalHitChanceText;
    [SerializeField] private TMP_Text playerCriticalHitPowerText;

    [Header("Start stat")] 
    [SerializeField] private string playerNickname;
    [SerializeField] private int playerLvl; 
    [SerializeField] private List<Stat> primaryStats;

    public string PlayerNickname => playerNickname;
    public int Lvl => playerLvl;
    public int Attack => _attack;
    public int Defence => _defence;
    public int CriticalHitChance => _criticalHitChance;
    public int CriticalHitPower => _criticalHitPower;

    private void Start()
    {
        if(playerNickname.Length > 0)
            playerNicknameText.SetText(playerNickname);
        
        if(playerLvl != 0) 
            playerLvlText.SetText(playerLvl.ToString());      
       
        if (primaryStats == null || primaryStats.Count == 0)
        {
            _criticalHitPower = 1;
            playerCriticalHitPowerText.SetText( "x1");
            return;
        }
        
        foreach (var stat in primaryStats)
        {
            switch (stat.StatType)
            {
                case StatType.Attack:
                    _attack = stat.Amount;
                    playerAttackText.SetText(_attack.ToString());
                    break;
                case StatType.Defence:
                    _defence = stat.Amount;
                    playerDefenceText.SetText(_defence.ToString());
                    break;
                case StatType.CriticalHitChance:
                    _criticalHitChance = stat.Amount;
                    playerCriticalHitChanceText.SetText(_criticalHitChance.ToString());
                    break;
                case StatType.CriticalHitPower:
                    _criticalHitPower = stat.Amount;
                    playerCriticalHitPowerText.SetText( "x" + _criticalHitPower);
                    break;
            }
        }
    }

    public void SetNickname(string nickname)
    {
        playerNicknameText.SetText(nickname);
        playerNickname = nickname;
    }

    public void SetLvl(string lvl)
    {
        playerLvlText.SetText(lvl);
        playerLvl = Convert.ToInt32(lvl);
    }
    
    private bool SetStat(StatType statType, int value)
    {
        if (value < 0) return false;
        
        switch (statType)
        {
            case StatType.Attack:
                _attack = value;
                break;
            case StatType.Defence:
                _defence = value;
                break;
            case StatType.CriticalHitChance:
                _criticalHitChance = value;
                break;
            case StatType.CriticalHitPower:
                _criticalHitPower = value;
                break;
        }

        return true;
    }

    public void ChangeStats(Equipment equipment, bool increase = true)
    {
        Stat[] stats = new Stat[equipment.PrimaryStats.Length + equipment.AdditionalStats.Length];
        
        equipment.PrimaryStats.CopyTo(stats, 0);
        equipment.AdditionalStats.CopyTo(stats, equipment.PrimaryStats.Length);

        foreach (var stat in stats)
        {
            if (increase)
            {
                if(!IncreaseStat(stat.StatType, stat.Amount)) return; // 'in future' provide decrease stat on false result
            }
            else
            {
                if(!DecreaseStat(stat.StatType, stat.Amount)) return; // 'in future' provide increase stat on false result
            }
        }
    }
    
    private bool IncreaseStat(StatType statType, int value)
    {
        if(value < 0) return false;
        
        switch (statType)
        {
            case StatType.Attack:
                _attack += value;
                playerAttackText.SetText(_attack.ToString());
                break;
            case StatType.Defence:
                _defence += value;
                playerDefenceText.SetText(_defence.ToString());
                break;
            case StatType.CriticalHitChance:
                _criticalHitChance += value;
                playerCriticalHitChanceText.SetText(_criticalHitChance.ToString());
                break;
            case StatType.CriticalHitPower:
                _criticalHitPower += value;
                playerCriticalHitPowerText.SetText( "x" + _criticalHitPower);
                break;
        }
        
        return true;
    }

    private bool DecreaseStat(StatType statType, int value)
    {
        if(value < 0) return false;
        
        switch (statType)
        {
            case StatType.Attack:
                _attack -= value;
                playerAttackText.SetText(_attack.ToString());
                break;
            case StatType.Defence:
                _defence -= value;
                playerDefenceText.SetText(_defence.ToString());
                break;
            case StatType.CriticalHitChance:
                _criticalHitChance -= value;
                playerCriticalHitChanceText.SetText(_criticalHitChance.ToString());
                break;
            case StatType.CriticalHitPower:
                _criticalHitPower -= value;
                playerCriticalHitPowerText.SetText( "x" + _criticalHitPower);
                break;
        }

        return true;
    }
}
