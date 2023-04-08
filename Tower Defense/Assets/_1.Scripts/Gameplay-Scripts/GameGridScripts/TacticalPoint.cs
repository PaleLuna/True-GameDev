using UnityEngine;
using GlobalUIEvents;

[RequireComponent(typeof(BoxCollider2D))]
public class TacticalPoint : MonoBehaviour, IInteractable
{
    [Header("���������� ��������")]
    [Tooltip("����� ����� ��� ���������")]
    [SerializeField] private Color _selectedColor;

    // ����������
    private SpriteRenderer[] _childsSpriteRenderers;
    private BoxCollider2D _boxCollider2D;

    // ���� ������
    private Building _building;
    
    // ������� � �������
    public Building GetBuilding() => _building;
    public bool isOccupied => _building != null;

    public void SetBuilding(Building newBuilding)
    {
        _building = newBuilding;
        _building.transform.position = (Vector2)transform.position + _boxCollider2D.offset;

        OnDeselected();
    }
    public void DescructBuilding()
    {
        if (_building)
            Destroy(_building.gameObject);
    }

    public void OnSelected()
    {
        if (!_building)
            UIEvents.SendSelectedEmptyBlock(this);
        else
            UIEvents.SendSelectedOccupiedBlock(this);


        _building?.OnSelected();
        ChangeColor(_selectedColor);
    }
    public void OnDeselected()
    {
        UIEvents.SendDeselectBlock();
        _building?.OnDeselected();
        ChangeColor(Color.white);
    }

    private void Start()
    {
        _childsSpriteRenderers = transform.GetComponentsInChildren<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void ChangeColor(Color newColor)
    {
        foreach (SpriteRenderer childSpriteRenderer in _childsSpriteRenderers)
            childSpriteRenderer.color = newColor;
    }
}
