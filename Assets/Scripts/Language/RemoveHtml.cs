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
<<<<<<< HEAD
        var step3 = step2.Replace("\\n", string.Empty);
=======
        var step3 = step2.Replace("\\n", "");
>>>>>>> 6f55a59d5408080a30da2e37f70ca10bc1358859
        return step3;
    }

}