using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveSheep : MonoBehaviour, IPointerClickHandler
{
    private Transform player;
    [SerializeField] private float speedAnswear = 10;
    private Vector3 sheepPosition;
    [SerializeField] AudioSource baranSound;
    private SpriteRenderer baran;

    public void OnPointerClick(PointerEventData eventData)
    {
        baranSound.Play();
        Debug.Log("Клик на барана");
    }

    private void Awake()
    {
        if (!player)
        {
            player = FindObjectOfType<Hero>().transform;
        }
        baran = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        sheepPosition = player.position;
        sheepPosition.y = 1;
        transform.position = Vector3.Lerp(transform.position, sheepPosition, Time.deltaTime * speedAnswear);
        
    }
}
