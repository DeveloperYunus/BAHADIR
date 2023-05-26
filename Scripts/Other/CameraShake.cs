using UnityEngine;
using EZCameraShake;

public class CameraShake : MonoBehaviour
{
    public float KuvvetMagnitude, siklikRoughness, hizliSon, gecSon;  //brackesy 4, 4, 0.1f, 1  (default)

    public void Shakee()
    {
        CameraShaker.Instance.ShakeOnce(KuvvetMagnitude, siklikRoughness, hizliSon, gecSon);  //kuvvet, sýklýk, artarsa hýzlý sona erer, artarsa gec sona erer
    }

    /*public IEnumerator Shake(float duration,float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;
        print(originalPos);
        while (elapsed < duration) 
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(0.02f);
        }
        transform.localPosition = originalPos;
    }*/
}
