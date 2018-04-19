using System;
using System.Collections.Generic;

namespace KeyEngine.Game
{
	[EditorIcon(Core.ResourceNames.IconGameObject)]
	public class GameObject : Core.IIdentifiable
	{
		string name;

		Guid id = Guid.NewGuid();

		bool active = true;

		GameObject parent = null;
		List<GameObject> children = new List<GameObject>();
		List<Component> components = new List<Component>();

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public Guid ID
		{
			get { return id; }
		}

		public bool Active
		{
			get { return active; }
			set { active = value; }
		}

		public GameObject Parent
		{
			get { return parent; }
			set { SetParent(value); }
		}

		public GameObject(string name)
		{
			this.name = name;
			id = Guid.NewGuid();
			active = true;
		}

		public void SetParent(GameObject parent)
		{
			if (this.parent != null)
			{
				this.parent.children.Remove(this);
			}

			this.parent = parent;

			if (parent != null)
			{
				parent.children.Add(this);
			}
		}

		public void AddChild(GameObject gameObject)
		{
			if (gameObject == null)
				return;

			if (gameObject.parent != null)
			{
				gameObject.parent.children.Remove(gameObject);
			}

			gameObject.parent = this;
			children.Add(gameObject);
		}

		public void RemoveChild(GameObject gameObject)
		{
			gameObject.parent = null;
			children.Remove(gameObject);
		}

		public void RemoveChild(int index)
		{
			children.RemoveAt(index);
		}

		public void AddComponent<T>() where T : Component, new()
		{
			T component = new T();
			component.GameObject = this;

			components.Add(component);
		}

		public T GetComponent<T>() where T : Component
		{
			foreach (var component in components)
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
			foreach (var component in components)
			{
				if (component == null)
					continue;

				if (component is T)
				{
					yield return component as T;
				}
			}
		}

		public T GetComponentInChildren<T>() where T : Component
		{
			if (children.Count == 0)
			{
				return null;
			}

			foreach (var child in children)
			{
				var component = child.GetComponent<T>();
				if (component != null)
				{
					return component;
				}

				component = child.GetComponentInChildren<T>();
				if (component != null)
				{
					return component;
				}
			}

			return null;
		}

		public IEnumerable<T> GetComponentsInChildren<T>() where T : Component
		{
			if (children.Count == 0)
			{
				yield break;
			}

			foreach (var child in children)
			{
				var components = child.GetComponents<T>();
				if (components != null)
				{
					foreach (var component in components)
					{
						yield return component;
					}
				}

				components = child.GetComponentsInChildren<T>();
				if (components != null)
				{
					foreach (var component in components)
					{
						yield return component;
					}
				}
			}
		}

		public void RemoveComponent<T>()
		{
			foreach (var component in components)
			{
				if (component == null)
					continue;

				if (component is T)
				{
					components.Remove(component);
					return;
				}
			}
		}

		public void RemoveComponents<T>()
		{
			foreach (var component in components)
			{
				if (component == null)
					continue;

				if (component is T)
				{
					components.Remove(component);
				}
			}
		}

		public GameObject Clone()
		{
			return null;//TODO: implement DeepClone!!!
		}

		public void Dispose()
		{
			foreach (var component in components)
			{
				component.Dispose();
			}

			foreach (var child in children)
			{
				child.Dispose();
			}

			SetParent(null);
		}

		internal void RemoveComponent(int index)
		{
			components.RemoveAt(index);
		}

		internal void RemoveComponent(Component component)
		{
			if (component == null)
				return;

			components.Remove(component);
		}

		internal void SwapComponents(int index1, int index2)
		{
			Component temp = components[index1];
			components[index1] = components[index2];
			components[index2] = temp;
		}
	}
}
