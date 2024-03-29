using Buildings;
using ConfigClasses;
using ModuleClass;
using System;

using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CombatRadiusVisualizer : Module
{
    [Header("��������")]
    [SerializeField] private int _segments;
    [SerializeField][Range(0.01F, 1)] private float _yRadiusCoeff;

    [SerializeField] private float _lineWidth;
    [SerializeField] private Color _lineColor;

    private float radius; /**< Float. ������ ����������. */
    private LineRenderer _lineRenderer; /**< LineRenderer. ���������, �������������� �����. */

    private Building _parentBuilding;

    private void Awake()
    {
        base.Init();

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segments + 1;

        _lineRenderer.startWidth = _lineWidth;
        _lineRenderer.endWidth = _lineWidth;

        _lineRenderer.startColor = _lineColor;
        _lineRenderer.endColor = _lineColor;

        SetParent();

        _parentBuilding.onDeselect.AddListener(DeactiveLine);
    }

    public void ActiveLine() => _lineRenderer.enabled = true;
    public void DeactiveLine() => _lineRenderer.enabled = false;

    private void OnEnable()
    {
        _parentBuilding.AddModule(this);
        _parentBuilding.onSelect.AddListener(ActiveLine);
        _lineRenderer.enabled = _parentBuilding.isSelect;

        UpdateData(_parentBuilding.buildingsConfig);
    }
    private void OnDisable()
    {
        _parentBuilding.onSelect.RemoveListener(ActiveLine);
        _parentBuilding.RemoveModule(this);

        DeactiveLine();
    }

    private void SetParent()
    {
        _parentBuilding = ClassConverter<Building>.Convert(m_moduleParent);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.green; // ������������� ����
            Gizmos.DrawWireSphere(transform.position, radius); // ������ ����������
        }
    }

    private void SetLine()
    {
        Vector3[] points = new Vector3[_segments + 1];
        float angle = 0f;

        for (int i = 0; i < _segments + 1; i++)
        {
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Cos(angle * Mathf.Deg2Rad) * (radius * _yRadiusCoeff);

            points[i] = new Vector3(x, y, 0f) + transform.position;
            angle += 360f / _segments;
        }

        _lineRenderer.SetPositions(points);
    }

    public override void UpdateData(EntityConfig data)
    {
        if (data == null || data.combatRadius < 0) return;

        radius = data.combatRadius;
        SetLine();
    }
}
