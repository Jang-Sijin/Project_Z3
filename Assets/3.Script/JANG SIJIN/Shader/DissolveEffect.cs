using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    public Material dissolveMaterial; // ������ ȿ���� ��Ƽ����
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
        rend.material = dissolveMaterial; // ���� ��Ƽ������ ������ ���̴��� ����� ������ ����
    }
}
