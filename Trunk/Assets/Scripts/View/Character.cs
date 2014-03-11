using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class Character_Model
    {
        public Position3 Position_Body { get; set; }
        public Position3 Position_Body_Head { get; set; }
        public Position3 Position_Body_Hand_Left { get; set; }
        public Position3 Position_Body_Hand_Right { get; set; }
        public Position3 Position_Body_Foot_Left { get; set; }
        public Position3 Position_Body_Foot_Right { get; set; }
        public Position3 Position_Hand_Left_Equipment_Left { get; set; }
        public Position3 Position_Hand_Right_Equipment_Right { get; set; }

        public Position3 Rotation_Body { get; set; }
        public Position3 Rotation_Body_Head { get; set; }
        public Position3 Rotation_Body_Hand_Left { get; set; }
        public Position3 Rotation_Body_Hand_Right { get; set; }
        public Position3 Rotation_Body_Foot_Left { get; set; }
        public Position3 Rotation_Body_Foot_Right { get; set; }
        public Position3 Rotation_Hand_Left_Equipment_Left { get; set; }
        public Position3 Rotation_Hand_Right_Equipment_Right { get; set; }

        public List<Cub.Cubon> Head { get; set; }
        public List<Cub.Cubon> Body { get; set; }
        public List<Cub.Cubon> Hand_Left { get; set; }
        public List<Cub.Cubon> Hand_Right { get; set; }
        public List<Cub.Cubon> Foot_Left { get; set; }
        public List<Cub.Cubon> Foot_Right { get; set; }
        public List<Cub.Cubon> Equipment_Left { get; set; }
        public List<Cub.Cubon> Equipment_Right { get; set; }
    }

    public class Character_Stat
    {
        public System.Guid ID { get; set; }
        public Cub.Class Class { get; set; }
        public int MHP { get; set; }
        public int HP { get; set; }
        public Cub.Position2 Position { get; set; }
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

        public void Initialize_Stat(Guid _ID, Cub.Class _Class, int _MHP, int _HP, Position2 _Position)
        {            
            this.Stat.ID = _ID;
            this.Stat.Class = _Class;
            this.Stat.MHP = _MHP;
            this.Stat.HP = _HP;
            this.Stat.Position = _Position;
        }

        public void Initialize_Model()
        {
            this.Model = Library.Get_Character_Model(this.Stat.Class);

            GameObject GO_Head = this.gameObject.transform.FindChild("Head").gameObject;
            GameObject GO_Body = this.gameObject.transform.FindChild("Body").gameObject;
            GameObject GO_Hand_Left = this.gameObject.transform.FindChild("Hand_Left").gameObject;
            GameObject GO_Hand_Right = this.gameObject.transform.FindChild("Hand_Right").gameObject;
            GameObject GO_Foot_Left = this.gameObject.transform.FindChild("Foot_Left").gameObject;
            GameObject GO_Foot_Right = this.gameObject.transform.FindChild("Foot_Right").gameObject;
            GameObject GO_Equipment_Left = GO_Hand_Left.transform.FindChild("Model/Equipment_Left").gameObject;
            GameObject GO_Equipment_Right = GO_Hand_Right.transform.FindChild("Model/Equipment_Right").gameObject;

            GO_Body.transform.position = this.Model.Position_Body.ToVector3();
            GO_Head.transform.localPosition = this.Model.Position_Body.ToVector3() + this.Model.Position_Body_Head.ToVector3();
            GO_Hand_Left.transform.localPosition = this.Model.Position_Body.ToVector3() + this.Model.Position_Body_Hand_Left.ToVector3();
            GO_Hand_Right.transform.localPosition = this.Model.Position_Body.ToVector3() + this.Model.Position_Body_Hand_Right.ToVector3();
            GO_Foot_Left.transform.localPosition = this.Model.Position_Body.ToVector3() + this.Model.Position_Body_Foot_Left.ToVector3();
            GO_Foot_Right.transform.localPosition = this.Model.Position_Body.ToVector3() + this.Model.Position_Body_Foot_Right.ToVector3();
            GO_Equipment_Left.transform.localPosition = this.Model.Position_Body.ToVector3() + this.Model.Position_Hand_Left_Equipment_Left.ToVector3();
            GO_Equipment_Right.transform.localPosition = this.Model.Position_Body.ToVector3() + this.Model.Position_Hand_Right_Equipment_Right.ToVector3();

            GO_Body.transform.rotation = Quaternion.Euler(this.Model.Rotation_Body.ToVector3());
            GO_Head.transform.rotation = Quaternion.Euler(this.Model.Rotation_Body_Head.ToVector3());
            GO_Hand_Left.transform.rotation = Quaternion.Euler(this.Model.Rotation_Body_Hand_Left.ToVector3());
            GO_Hand_Right.transform.rotation = Quaternion.Euler(this.Model.Rotation_Body_Hand_Right.ToVector3());
            GO_Foot_Left.transform.rotation = Quaternion.Euler(this.Model.Rotation_Body_Foot_Left.ToVector3());
            GO_Foot_Right.transform.rotation = Quaternion.Euler(this.Model.Rotation_Body_Foot_Right.ToVector3());
            GO_Equipment_Left.transform.rotation = Quaternion.Euler(this.Model.Rotation_Hand_Left_Equipment_Left.ToVector3());
            GO_Equipment_Right.transform.rotation = Quaternion.Euler(this.Model.Rotation_Hand_Right_Equipment_Right.ToVector3());

            foreach (Cubon C in this.Model.Head)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                G.transform.parent = GO_Head.transform.FindChild("Model").transform;
                G.transform.localPosition = C.Position.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
                G.renderer.material.color = C.Color;
            }

            foreach (Cubon C in this.Model.Body)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                G.transform.parent = GO_Body.transform.FindChild("Model").transform;
                G.transform.localPosition = C.Position.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
                G.renderer.material.color = C.Color;
            }

            foreach (Cubon C in this.Model.Hand_Left)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                G.transform.parent = GO_Hand_Left.transform.FindChild("Model").transform;
                G.transform.localPosition = C.Position.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
                G.renderer.material.color = C.Color;
            }

            foreach (Cubon C in this.Model.Hand_Right)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                G.transform.parent = GO_Hand_Right.transform.FindChild("Model").transform;
                G.transform.localPosition = C.Position.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
                G.renderer.material.color = C.Color;
            }

            foreach (Cubon C in this.Model.Foot_Left)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                G.transform.parent = GO_Foot_Left.transform.FindChild("Model").transform;
                G.transform.localPosition = C.Position.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
                G.renderer.material.color = C.Color;
            }

            foreach (Cubon C in this.Model.Foot_Right)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                G.transform.parent = GO_Foot_Right.transform.FindChild("Model").transform;
                G.transform.localPosition = C.Position.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
                G.renderer.material.color = C.Color;
            }

            foreach (Cubon C in this.Model.Equipment_Left)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                G.transform.parent = GO_Equipment_Left.transform.FindChild("Model").transform;
                G.transform.localPosition = C.Position.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
                G.renderer.material.color = C.Color;
            }

            foreach (Cubon C in this.Model.Equipment_Right)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                G.transform.parent = GO_Equipment_Right.transform.FindChild("Model").transform;
                G.transform.localPosition = C.Position.ToVector3();
                G.transform.localScale = G.transform.lossyScale;
                G.transform.localRotation = Quaternion.identity;
                G.renderer.material.color = C.Color;
            }
        }
    }
}
