﻿using System.Collections.Generic;
using UnityEngine;

namespace Vexe.Runtime.Types.Examples
{
	public class DefaultValueExample : BetterBehaviour
	{
		public int defIntX = 10;

		[Default("Hello")]
		public string stringField;

		[Default("World")]
		public string StringProperty { get; set; }

		[Default(10)]
		public int intField;

		[Default(15)]
		public int IntProperty { get; set; }

		[Default(10.75f)]
		public float floatField;

		[Default(15.25f)]
		public float FloatProperty { get; set; }

		[Default(true)]
		public bool BoolProperty { get; set; }

		[Default(1f, 2f)]
		public Vector2 vector2;

		[Default(0f, 2f, 4f)]
		public Vector3 vector3 { get; set; }

		[Default(Enum = (int)KeyCode.Space)]
		public KeyCode keyJump;

		[Default(new[] { 1, 2, 3 })]
		public int[] intArray;

		[Default(new[] { 1.5f, 2.5f, 3.2f })]
		public float[] FloatArray { get; set; }

		[Default(new[] { true, true, false, true })]
		public bool[] boolArray;

		[Default(new[] { "hello", "world" })]
		public string[] StringArray { get; set; }
	}
}