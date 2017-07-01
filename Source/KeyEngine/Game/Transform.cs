using System;
using System.Collections;

namespace KeyEngine.Game
{
    [Serializable]
    public class Transform : Component//, IEnumerable
    {
        public enum SpaceType : byte
        {
            Local,
            World
        }

        Transform m_Parent = null;

        Core.Vector3 m_LocalPosition = Core.Vector3.Zero;
        Core.Quaternion m_LocalRotation = Core.Quaternion.Identity;
        Core.Vector3 m_LocalScale = Core.Vector3.One;

        Core.Vector3 m_Position = Core.Vector3.Zero;//cached
        Core.Quaternion m_Rotation = Core.Quaternion.Identity;//cached
        Core.Vector3 m_Scale = Core.Vector3.One;//cached
        Core.Vector3 m_LocalEulerAngles = Core.Vector3.Zero;//cached
        Core.Vector3 m_EulerAngles = Core.Vector3.Zero;//cached

        bool m_IsDirty = true;

        public Core.Vector3 LocalPosition
        {
            get { return m_LocalPosition; }
            set { m_LocalPosition = value; }
        }

        public Core.Quaternion LocalRotation
        {
            get { return m_LocalRotation; }
            set { m_LocalRotation = value; }
        }

        public Core.Vector3 LocalScale
        {
            get { return m_LocalScale; }
            set { m_LocalScale = value; }
        }

        public Core.Vector3 EulerAngles
        {
            get { return m_EulerAngles; }
            set { m_EulerAngles = value; }
        }

        public Core.Vector3 Position
        {
            get { return m_Position; }
            set { SetGlobalPosition(value); }
        }

        public Core.Quaternion Rotation
        {
            get { return m_Rotation; }
            set { SetGlobalRotation(value); }
        }

        public Core.Vector3 Scale
        {
            get { return m_Scale; }
        }

        public Core.Vector3 Right
        {
            get { return m_Rotation * Core.Vector3.Right; }
        }

        public Core.Vector3 Up
        {
            get { return m_Rotation * Core.Vector3.Up; }
        }

        public Core.Vector3 Forward
        {
            get { return m_Rotation * Core.Vector3.Forward; }
        }

        public void Translate(Core.Vector3 translation, SpaceType relativeTo = SpaceType.Local)
        {
            switch (relativeTo)
            {
                case SpaceType.Local:
                    {
                        m_LocalPosition += translation;
                    }
                    break;
                case SpaceType.World:
                    {
                        if (m_Parent != null)
                        {
                            m_LocalPosition = (m_Parent.m_LocalRotation.conjugate * (m_LocalPosition + translation - m_Parent.m_LocalPosition)) / m_Parent.m_LocalScale;
                        }
                        else
                        {
                            m_LocalPosition += translation;
                        }
                    }
                    break;
            }

            m_IsDirty = true;
        }

        public void Rotate(Core.Vector3 eulerAngles, SpaceType relativeTo = SpaceType.Local)
        {
            switch (relativeTo)
            {
                case SpaceType.Local:
                    {
                        m_LocalRotation *= Core.Quaternion.EulerAngles(eulerAngles);
                    }
                    break;
                case SpaceType.World:
                    {
                        if (m_Parent != null)
                        {
                            Core.Quaternion scaleAdjust = new Core.Quaternion(m_Parent.m_LocalScale.Y * m_Parent.m_LocalScale.Z, m_Parent.m_LocalScale.X * m_Parent.m_LocalScale.Z, m_Parent.m_LocalScale.X * m_Parent.m_LocalScale.Y, 0f);
                            m_LocalRotation *= (m_Parent.m_LocalRotation.conjugate * Core.Quaternion.EulerAngles(eulerAngles)) * scaleAdjust;
                        }
                        else
                        {
                            m_LocalRotation *= Core.Quaternion.EulerAngles(eulerAngles);
                        }
                    }
                    break;
            }

            m_IsDirty = true;
        }

        public override void Begin() { }

        public override void Update()
        {
            if (m_IsDirty)
            {
                if (m_Parent != null)
                {
                    m_Position = m_Parent.m_LocalPosition + m_Parent.m_LocalRotation * (m_LocalPosition * m_Parent.m_LocalScale);
                    m_Rotation = m_Parent.m_LocalRotation * m_LocalRotation;
                    m_Scale = m_Parent.m_LocalScale * m_LocalScale;

                    m_LocalEulerAngles = Core.Quaternion.EulerAngles(m_LocalRotation);
                    m_EulerAngles = Core.Quaternion.EulerAngles(m_Rotation);
                }
                else
                {
                    m_Position = m_LocalPosition;
                    m_Rotation = m_LocalRotation;
                    m_Scale = m_LocalScale;

                    m_LocalEulerAngles = Core.Quaternion.EulerAngles(m_LocalRotation);
                    m_EulerAngles = m_LocalEulerAngles;
                }

                m_IsDirty = false;
            }
        }

        public override void End() { }

        void SetGlobalPosition(Core.Vector3 value)
        {
            if (m_Parent != null)
            {
                m_LocalPosition = (m_Parent.m_LocalRotation.conjugate * (value - m_Parent.m_LocalPosition)) / m_Parent.m_LocalScale;
            }
            else
            {
                m_LocalPosition = value;
            }

            m_IsDirty = true;
        }

        void SetGlobalRotation(Core.Quaternion value)
        {
            if (m_Parent != null)
            {
                Core.Quaternion scaleAdjust = new Core.Quaternion(m_Parent.m_LocalScale.Y * m_Parent.m_LocalScale.Z, m_Parent.m_LocalScale.X * m_Parent.m_LocalScale.Z, m_Parent.m_LocalScale.X * m_Parent.m_LocalScale.Y, 0f);
                m_LocalRotation = (m_Parent.m_LocalRotation.conjugate * value) * scaleAdjust;
            }
            else
            {
                m_LocalRotation = value;
            }

            m_IsDirty = true;
        }
    }
}
