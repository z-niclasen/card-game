using System.Collections.Generic;
using System.Linq;
using CardGameCore.Framework.Relics;
using CardGameCore.Impl.Relics;
using Godot;

namespace CardGame;

public partial class RelicDisplay : Control
{
	public RelicCollection RelicCollection
	{
		get => _relicCollection;
		set
		{
			RemoveObservers();
			_relicCollection = value;
			AddObservers();
			InitializeCollection();
		}
	}

	private RelicCollection _relicCollection;
	
	private readonly List<RelicUI> _relics = [];
	
	private HBoxContainer _container;

	public override void _Ready()
	{
		_container = GetNode<HBoxContainer>("%Container");
	}
	
	private void InitializeCollection()
	{
		foreach (IRelic relic in RelicCollection.Relics)
		{
			AddRelic(relic);
		}
	}

	private void AddRelic(IRelic relic)
	{
		RelicUI relicUI = RelicUI.InstantiateScene();
		_container.AddChild(relicUI);
		relicUI.Relic = relic;
		//_relics.Add(relicUI);
		//DrawRelics();
		
	}

	private void RemoveRelic(IRelic relic)
	{
		throw new System.NotImplementedException();
	}

	private void AddObservers()
	{
		RelicCollection?.OnRelicAdded += AddRelic;
	}

	private void DrawRelics()
	{
		GD.Print($"Container size {_container.GetRect().Size}");
		for (int i = 0; i < _relics.Count; i++)
		{
			RelicUI relic = _relics[i];

			int relicWidth = relic.TextureRect.Texture.GetWidth();

			int sep = 20;
			
			int newX = (relicWidth + sep) * i ;
			
			//relic.Position = Position with {X = newX};
		}
	}

	private void RemoveObservers()
	{
		RelicCollection?.OnRelicRemoved += RemoveRelic;
	}
}