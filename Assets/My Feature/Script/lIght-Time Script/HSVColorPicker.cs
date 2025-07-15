using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSVColorPicker : MonoBehaviour
{
    [Header("Sliders")]
    public Slider sliderHue;
    public Slider sliderSaturation;
    public Slider sliderValue;

    [Header("RawImages for Background")]
    public RawImage hueImage;
    public RawImage saturationImage;
    public RawImage valueImage;

    [Header("Output")]
    public Image previewImage;
    public Light targetLight;

    private Texture2D hueTexture;
    private Texture2D saturationTexture;
    private Texture2D valueTexture;

    private const int textureWidth = 256;

    void Start()
    {
        GenerateHueTexture();
        GenerateSaturationTexture();
        GenerateValueTexture();

        sliderHue.onValueChanged.AddListener(delegate { OnHSVChanged(); });
        sliderSaturation.onValueChanged.AddListener(delegate { OnHSVChanged(); });
        sliderValue.onValueChanged.AddListener(delegate { OnHSVChanged(); });

        OnHSVChanged(); // Init
    }

    void GenerateHueTexture()
    {
        hueTexture = new Texture2D(textureWidth, 1);
        for (int x = 0; x < textureWidth; x++)
        {
            float h = (float)x / (textureWidth - 1);
            Color color = Color.HSVToRGB(h, 1f, 1f);
            hueTexture.SetPixel(x, 0, color);
        }
        hueTexture.Apply();
        hueImage.texture = hueTexture;
    }

    void GenerateSaturationTexture()
    {
        saturationTexture = new Texture2D(textureWidth, 1);
        hueImage.texture = hueTexture;
        saturationImage.texture = saturationTexture;
    }

    void GenerateValueTexture()
    {
        valueTexture = new Texture2D(textureWidth, 1);
        valueImage.texture = valueTexture;
    }

    void OnHSVChanged()
    {
        float h = sliderHue.value;
        float s = sliderSaturation.value;
        float v = sliderValue.value;

        // อัปเดต Preview และ Light
        Color c = Color.HSVToRGB(h, s, v);
        if (previewImage) previewImage.color = c;
        if (targetLight) targetLight.color = c;

        // อัปเดตแถบ S และ V ใหม่ ตาม Hue ที่เลือก
        for (int x = 0; x < textureWidth; x++)
        {
            float t = (float)x / (textureWidth - 1);

            // S: สีไปขาว
            Color colorS = Color.HSVToRGB(h, t, v);
            saturationTexture.SetPixel(x, 0, colorS);

            // V: ดำไปสี
            Color colorV = Color.HSVToRGB(h, s, t);
            valueTexture.SetPixel(x, 0, colorV);
        }

        saturationTexture.Apply();
        valueTexture.Apply();
    }
}
