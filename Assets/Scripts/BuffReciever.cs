using System;
using System.Collections.Generic;
using UnityEngine;


public class BuffReciever : MonoBehaviour
{
    public Action OnBuffChanged; //Использует "Using System".
    private List<Buff> buffs; //Список "бафов".


    public List<Buff> Buffs
    {
        get { return buffs; }
    }

    private void Start()
    {
        GameManager.Instance.buffRecieverContainer.Add(gameObject, this);
        buffs = new List<Buff>();
    }

    public void AddBuff(Buff buff) //Вызов в "BuffEmitter", добавляет бафф.
    {
        if (!buffs.Contains(buff)) //Проверка на наличие баффа в текущем листе, при отсутствие - добавляет.
        {
            buffs.Add(buff);
        }

        if(OnBuffChanged != null) //Проверка делегата на подписанные методы, при наличие - вызывает.
        {
            OnBuffChanged();
        }
    }

    public void RemoveBuff(Buff buff) //Вызов в "BuffEmitter", удаляет бафф.
    {
        if (buffs.Contains(buff))
        {
            buffs.Remove(buff);
        }        
    }
}
