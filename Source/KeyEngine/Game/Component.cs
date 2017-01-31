using System;
using System.Collections.Generic;

namespace KeyEngine.Game
{
    [Serializable]
    public class Component
    {
        GameObject m_GameObject = null;

        public GameObject GameObject
        {
            get { return m_GameObject; }
            internal set { m_GameObject = value; }
        }
    }
}
