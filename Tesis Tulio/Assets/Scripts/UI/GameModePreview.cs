using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameModePreview : MonoBehaviour, IPointerEnterHandler
{
    [Header("Imagen global que cambiara")]
    [SerializeField] private Image previewImage;

    [Header("Imagen que se mostrara cuando este boton este seleccionado")]
    [SerializeField] private Sprite hoverSprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (previewImage != null && hoverSprite != null)
            previewImage.sprite = hoverSprite;
    }
}
