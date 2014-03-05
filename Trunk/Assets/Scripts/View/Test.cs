using System;
using UnityEngine;

namespace Cub.View
{
    public class Test : MonoBehaviour
    {
        public void Awake()
        {
            Cub.View.Library.Initialization();
        }

        public void Start()
        {
            /*
            Cub.View.Model.Character C = new Model.Character();
            C.Head = new System.Collections.Generic.List<Cubon>();
            C.Head.Add(new Cubon(new Color32(255, 0, 0, 255), new Position3(0, 0, 0)));
            C.Head.Add(new Cubon(new Color32(0, 255, 0, 255), new Position3(0, 1, 0)));
            C.Body = new System.Collections.Generic.List<Cubon>();
            C.Body.Add(new Cubon(new Color32(255, 0, 0, 255), new Position3(0, 0, 0)));
            C.Hand_Left = new System.Collections.Generic.List<Cubon>();
            C.Hand_Left.Add(new Cubon(new Color32(255, 0, 0, 255), new Position3(0, 0, 0)));
            C.Hand_Right = new System.Collections.Generic.List<Cubon>();
            C.Hand_Right.Add(new Cubon(new Color32(255, 0, 0, 255), new Position3(0, 0, 0)));
            C.Foot_Left = new System.Collections.Generic.List<Cubon>();
            C.Foot_Left.Add(new Cubon(new Color32(255, 0, 0, 255), new Position3(0, 0, 0)));
            C.Foot_Right = new System.Collections.Generic.List<Cubon>();
            C.Foot_Right.Add(new Cubon(new Color32(255, 0, 0, 255), new Position3(0, 0, 0)));

            C.Node_Body_Foot_Left = new Position3(0, 0, 0);
            C.Node_Body_Foot_Right = new Position3(0, 0, 0);
            C.Node_Body_Hand_Left = new Position3(0, 0, 0);
            C.Node_Body_Hand_Right = new Position3(0, 0, 0);
            C.Node_Body_Head = new Position3(0, 0, 0);

            Cub.Tool.Xml.Serialize(C, "Data/Model_Character.xml");
             * */

            

            

            /*

            Cub.View.Character Knight = KGO.GetComponent<Cub.View.Character>();
            
            foreach (Cubon C in Knight.Head)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                G.transform.parent = CH_H.transform;
                G.transform.localPosition = C.Position.ToVector3();
                G.transform.rotation = Quaternion.identity;
                G.renderer.material.color = C.Color;
            }

            foreach (Cubon C in Knight.Body)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                G.transform.parent = CH_B.transform;
                G.transform.localPosition = C.Position.ToVector3();
                G.transform.rotation = Quaternion.identity;
                G.renderer.material.color = C.Color;
            }

            foreach (Cubon C in Knight.Hand_Left)
            {
                GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
                G.transform.parent = CH_HL.transform;
                G.transform.localPosition = C.Position.ToVector3();
                G.transform.rotation = Quaternion.identity;
                G.renderer.material.color = C.Color;
            }
             * */
            /*

            Cub.View.Model.Character Knight = Library.Get_Model_Character(Class.Knight);

            GameObject CH = new GameObject("Character");
            CH.transform.position = Vector3.zero;

            GameObject CH_H = new GameObject("Head");
            CH_H.transform.parent = CH.transform;
            CH_H.transform.localPosition = Knight.Node_Body_Head.ToVector3();

            Debug.Log(Knight.Head.Count);

            foreach (Cubon C in Knight.Head)
            {
                GameObject G = Instantiate(Library.Get_Cube(C.Cube), Vector3.zero, Quaternion.identity) as GameObject;
                G.renderer.material.color = new Color(1.0F, 0.0F, 0.0F, 1.0F);
                G.transform.parent = CH_H.transform;
                G.transform.localPosition = C.Position.ToVector3();
            }
             * */

            /*
            Cub.View.Model.Character C = new Model.Character();
            C.Head = new System.Collections.Generic.List<Cubon>();
            C.Head.Add(new Cubon(new Position3(0, 0, 0), Cube.Steel));
            C.Head.Add(new Cubon(new Position3(0, 1, 0), Cube.Steel));
            C.Body = new System.Collections.Generic.List<Cubon>();
            C.Body.Add(new Cubon(new Position3(0, 0, 0), Cube.Carbon));            
            C.Hand_Left = new System.Collections.Generic.List<Cubon>();
            C.Hand_Left.Add(new Cubon(new Position3(0, 0, 0), Cube.Steel));
            C.Hand_Right = new System.Collections.Generic.List<Cubon>();
            C.Hand_Right.Add(new Cubon(new Position3(0, 0, 0), Cube.Steel));
            C.Foot_Left = new System.Collections.Generic.List<Cubon>();
            C.Foot_Left.Add(new Cubon(new Position3(0, 0, 0), Cube.Steel));
            C.Foot_Right = new System.Collections.Generic.List<Cubon>();
            C.Foot_Right.Add(new Cubon(new Position3(0, 0, 0), Cube.Steel));
            Cub.Tool.Xml.Serialize(C, "Data/Model_Character.xml");
             * */

            //Cub.View.Model.Character Knight = Cub.Tool.Xml.Deserialize(typeof(Cub.View.Model.Character), "Data/A.xml") as Cub.View.Model.Character;
        }
    }
}
