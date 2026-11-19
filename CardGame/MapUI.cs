using System.Collections.Generic;
using CardGameCore.Impl;
using CardGameCore.Impl.Runs;
using Godot;
using Map = CardGameCore.Map;

namespace CardGame;

public partial class MapUI : MarginContainer
{
	private readonly Map _map = Run.Instance.Map;
	private readonly Dictionary<Map.MapPosition, TextureButton> _positionToButton = new();

	private VBoxContainer _vbox;
	private MarginContainer _vboxParent;

	public override void _Ready()
	{
		_vbox = GetNode<VBoxContainer>("%VBox");
		_vboxParent = GetNode<MarginContainer>("%MarginContainer");
		DrawMap();
		DrawEdges();
	}

	private void DrawMap()
	{
		foreach (IEnumerable<Map.MapPosition> layer in _map.GetMapLayers())
		{
			HBoxContainer layerContainer = new();
			layerContainer.AddThemeConstantOverride("separation", 200);
			_vbox.AddChild(layerContainer);
			_vbox.MoveChild(layerContainer, 0);

			int currentIndex = 0;
			foreach (Map.MapPosition position in layer)
			{
				while (currentIndex < position.Index)
				{
					TextureButton empty = new();
					empty.CustomMinimumSize = new Vector2(64, 64);
					empty.Disabled = true;
					layerContainer.AddChild(empty);
					currentIndex++;
				}
				
				TextureButton button = new();
				button.TextureNormal = ResourceUtil.GetEncounterTypeTexture(EncounterType.Combat);
				button.ZIndex = 10;
				layerContainer.AddChild(button);
				
				_positionToButton.Add(position, button);
				GD.Print($"-------- {position}");
				foreach (Map.MapEdge edge in _map.GetMapEdges(position))
					GD.Print(edge);
				GD.Print();

				currentIndex++;
			}
		}
	}

	private async void DrawEdges()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		
		foreach (Map.MapEdge edge in _map.GetAllMapEdges())
		{
			//GD.Print($"({edge.From.Layer}, {edge.From.Index}) - ({edge.To.Layer}, {edge.To.Index})");
			TextureButton fromButton = _positionToButton[edge.From];
			float centerOffset = fromButton.Size.X / 2;
			
			Vector2 globalFrom = fromButton.GetGlobalPosition();
			globalFrom = new Vector2(globalFrom.X + centerOffset, globalFrom.Y + centerOffset);
			Vector2 localFrom = _vboxParent.GetGlobalTransform().AffineInverse() * globalFrom;
			
			TextureButton toButton = _positionToButton[edge.To];
			Vector2 globalTo = toButton.GetGlobalPosition();
			globalTo = new Vector2(globalTo.X + centerOffset, globalTo.Y + centerOffset);
			Vector2 localTo = _vboxParent.GetGlobalTransform().AffineInverse() * globalTo;

			Line2D line = new();
			_vboxParent.AddChild(line);
			line.AddPoint(localFrom);
			line.AddPoint(localTo);
		}
	}
}