//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace AssemblyCSharp
{
	[XmlRoot("SaveFile")]
	public class SaveFile
	{

		[XmlAttribute("text")]
		public string Text = "A";
		[XmlAttribute("int")]
		public int Int = 1;
		[XmlAttribute("float")]
		public float Float = 1.5f;
		List<string> Stuff = new List<string>{"B","C","D"};

			public SaveFile ()
			{
			}
	}
}
