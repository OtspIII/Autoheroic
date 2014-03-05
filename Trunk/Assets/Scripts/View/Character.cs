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

        public Position3 Rotation_Body { get; set; }
        public Position3 Rotation_Body_Head { get; set; }
        public Position3 Rotation_Body_Hand_Left { get; set; }
        public Position3 Rotation_Body_Hand_Right { get; set; }
        public Position3 Rotation_Body_Foot_Left { get; set; }
        public Position3 Rotation_Body_Foot_Right { get; set; }

        public List<Cub.Cubon> Head { get; set; }
        public List<Cub.Cubon> Body { get; set; }
        public List<Cub.Cubon> Hand_Left { get; set; }
        public List<Cub.Cubon> Hand_Right { get; set; }
        public List<Cub.Cubon> Foot_Left { get; set; }
        public List<Cub.Cubon> Foot_Right { get; set; }
    }

    public class Character : MonoBehaviour
    {
        public Cub.Class Class; //Unfortunately Unity sucks sucks sucks (Trust me I can do more) at here thus I have to use member instead of Property to setup the parameter for the constructor
        public Character_Model Model { get; set; }

        public void Start()
        {
            Cubing();
        }

        private void Cubing()
        {
            this.Model = Library.Get_Character_Model(this.Class);

            GameObject GO_Head = this.gameObject.transform.FindChild("Head").gameObject;
            GameObject GO_Body = this.gameObject.transform.FindChild("Body").gameObject;
            GameObject GO_Hand_Left = this.gameObject.transform.FindChild("Hand_Left").gameObject;
            GameObject GO_Hand_Right = this.gameObject.transform.FindChild("Hand_Right").gameObject;
            GameObject GO_Foot_Left = this.gameObject.transform.FindChild("Foot_Left").gameObject;
            GameObject GO_Foot_Right = this.gameObject.transform.FindChild("Foot_Right").gameObject;

            GO_Body.transform.position = this.Model.Position_Body.ToVector3();
            GO_Head.transform.localPosition = this.Model.Position_Body.ToVector3() + this.Model.Position_Body_Head.ToVector3();
            GO_Hand_Left.transform.localPosition = this.Model.Position_Body.ToVector3() + this.Model.Position_Body_Hand_Left.ToVector3();
            GO_Hand_Right.transform.localPosition = this.Model.Position_Body.ToVector3() + this.Model.Position_Body_Hand_Right.ToVector3();
            GO_Foot_Left.transform.localPosition = this.Model.Position_Body.ToVector3() + this.Model.Position_Body_Foot_Left.ToVector3();
            GO_Foot_Right.transform.localPosition = this.Model.Position_Body.ToVector3() + this.Model.Position_Body_Foot_Right.ToVector3();

            GO_Body.transform.rotation = Quaternion.Euler(this.Model.Position_Body.ToVector3());
            GO_Head.transform.rotation = Quaternion.Euler(this.Model.Position_Body.ToVector3() + this.Model.Rotation_Body_Head.ToVector3());
            GO_Hand_Left.transform.rotation = Quaternion.Euler(this.Model.Position_Body.ToVector3() + this.Model.Rotation_Body_Hand_Left.ToVector3());
            GO_Hand_Right.transform.rotation = Quaternion.Euler(this.Model.Position_Body.ToVector3() + this.Model.Rotation_Body_Hand_Right.ToVector3());
            GO_Foot_Left.transform.rotation = Quaternion.Euler(this.Model.Position_Body.ToVector3() + this.Model.Rotation_Body_Foot_Left.ToVector3());
            GO_Foot_Right.transform.rotation = Quaternion.Euler(this.Model.Position_Body.ToVector3() + this.Model.Rotation_Body_Foot_Right.ToVector3());

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
        }
    }
}
