using System;
using System.Collections;

namespace KeyEngine.Game
{
	public class Transform : Component//, IEnumerable
	{
		public enum SpaceType : byte
		{
			Local,
			World
		}

		Transform parent = null;

		Core.Vector3 localPosition = Core.Vector3.Zero;
		Core.Quaternion localRotation = Core.Quaternion.Identity;
		Core.Vector3 localScale = Core.Vector3.One;
		Core.Vector3 localEulerAngles = Core.Vector3.Zero;//cached

		Core.Vector3 position = Core.Vector3.Zero;//cached
		Core.Quaternion rotation = Core.Quaternion.Identity;//cached
		Core.Vector3 scale = Core.Vector3.One;//cached
		Core.Vector3 eulerAngles = Core.Vector3.Zero;//cached

		bool isDirty = true;

		public Core.Vector3 LocalPosition
		{
			get { return localPosition; }
			set { localPosition = value; }
		}

		public Core.Quaternion LocalRotation
		{
			get { return localRotation; }
			set { localRotation = value; }
		}

		public Core.Vector3 LocalScale
		{
			get { return localScale; }
			set { localScale = value; }
		}

		public Core.Vector3 EulerAngles
		{
			get { return eulerAngles; }
			set { eulerAngles = value; }
		}

		public Core.Vector3 Position
		{
			get { return position; }
			set { SetGlobalPosition(value); }
		}

		public Core.Quaternion Rotation
		{
			get { return rotation; }
			set { SetGlobalRotation(value); }
		}

		public Core.Vector3 Scale
		{
			get { return scale; }
		}

		public Core.Vector3 Right
		{
			get { return rotation * Core.Vector3.Right; }
		}

		public Core.Vector3 Up
		{
			get { return rotation * Core.Vector3.Up; }
		}

		public Core.Vector3 Forward
		{
			get { return rotation * Core.Vector3.Forward; }
		}

		public void Translate(Core.Vector3 translation, SpaceType relativeTo = SpaceType.Local)
		{
			switch (relativeTo)
			{
				case SpaceType.Local:
					{
						localPosition += translation;
					}
					break;
				case SpaceType.World:
					{
						if (parent != null)
						{
							localPosition = (parent.localRotation.conjugate * (localPosition + translation - parent.localPosition)) / parent.localScale;
						}
						else
						{
							localPosition += translation;
						}
					}
					break;
			}

			isDirty = true;
		}

		public void Rotate(Core.Vector3 eulerAngles, SpaceType relativeTo = SpaceType.Local)
		{
			switch (relativeTo)
			{
				case SpaceType.Local:
					{
						localRotation *= Core.Quaternion.EulerAngles(eulerAngles);
					}
					break;
				case SpaceType.World:
					{
						if (parent != null)
						{
							Core.Quaternion scaleAdjust = new Core.Quaternion(parent.localScale.Y * parent.localScale.Z, parent.localScale.X * parent.localScale.Z, parent.localScale.X * parent.localScale.Y, 0f);
							localRotation *= (parent.localRotation.conjugate * Core.Quaternion.EulerAngles(eulerAngles)) * scaleAdjust;
						}
						else
						{
							localRotation *= Core.Quaternion.EulerAngles(eulerAngles);
						}
					}
					break;
			}

			isDirty = true;
		}

		public override void Begin() { }

		public override void Update()
		{
			if (isDirty)
			{
				if (parent != null)
				{
					position = parent.localPosition + parent.localRotation * (localPosition * parent.localScale);
					rotation = parent.localRotation * localRotation;
					scale = parent.localScale * localScale;

					localEulerAngles = Core.Quaternion.EulerAngles(localRotation);
					eulerAngles = Core.Quaternion.EulerAngles(rotation);
				}
				else
				{
					position = localPosition;
					rotation = localRotation;
					scale = localScale;

					localEulerAngles = Core.Quaternion.EulerAngles(localRotation);
					eulerAngles = localEulerAngles;
				}

				isDirty = false;
			}
		}

		public override void End() { }

		void SetGlobalPosition(Core.Vector3 value)
		{
			if (parent != null)
			{
				localPosition = (parent.localRotation.conjugate * (value - parent.localPosition)) / parent.localScale;
			}
			else
			{
				localPosition = value;
			}

			isDirty = true;
		}

		void SetGlobalRotation(Core.Quaternion value)
		{
			if (parent != null)
			{
				Core.Quaternion scaleAdjust = new Core.Quaternion(parent.localScale.Y * parent.localScale.Z, parent.localScale.X * parent.localScale.Z, parent.localScale.X * parent.localScale.Y, 0f);
				localRotation = (parent.localRotation.conjugate * value) * scaleAdjust;
			}
			else
			{
				localRotation = value;
			}

			isDirty = true;
		}
	}
}
