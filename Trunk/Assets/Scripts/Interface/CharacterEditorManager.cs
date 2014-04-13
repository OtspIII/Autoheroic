using UnityEngine;
using System.Collections;
using Cub.Interface;
using System.Collections.Generic;

public class CharacterEditorManager : OptionsListController
{

    public GameObject PersonalCamera;

    Dictionary<MenuChoiceController, List<Cub.Model.Bodypart>> OptionOptions;
    //MenuChoiceController SelectedHori;
    //List<Cub.Model.BPHead> Heads;
    //List<Cub.Model.BPBody> Bodies;
    //List<Cub.Model.BPArms> Arms;
    //List<Cub.Model.BPLegs> Legs;
    Cub.Model.BPHead Head;
    Cub.Model.BPBody Body;
    Cub.Model.BPArms Arms;
    Cub.Model.BPLegs Legs;

    float HoriTimer;

    Cub.View.Character Who;
    Cub.Model.Character_Save WhoSave;

    public UILabel Name;
    public UILabel Cost;
    public UILabel AI;
    public UILabel Weapon;
    public UILabel Special;

    public UILabel Health;
    public UILabel Speed;
    public UILabel Range;
    public UILabel Damage;

    protected override void MoreUpdate()
    {
        if (HoriTimer > 0)
            HoriTimer -= Time.deltaTime;

        if (GetInput("Horizontal") > 0.1f && HoriTimer <= 0)
        {
            ChangeHoriSelection(-1);
            HoriTimer = SelectTimer;
        }
        else if (GetInput("Horizontal") < -0.1f && HoriTimer <= 0)
        {
            ChangeHoriSelection(1);
            HoriTimer = SelectTimer;
        }
        else if (HoriTimer > 0 && Mathf.Abs(GetInput("Horizontal")) < 0.1f)
            HoriTimer = 0;
    }

    public void Setup(Cub.View.Character VChar, Cub.Model.Character_Save SChar)
    {
        Who = VChar;
        WhoSave = SChar;
        PersonalCamera.SetActive(true);
        PersonalCamera.transform.position =
            new Vector3(VChar.transform.position.x + 2, VChar.transform.position.y + 1.2f, VChar.transform.position.z + 0.5f);
        CurrentlyActive = true;
        OnSelectChange();
        //Heads = Cub.Model.Library.List_Heads();
        //Arms = Cub.Model.Library.List_Arms();
        //Bodies = Cub.Model.Library.List_Bodies();
        //Legs = Cub.Model.Library.List_Legs();
        OptionOptions = new Dictionary<MenuChoiceController, List<Cub.Model.Bodypart>>();
        OptionOptions.Add(Options[0], new List<Cub.Model.Bodypart>());
        foreach (Cub.Model.Bodypart bp in Cub.Model.Library.List_Heads())
            OptionOptions[Options[0]].Add(bp);

        OptionOptions.Add(Options[1], new List<Cub.Model.Bodypart>());
        foreach (Cub.Model.Bodypart bp in Cub.Model.Library.List_Arms())
            OptionOptions[Options[1]].Add(bp);

        OptionOptions.Add(Options[2], new List<Cub.Model.Bodypart>());
        foreach (Cub.Model.Bodypart bp in Cub.Model.Library.List_Bodies())
            OptionOptions[Options[2]].Add(bp);

        OptionOptions.Add(Options[3], new List<Cub.Model.Bodypart>());
        foreach (Cub.Model.Bodypart bp in Cub.Model.Library.List_Legs())
            OptionOptions[Options[3]].Add(bp);

        Head = SChar.Head_Part;
        Arms = SChar.Arms_Part;
        Body = SChar.Body_Part;
        Legs = SChar.Legs_Part;

        WriteDescriptions();

        //PersonalCamera.transform.LookAt(VChar.transform);
    }

    void WriteDescriptions()
    {
        Name.text = WhoSave.Name;
        Cost.text = "Cost: " + WhoSave.Value.ToString() + "pts";
        AI.text = "AI: " + WhoSave.Head_Part.Description + " (" + WhoSave.Head_Part.Cost.ToString() + "pts)";
        Weapon.text = "Wpn: " + WhoSave.Arms_Part.Name + " (" + WhoSave.Arms_Part.Cost.ToString() + "pts)";
        Special.text = "Spcl: " + "--";

        Health.text = "HP: " + WhoSave.Body_Part.Health.ToString();
        Speed.text = "Speed: " + WhoSave.Legs_Part.Speed.ToString();
        Range.text = "Range: " + WhoSave.Weapon.Range.ToString();
        Damage.text = "Damage: " + WhoSave.Weapon.HitDam.ToString();
    }

    protected override void Click()
    {

    }

    protected virtual void ChangeHoriSelection(int n)
    {
        Cub.Model.Bodypart bp = null;
        if (Selected.Option == MenuOptions.Head) bp = Head;
        else if (Selected.Option == MenuOptions.Arms) bp = Arms;
        else if (Selected.Option == MenuOptions.Body) bp = Body;
        else if (Selected.Option == MenuOptions.Legs) bp = Legs;
        int current = OptionOptions[Selected].IndexOf(bp);
        current += n;
        if (current >= OptionOptions[Selected].Count)
            current = 0;
        else if (current < 0)
            current = OptionOptions[Selected].Count - 1;
        if (Selected.Option == MenuOptions.Head)
        {
            Head = (Cub.Model.BPHead)OptionOptions[Selected][current];
            Who.Stat.Head = ((Cub.Model.BPHead)Head).E;
            WhoSave.Head = ((Cub.Model.BPHead)Head).E;
        }
        else if (Selected.Option == MenuOptions.Arms)
        {
            Arms = (Cub.Model.BPArms)OptionOptions[Selected][current];
            Who.Stat.Arms = ((Cub.Model.BPArms)Arms).E;
            WhoSave.Arms = ((Cub.Model.BPArms)Arms).E;
        }
        else if (Selected.Option == MenuOptions.Body)
        {
            Body = (Cub.Model.BPBody)OptionOptions[Selected][current];
            Who.Stat.Body = ((Cub.Model.BPBody)Body).E;
            WhoSave.Body = ((Cub.Model.BPBody)Body).E;
        }
        else if (Selected.Option == MenuOptions.Legs)
        {
            Legs = (Cub.Model.BPLegs)OptionOptions[Selected][current];
            Who.Stat.Legs = ((Cub.Model.BPLegs)Legs).E;
            WhoSave.Legs = ((Cub.Model.BPLegs)Legs).E;
        }
        Who.Delete_Part();
        Who.Initialize_Part();
        WriteDescriptions();
    }

    protected override void OnSelectChange()
    {
        foreach (MenuChoiceController mcc in Options)
        {
            foreach (Transform t in mcc.transform)
            {
                ((UISprite)t.gameObject.GetComponent("UISprite")).color = Color.white;
            }
        }
        foreach (Transform t in Selected.transform)
        {
            ((UISprite)t.gameObject.GetComponent("UISprite")).color = Color.red;
        }
    }
}
