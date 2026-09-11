using CardGameCore.Framework;
using CardGameCore.Framework.Relics;
using Godot;

namespace CardGame;

public partial class RelicUI : Control
{
	public IRelic Relic
	{
		get => _relic;
		set
		{
			_relic = value;
			UpdateTexture();
		}
	}
	private IRelic _relic;

	public TextureRect TextureRect;

	public override void _Ready()
	{
		
		TextureRect = GetNode<TextureRect>("%TextureRect");
	}

	public static RelicUI InstantiateScene()
	{
		return ResourceLoader.Load<PackedScene>("uid://h2mdovtfd7af").Instantiate<RelicUI>();
	}

	private void UpdateTexture()
	{
		if (Relic == null)
			return;
		
		Texture2D texture = ResourceUtil.GetRelicTexture(Relic.Name);
		TextureRect.Texture = texture;
	}
}