using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartupScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent OnStart;
    [SerializeField] private UnityEngine.Events.UnityEvent OnDone;
    
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField] private Image image;
    [SerializeField] private bool nextScene;



    private void Start()
    {
        OnStart.Invoke();
        if (image)
        {
            StartCoroutine(StartFade());
        }
        else
        {
            LoadScene(1);
        }
    }


    IEnumerator StartFade()
    {
        var percent = 0f;
        while(percent < 1)
        {
            var value = Time.deltaTime * ((fadeTime != 0) ? (fadeTime / 1) : 1f);
            percent = Mathf.Clamp01(percent + value);
            var newColor = animationCurve.Evaluate(percent);
            image.color = new Color(newColor, newColor, newColor);
            yield return 0;
        }
        if(nextScene)
        {
            LoadScene(1);
        }
        else
        {
            OnDone.Invoke();
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);

    }

}
