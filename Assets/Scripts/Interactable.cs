using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string promptMessage = "Nhấn E để nhặt";

    public void Interact()
    {
        Debug.Log("Đã tương tác với vật phẩm!");
        Destroy(gameObject);
    }
}