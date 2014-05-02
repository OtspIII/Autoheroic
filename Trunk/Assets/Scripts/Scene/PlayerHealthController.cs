using UnityEngine;
using System.Collections;

public class PlayerHealthController : MonoBehaviour
{

    public Cub.View.Character Who;
    public UITexture FG;
    public UILabel Name;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(Cub.View.Character who, Cub.Model.Team t)
    {
        Who = who;
        //FG.color = t.Colour_Primary;
        FG.SetDimensions(60, 10);
        Name.text = who.Stat.Name;
    }
}
