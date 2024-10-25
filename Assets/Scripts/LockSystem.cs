using UnityEngine;

public class LockSystem : MonoBehaviour
{
    [SerializeField] private string _requiredKeyID;
    [SerializeField] private bool _isLocked = false;

    public bool IsLocked()
    {
        TryUnlock();
        return _isLocked;
    }

    public void TryUnlock()
    {
        if (_isLocked)
        {
            if (Inventory.Instance.HasKey(_requiredKeyID))
            {
                Unlock();
                _isLocked = false;
            }
            else
            {
                //_soundManager.PlayLockedSound();
                Debug.Log("Нужен ключ для открытия этой двери!");
                _isLocked = true;
            }
        }
        else
        {
            _isLocked = false;
        }
    }
    public void Lock()
    {
        _isLocked = true;
    }

    public void Unlock()
    {
        _isLocked = false;
    }
}
