using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine;

public class AnimatableExtension : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private IEnumerator blinkCoroutine(float blindTime)
    {
        Material mat = GameObject.FindGameObjectWithTag("UI").transform.Find("Canvas_Blink").Find("UIBlink").GetComponent<Image>().material;
        float currentTime = 0;
        //float start = audioSource.volume;
        float duration = 0.7f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            mat.SetColor("_BaseColor", new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, currentTime / duration)));
            yield return null;
        }
        yield return new WaitForSeconds(blindTime);
        currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            mat.SetColor("_BaseColor", new Color(0f, 0f, 0f, Mathf.Lerp(1f, 0f, currentTime / duration)));
            yield return null;
        }
        yield break;
    }

    private ScriptableRendererData ExtractScriptableRendererData()
    {
        var pipeline = ((UniversalRenderPipelineAsset)GraphicsSettings.renderPipelineAsset);
        FieldInfo propertyInfo = pipeline.GetType().GetField("m_RendererDataList", BindingFlags.Instance | BindingFlags.NonPublic);
        return ((ScriptableRendererData[])propertyInfo?.GetValue(pipeline))?[0];
    }

    public void setBlurAmount(int passes)
    {
        ScriptableRendererData rendererData = ExtractScriptableRendererData();
        var renderObj = (KawaseBlur)rendererData.rendererFeatures[0];
        renderObj.settings.blurPasses = passes;
        rendererData.SetDirty();
    }
    public void blink(float duration)
    {
        StartCoroutine(blinkCoroutine(duration));
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    public void SetPlayerImmobile(int state)
    {
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.isImmobile = (state == 1);
    }
    public void SetPlayerFlashlightState(int state)
    {
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.setFlashLightState(state == 1);
    }
    public void PlaySound(string name)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(string.Concat("Sounds/" ,name));
        if (!audioClip)
            return;
        AudioSource audioSource =  this.gameObject.AddComponent<AudioSource>();
        audioSource.Stop();
        audioSource.volume = 1f;
        audioSource.loop = false;
        audioSource.spatialBlend = 1.0f;
        audioSource.maxDistance = 25;
        audioSource.clip = audioClip;

        audioSource.Play();
    }
}
