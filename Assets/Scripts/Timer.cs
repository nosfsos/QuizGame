using System.Collections;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class Timer : MonoBehaviour
{
    public Text Label;
    public float TotalTime;

    private Image _image;
    

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }
    
    private IEnumerator StartTimer()
    {
        while (true)
        {
            if (_image.fillAmount == 0)
            {
                Label.text = "0";
                yield break;
                //ResetTimer();
            }

            _image.fillAmount -= 1.0f / TotalTime * Time.deltaTime;
            Label.text = ((int)(TotalTime * _image.fillAmount) + 1).ToString();
            yield return null;
        }
    }

    public void ResetTimer()
    {
        _image.fillAmount = 1f;
        Label.text = TotalTime.ToString();
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void StopTimer()
    {
        _image.fillAmount = 0;
    }
}
