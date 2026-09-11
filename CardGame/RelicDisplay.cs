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
	
	private HBoxContainer _container;

	public override void _Ready()
	{
		_container = GetNode<HBoxContainer>("%Container");
	}
	
	private void InitializeCollection()
	{
		foreach (IRelic relic in RelicCollection.Relics)
			AddRelic(relic);
	}

	private void AddRelic(IRelic relic)
	{
		RelicUI relicUI = RelicUI.InstantiateScene();
		relicUI.Relic = relic;
		_container.AddChild(relicUI);
	}

	private void RemoveRelic(IRelic relic)
	{
		throw new System.NotImplementedException();
	}

	private void AddObservers()
	{
		RelicCollection?.OnRelicAdded += AddRelic;
	}

	private void RemoveObservers()
	{
		RelicCollection?.OnRelicRemoved += RemoveRelic;
	}
}