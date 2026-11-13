/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Actualiza el HUD de contaminantes recolectados y el progreso hacia la meta.
 * Explicación: Escucha al CleaningManager y, cada vez que se recoge un contaminante,
 *              suma uno al contador de su slot (que puede agrupar varios datos del
 *              mismo tipo, como lata entera y abollada) y actualiza el slider, que
 *              se llena respecto a la meta elegida, junto con su texto.
 *********************************************************************************************/

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContaminantsHUD : MonoBehaviour
{
    [Serializable]
    private class ContaminantSlotUI
    {
        [Tooltip("Nombre del slot, solo para identificarlo en el Inspector.")]
        public string label;

        [Tooltip("Todos los ScriptableObject que cuentan en este slot (ej: lata entera y lata abollada).")]
        public ContaminantDataSO[] contaminants;

        [Tooltip("Imagen del ícono del slot. Solo se usa si 'Dim When Empty' está activo.")]
        public Image icon;

        public TMP_Text countText;

        [NonSerialized] public int count;
    }

    [Header("Slots (uno por tipo de contaminante)")]
    [SerializeField] private ContaminantSlotUI[] slots;

    [Header("Progreso hacia la meta")]
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TMP_Text percentText;

    [Header("Estado visual (opcional)")]
    [Tooltip("Si está activo, el ícono se ve atenuado mientras el contador está en 0.")]
    [SerializeField] private bool dimWhenEmpty = false;
    [SerializeField, Range(0f, 1f)] private float emptyAlpha = 0.4f;

    private readonly Dictionary<ContaminantDataSO, ContaminantSlotUI> slotsByData =
        new Dictionary<ContaminantDataSO, ContaminantSlotUI>();

    private void Awake()
    {
        foreach (ContaminantSlotUI slot in slots)
        {
            foreach (ContaminantDataSO data in slot.contaminants)
            {
                if (data != null)
                {
                    slotsByData[data] = slot;
                }
            }

            slot.count = 0;
            RefreshSlot(slot);
        }
    }

    private void Start()
    {
        if (CleaningManager.Instance != null)
        {
            CleaningManager.Instance.ContaminantRegistered += OnContaminantRegistered;
        }

        RefreshProgress();
    }

    private void OnDestroy()
    {
        if (CleaningManager.Instance != null)
        {
            CleaningManager.Instance.ContaminantRegistered -= OnContaminantRegistered;
        }
    }

    private void OnContaminantRegistered(ContaminantDataSO data)
    {
        if (slotsByData.TryGetValue(data, out ContaminantSlotUI slot))
        {
            slot.count++;
            RefreshSlot(slot);
        }
        else
        {
            Debug.LogWarning($"[ContaminantsHUD] '{data.contaminantName}' no está asignado a ningún slot.");
        }

        RefreshProgress();
    }

    private void RefreshSlot(ContaminantSlotUI slot)
    {
        if (slot.countText != null)
        {
            slot.countText.text = $"x{slot.count}";
        }

        if (dimWhenEmpty && slot.icon != null)
        {
            Color color = slot.icon.color;
            color.a = slot.count > 0 ? 1f : emptyAlpha;
            slot.icon.color = color;
        }
    }

    private void RefreshProgress()
    {
        CleaningManager manager = CleaningManager.Instance;
        if (manager == null) return;

        if (progressSlider != null)
        {
            progressSlider.value = manager.GoalProgress01;
        }

        if (percentText != null)
        {
            percentText.text =
                $"{Mathf.RoundToInt(manager.CurrentCleaningPercentage)}% / {Mathf.RoundToInt(manager.GoalPercentage)}%";
        }
    }
}