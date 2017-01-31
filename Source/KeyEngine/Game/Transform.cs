using System;
using System.Collections;

namespace KeyEngine.Game
{
    [Serializable]
    public class Transform : Component//, IEnumerable
    {
        Transform m_Parent = null;

        Core.Vector3 m_LocalPosition = Core.Vector3.Zero;

        Core.Quaternion m_LocalRotation = Core.Quaternion.Identity;

        Core.Vector3 m_LocalScale = Core.Vector3.One;

        Core.Vector3 m_EulerAngles = Core.Vector3.Zero;//cached euler angles


        public Core.Vector3 EulerAngles
        {
            get { return m_EulerAngles; }
            set { m_EulerAngles = value; }
        }
    }
}
