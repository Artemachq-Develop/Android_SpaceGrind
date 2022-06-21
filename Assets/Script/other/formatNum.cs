using UnityEngine;

public static class formatNum
{
    private static string[] names = new[]
    {
        "",
        "K",
        "M",
        "B",
        "T"
    };

    public static string FormatNum(float num)
    {
        if (num == 0) return "0";

        num = Mathf.Round((float) num);

        int i = 0;
        while (i + 1 < names.Length && num >= 1000d)
        {
            num /= 1000f;
            i++;
        }

        return num.ToString("#.##") + names[i];
    }
}