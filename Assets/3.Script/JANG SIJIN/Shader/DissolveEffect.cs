using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    public Material dissolveMaterial; // 디졸브 효과용 머티리얼
    private Renderer rend;
    private float dissolveAmount = 0f;
    private bool isDissolving = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isDissolving)
        {
            dissolveAmount += Time.deltaTime * 0.5f;
            dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);

            if (dissolveAmount >= 1f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void StartDissolve()
    {
        isDissolving = true;
        rend.material = dissolveMaterial; // 기존 머티리얼을 디졸브 셰이더가 적용된 것으로 변경
    }
}
