using UnityEngine;

public class ModelProperties : MonoBehaviour
{
    //เก็บเป็น int ไม่ใช่ string เพราะจะเอาไปใช้ต่อในภายหลัง
    public int studenID;
    [Header("Catagories")]
    public bool humanoids;
    public bool monsters;
    public bool props;
    public bool others;

    #if UNITY_EDITOR
    private string _lastValidatedName = "";
    private void OnValidate()
    {
        if (gameObject.name != _lastValidatedName)
        {
            _lastValidatedName = gameObject.name;

            if (int.TryParse(gameObject.name, out int parsedID)) studenID = parsedID;
            else Debug.LogError($"ชื่อของ '{gameObject.name}' ต้องเป็นรหัศนักศึกษาเท่านั้น ");
        }
        //ถ้าเพิ่ม Catagories มาแก้ใขตรงนี้เพิ่มด้วย
        if (!humanoids && !monsters && !props) others = true;
        else others = false;
    }
    #endif
}