using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinalBossDeath : MonoBehaviour
{
    public float TimeDelay = 1;
    public string NextScene = "";

    void OnDestroy()
    {
        if (NextScene.Length > 0) {
            BulletArena.Instance.StartCoroutine(GoNext());
        }
    }

    IEnumerator GoNext()
    {
        yield return new WaitForSeconds(TimeDelay);
        SceneManager.LoadScene(NextScene);
        yield break;
    }
}
