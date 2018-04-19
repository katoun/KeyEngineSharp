using System;
using System.Collections.Generic;

namespace KeyEngine.Game
{
	[EditorIcon(Core.ResourceNames.IconComponent)]
	public class Component : Core.IIdentifiable
	{
		protected Guid id = Guid.NewGuid();

		protected bool active = true;
		protected GameObject gameObject = null;

		public Guid ID
		{
			get { return id; }
		}

		public bool Active
		{
			get { return active; }
			set { active = value; }
		}

		public GameObject GameObject
		{
			get { return gameObject; }
			internal set { gameObject = value; }
		}

		public virtual void Begin() { }
		public virtual void Update() { }
		public virtual void End() { }

		public void Dispose()
		{
			if (gameObject != null)
			{
				gameObject.RemoveComponent(this);
			}
		}

		internal class Serializer: Serialization.Serializer<Component>
		{
			public override void Serialize(ref Component obj, Serialization.ArchiveMode mode)
			{
				var id = obj.ID;
				//TODO!!!
			}
		}
	}
}
