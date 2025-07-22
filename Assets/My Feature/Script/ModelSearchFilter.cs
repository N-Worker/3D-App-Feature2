using System.Collections.Generic;
using UnityEngine;

public static class ModelSearchFilter
{
    public static List<GameObject> Filter(List<GameObject> allModels, string keyword)
    {
        if (string.IsNullOrEmpty(keyword))
            return allModels;

        keyword = keyword.ToLower();

        return allModels.FindAll(model =>
        {
            string name = model.name.ToLower();

            // ตรงตามตัวสะกดเริ่มต้น หรือพบในชื่อ
            if (name.StartsWith(keyword) || name.Contains(keyword))
                return true;

            if (model.TryGetComponent(out ModelProperties props))
            {
                string idString = props.studenID.ToString();

                // ค้นจากรหัสนักศึกษา
                if (idString.StartsWith(keyword) || idString.Contains(keyword))
                    return true;

                // ค้นหมวดแบบขึ้นต้น เช่น "hum", "mon", "pro", "oth"
                if ("humanoid".StartsWith(keyword) && props.humanoids) return true;
                if ("monster".StartsWith(keyword) && props.monsters) return true;
                if ("prop".StartsWith(keyword) && props.props) return true;
                if ("other".StartsWith(keyword) && props.others) return true;
            }

            return false;
        });
    }
}
