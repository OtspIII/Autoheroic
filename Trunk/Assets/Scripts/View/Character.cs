using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class Character_Skeleton
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
    }

    public class Character_Part
    {
        public List<Cub.View.Cubon> Head { get; set; }
        public List<Cub.View.Cubon> Body { get; set; }
        public List<Cub.View.Cubon> Arms_Left { get; set; }
        public List<Cub.View.Cubon> Arms_Right { get; set; }
        public List<Cub.View.Cubon> Legs_Left { get; set; }
        public List<Cub.View.Cubon> Legs_Right { get; set; }
    }

    public class Character_Stat
    {
        public System.Guid ID { get; set; }
        public Cub.Model.Team Team { get; set; }
        public int MHP { get; set; }
        public int HP { get; set; }
        public Cub.Position2 Position { get; set; }

        public Part_Head Head { get; set; }
        public Part_Body Body { get; set; }
        public Part_Arms Arms { get; set; }
        public Part_Legs Legs { get; set; }
    }

    public class Character : MonoBehaviour
    {
        public Character_Stat Stat { get; private set; }
        public Character_Skeleton Skeleton { get; private set; }
        public Character_Part Part { get; private set; }

        public Character()
        {
            this.Stat = new Character_Stat();
            this.Skeleton = new Character_Skeleton();
            this.Part = new Character_Part();
        }

        public void Initialize_Stat(Guid _ID, int _MHP, int _HP, Position2 _Position, Cub.Model.Team _Team, Part_Head _Head, Part_Body _Body, Part_Arms _Arms, Part_Legs _Legs)
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
            this.Skeleton = Library.Get_Character_Model();
        }

        public void Initialize_Bodypart()
        {
            this.Part.Head = Library.Get_Part_Head(this.Stat.Head);
            Debug.Log("X: " + Part.Head);
            this.Part.Body = Library.Get_Part_Body(this.Stat.Body);
            this.Part.Arms_Left = Library.Get_Part_Arms_Left(this.Stat.Arms);
            this.Part.Arms_Right = Library.Get_Part_Arms_Right(this.Stat.Arms);
            this.Part.Legs_Left = Library.Get_Part_Legs_Left(this.Stat.Legs);
            this.Part.Legs_Right = Library.Get_Part_Legs_Right(this.Stat.Legs);

            GameObject GO_Head = this.gameObject.transform.FindChild("Head").gameObject;
            GameObject GO_Body = this.gameObject.transform.FindChild("Body").gameObject;
            GameObject GO_Arms_Left = this.gameObject.transform.FindChild("Arms_Left").gameObject;
            GameObject GO_Arms_Right = this.gameObject.transform.FindChild("Arms_Right").gameObject;
            GameObject GO_Legs_Left = this.gameObject.transform.FindChild("Legs_Left").gameObject;
            GameObject GO_Legs_Right = this.gameObject.transform.FindChild("Legs_Right").gameObject;

            GO_Body.transform.localPosition = Skeleton.Position_Body;
            GO_Head.transform.localPosition = Skeleton.Position_Body + Skeleton.Position_Body_Head;
            Debug.Log(GO_Head.transform.localPosition);
            GO_Arms_Left.transform.localPosition = Skeleton.Position_Body + Skeleton.Position_Body_Arms_Left;
            GO_Arms_Right.transform.localPosition = Skeleton.Position_Body + Skeleton.Position_Body_Arms_Right;
            GO_Legs_Left.transform.localPosition = Skeleton.Position_Body + Skeleton.Position_Body_Legs_Left;
            GO_Legs_Right.transform.localPosition = Skeleton.Position_Body + Skeleton.Position_Body_Legs_Right;

            GO_Body.transform.rotation = Quaternion.Euler(Skeleton.Rotation_Body);
            GO_Head.transform.rotation = Quaternion.Euler(Skeleton.Rotation_Body_Head);
            GO_Arms_Left.transform.rotation = Quaternion.Euler(Skeleton.Rotation_Body_Arms_Left);
            GO_Arms_Right.transform.rotation = Quaternion.Euler(Skeleton.Rotation_Body_Arms_Right);
            GO_Legs_Left.transform.rotation = Quaternion.Euler(Skeleton.Rotation_Body_Legs_Left);
            GO_Legs_Right.transform.rotation = Quaternion.Euler(Skeleton.Rotation_Body_Legs_Right);

            foreach (Cubon C in Part.Head)
            {
                foreach (Position3 P in C.Position)
                {
                    GameObject G = Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = GO_Head.transform.FindChild("Model").transform;
                    G.transform.localPosition = P.ToVector3();
                    G.transform.localScale = G.transform.lossyScale;
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material.color = Get_Color(C.Colour);
                }
            }

            foreach (Cubon C in Part.Body)
            {
                foreach (Position3 P in C.Position)
                {
                    GameObject G = Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = GO_Body.transform.FindChild("Model").transform;
                    G.transform.localPosition = P.ToVector3();
                    G.transform.localScale = G.transform.lossyScale;
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material.color = Get_Color(C.Colour);
                }
            }

            foreach (Cubon C in Part.Arms_Left)
            {
                foreach (Position3 P in C.Position)
                {
                    GameObject G = Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = GO_Arms_Left.transform.FindChild("Model").transform;
                    G.transform.localPosition = P.ToVector3();
                    G.transform.localScale = G.transform.lossyScale;
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material.color = Get_Color(C.Colour);
                }
            }

            foreach (Cubon C in Part.Arms_Right)
            {
                foreach (Position3 P in C.Position)
                {
                    GameObject G = Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = GO_Arms_Right.transform.FindChild("Model").transform;
                    G.transform.localPosition = P.ToVector3();
                    G.transform.localScale = G.transform.lossyScale;
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material.color = Get_Color(C.Colour);
                }
            }

            foreach (Cubon C in Part.Legs_Left)
            {
                foreach (Position3 P in C.Position)
                {
                    GameObject G = Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = GO_Legs_Left.transform.FindChild("Model").transform;
                    G.transform.localPosition = P.ToVector3();
                    G.transform.localScale = G.transform.lossyScale;
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material.color = Get_Color(C.Colour);
                }
            }

            foreach (Cubon C in Part.Legs_Right)
            {
                foreach (Position3 P in C.Position)
                {
                    GameObject G = Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = GO_Legs_Right.transform.FindChild("Model").transform;
                    G.transform.localPosition = P.ToVector3();
                    G.transform.localScale = G.transform.lossyScale;
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material.color = Get_Color(C.Colour);
                }
            }
        }

        private Color32 Get_Color(Colour _Colour)
        {
            switch (_Colour)
            {
                case Colour.Red:
                    {
                        return new Color32(255, 0, 0, 255);
                    }
                case Colour.Black:
                    {
                        return new Color32(0, 0, 0, 255);
                    }
                case Colour.White:
                    {
                        return new Color32(255, 255, 255, 255);
                    }
                case Colour.Silver:
                    {
                        return new Color32(100, 100, 100, 255);
                    }
                case Colour.Team_Primary:
                    {
                        return this.Stat.Team.Colour_Primary;
                    }
                case Colour.Team_Secondary:
                    {
                        return this.Stat.Team.Colour_Secondary;
                    }
                default:
                    {
                        return new Color32(0, 0, 0, 255);
                    }

            }
        }

        public AudioClip PlaySound(AudioClip sound)
        {
            audio.PlayOneShot(sound);
            return sound;
        }
    }
}
