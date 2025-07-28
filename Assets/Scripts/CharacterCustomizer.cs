using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizer : MonoBehaviour
{
    [Header("Accessory Objects")]
    [Tooltip("Add all swappable accessory GameObjects here.")]
    [SerializeField] private List<GameObject> accessories;
    private int currentAccessoryIndex = 0;

    [Header("Material Customization")]
    [Tooltip("The main Renderer of the character model.")]
    [SerializeField] private Renderer characterRenderer;
    private Material characterMaterialInstancel; // We'll work on a copy of the material

    private void Start()
    {
        characterMaterialInstancel = characterRenderer.material;

        for(int i = 0; i < accessories.Count; i++)
        {
            accessories[i].SetActive(i == currentAccessoryIndex);
        }
    }

    public void CycleAccessory()
    {
        accessories[currentAccessoryIndex].SetActive(false);

        currentAccessoryIndex = (currentAccessoryIndex + 1) % accessories.Count;

        accessories[currentAccessoryIndex].SetActive(true);
    }

    public void ChangePaintColor(Color newColor)
    {
        if(characterMaterialInstancel != null)
        {
            characterMaterialInstancel.color = newColor;
        }
    }

    public void ChangeLightColor(Color newColor)
    {
        if(characterMaterialInstancel != null)
        {
            characterMaterialInstancel.EnableKeyword("_EMISSION");
            characterMaterialInstancel.SetColor("_EmissionColor", newColor);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CycleAccessory();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangePaintColor(Color.red);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeLightColor(Color.cyan * 2f);
        }
    }
}
