using UnityEngine;
using System.Collections;
using Assets.Scripts.Language;

public abstract class ExhibitionContent
{
    public string path { get; set; }
    public Language lg { get; set; }
    public string caption { get; set; }

    protected ExhibitionContent(string path, string lg, string cpt)
    {
        this.path = path;
        this.lg = PoiDescription.convertStringToLang(lg);
        caption = cpt;
    }
}

