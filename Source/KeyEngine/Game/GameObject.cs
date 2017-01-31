using System;
using System.Collections.Generic;

namespace KeyEngine.Game
{
    [Serializable]
    public class GameObject
    {
        string m_Name;

        Guid m_ID;

        bool m_Active = true;

        GameObject m_Parent = null;
        List<GameObject> m_Children = new List<GameObject>();
        List<Component> m_Components = new List<Component>();

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public Guid ID
        {
            get { return m_ID; }
        }

        public bool Active
        {
            get { return m_Active; }
            set { m_Active = value; }
        }

        public GameObject Parent
        {
            get { return m_Parent; }
            set { SetParent(value); }
        }

        public GameObject(string name)
        {
            m_Name = name;
            m_ID = Guid.NewGuid();
            m_Active = true;
        }

        public void SetParent(GameObject parent)
        {
            if (parent == null)
                return;

            if (m_Parent != null)
            {
                m_Parent.m_Children.Remove(this);
            }

            m_Parent = parent;
            parent.m_Children.Add(this);
        }

        public void AddChild(GameObject gameObject)
        {
            if (gameObject == null)
                return;

            if (gameObject.m_Parent != null)
            {
                gameObject.m_Parent.m_Children.Remove(gameObject);
            }

            gameObject.m_Parent = this;
            m_Children.Add(gameObject);
        }

        public void RemoveChild(GameObject gameObject)
        {
            gameObject.m_Parent = null;
            m_Children.Remove(gameObject);
        }

        public void RemoveChild(int index)
        {
            m_Children.RemoveAt(index);
        }

        public void AddComponent<T>() where T : Component, new()
        {
            T component = new T();
            component.GameObject = this;

            m_Components.Add(component);
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (var component in m_Components)
            {
                if (component == null)
                    continue;

                if (component is T)
                {
                    return component as T;
                }
            }

            return null;
        }

        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            foreach (var component in m_Components)
            {
                if (component == null)
                    continue;

                if (component is T)
                {
                    yield return component as T;
                }
            }
        }

        public void RemoveComponent<T>()
        {
            foreach (var component in m_Components)
            {
                if (component == null)
                    continue;

                if (component is T)
                {
                    m_Components.Remove(component);
                    return;
                }
            }
        }

        public void RemoveComponents<T>()
        {
            foreach (var component in m_Components)
            {
                if (component == null)
                    continue;

                if (component is T)
                {
                    m_Components.Remove(component);
                }
            }
        }

        internal void RemoveComponent(int index)
        {
            m_Components.RemoveAt(index);
        }

        internal void SwapComponents(int index1, int index2)
        {
            Component temp = m_Components[index1];
            m_Components[index1] = m_Components[index2];
            m_Components[index2] = temp;
        }
    }
}
