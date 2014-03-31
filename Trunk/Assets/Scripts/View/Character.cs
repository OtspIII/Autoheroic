using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class Character_Model
    {
        public Vector3 Position_Body { get; set; }
        public Vector3 Position_Body_Head { get; set; }
        public Vector3 Position_Body_Arms_Left { get; set; }
        public Vector3 Position_Body_Arms_Right { get; set; }
        public Vector3 Position_Body_Legs_Left { get; set; }
        public Vector3 Position_Body_Legs_Right { get; set; }

        public Vector3 Rotation_Body { get; set; }
        public Vector3 Rotation_Body_Head { get; set; }
        public Vector3 Rotation_Body_Arms_Left { get; set; }
        public Vector3 Rotation_Body_Arms_Right { get; set; }
        public Vector3 Rotation_Body_Legs_Left { get; set; }
        public Vector3 Rotation_Body_Legs_Right { get; set; }

        public Cub.View.Cubon Cubon_Head { get; set; }
        public Cub.View.Cubon Cubon_Body { get; set; }
        public Cub.View.Cubon Cubon_Arms_Left { get; set; }
        public Cub.View.Cubon Cubon_Arms_Right { get; set; }
        public Cub.View.Cubon Cubon_Legs_Left { get; set; }
        public Cub.View.Cubon Cubon_Legs_Right { get; set; }
    }

    public class Character_Stat
    {
        public System.Guid ID { get; set; }
        public Cub.Model.Team Team { get; set; }
        public int MHP { get; set; }
        public int HP { get; set; }
        public Cub.Position2 Position { get; set; }

        public Bodypart_Head Head { get; set; }
        public Bodypart_Body Body { get; set; }
        public Bodypart_Arms Arms { get; set; }
        public Bodypart_Legs Legs { get; set; }
    }

    public class Character : MonoBehaviour
    {
        public Character_Stat Stat { get; private set; }
        public Character_Model Model { get; private set; }

        public Character()
        {
            this.Stat = new Character_Stat();
            this.Model = new Character_Model();
        }

        public void Initialize_Stat(Guid _ID, int _MHP, int _HP, Position2 _Position, Cub.Model.Team _Team, Bodypart_Head _Head, Bodypart_Body _Body, Bodypart_Arms _Arms, Bodypart_Legs _Legs)
        {
            this.Stat.ID = _ID;
            this.Stat.MHP = _MHP;
            this.Stat.HP = _HP;
            this.Stat.Position = _Position;
            this.Stat.Team = _Team;

            this.Stat.Head = _Head;
            this.Stat.Body = _Body;
            this.Stat.Arms = _Arms;
            this.Stat.Legs = _Legs;
        }

        public void Initialize_Model()
        {
            this.Model.Cubon_Head = Library.Get_Bodypart_Head(this.Stat.Head);
            this.Model.Cubon_Body = Library.Get_Bodypart_Body(this.Stat.Body);
            this.Model.Cubon_Arms_Left = Library.Get_Bodypart_Arms_Left(this.Stat.Arms);
            this.Model.Cubon_Arms_Right = Library.Get_Bodypart_Arms_Right(this.Stat.Arms);
            this.Model.Cubon_Legs_Left = Library.Get_Bodypart_Legs_Left(this.Stat.Legs);
            this.Model.Cubon_Legs_Right = Library.Get_Bodypart_Legs_Right(this.Stat.Legs);

            GameObject GO_Head = this.gameObject.transform.FindChild("Head").gameObject;
            GameObject GO_Body = this.gameObject.transform.FindChild("Body").gameObject;
            GameObject GO_Arms_Left = this.gameObject.transform.FindChild("Arms_Left").gameObject;
            GameObject GO_Arms_Right = this.gameObject.transform.FindChild("Arms_Right").gameObject;
            GameObject GO_Legs_Left = this.gameObject.transform.FindChild("Legs_Left").gameObject;
            GameObject GO_Legs_Right = this.gameObject.transform.FindChild("Legs_Right").gameObject;

            GO_Body.transform.localPosition = Model.Position_Body;
            GO_Head.transform.localPosition = Model.Position_Body + Model.Position_Body_Head;
            GO_Arms_Left.transform.localPosition = Model.Position_Body + Model.Position_Body_Arms_Left;
            GO_Arms_Right.transform.localPosition = Model.Position_Body + Model.Position_Body_Arms_Right;
            GO_Legs_Left.transform.localPosition = Model.Position_Body + Model.Position_Body_Legs_Left;
            GO_Legs_Right.transform.localPosition = Model.Position_Body + Model.Position_Body_Legs_Right;

            GO_Body.transform.rotation = Quaternion.Euler(Model.Rotation_Body);
            GO_Head.transform.rotation = Quaternion.Euler(Model.Rotation_Body_Head);
            GO_Arms_Left.transform.rotation = Quaternion.Euler(Model.Rotation_Body_Arms_Left);
            GO_Arms_Right.transform.rotation = Quaternion.Euler(Model.Rotation_Body_Arms_Right);
            GO_Legs_Left.transform.rotation = Quaternion.Euler(Model.Rotation_Body_Legs_Left);
            GO_Legs_Right.transform.rotation = Quaternion.Euler(Model.Rotation_Body_Legs_Right);

            foreach (Position3 P in Model.Cubon_Head.Position)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                
                G.transform.parent = GO_Head.transform.FindChild("Model").transform;
                G.transform.localPosition = P.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
            }

            foreach (Position3 P in Model.Cubon_Body.Position)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                
                G.transform.parent = GO_Body.transform.FindChild("Model").transform;
                G.transform.localPosition = P.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
            }

            foreach (Position3 P in Model.Cubon_Arms_Left.Position)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                
                G.transform.parent = GO_Arms_Left.transform.FindChild("Model").transform;
                G.transform.localPosition = P.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
            }

            foreach (Position3 P in Model.Cubon_Arms_Right.Position)
            {         
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
             
                G.transform.parent = GO_Arms_Right.transform.FindChild("Model").transform;
                G.transform.localPosition = P.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
            }

            foreach (Position3 P in Model.Cubon_Legs_Left.Position)
            {   
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                
                G.transform.parent = GO_Legs_Left.transform.FindChild("Model").transform;
                G.transform.localPosition = P.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
            }

            foreach (Position3 P in Model.Cubon_Legs_Right.Position)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                
                G.transform.parent = GO_Legs_Right.transform.FindChild("Model").transform;
                G.transform.localPosition = P.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
            }
        }

        public AudioClip PlaySound(AudioClip sound)
        {
            audio.PlayOneShot(sound);
            return sound;
        }
    }
}
