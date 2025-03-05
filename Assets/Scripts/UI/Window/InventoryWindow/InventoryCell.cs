using System;
using Services.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Window.InventoryWindow
{
    public class InventoryCell : MonoBehaviour, IPointerClickHandler
    {
        private event Action Clicked;
        
        [SerializeField] private Image _back;
        [SerializeField] private Color _lockColor;
        [SerializeField] private Color _unlockColor;
        [SerializeField] private TMP_Text _textCount;
        
        public void Setup(bool isLock)
        {
            _back.color = isLock ? _lockColor : _unlockColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }
        
        
    }
}