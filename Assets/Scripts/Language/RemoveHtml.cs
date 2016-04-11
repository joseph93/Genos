using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class RemoveHtml
{

    public static string RemoveHtmlTags(string value)
    {
        var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
        var step2 = Regex.Replace(step1, @"\s{2,}", " ");
        return step2;
    }

}