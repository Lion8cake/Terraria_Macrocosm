using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI.Chat;
using System.IO;
using Terraria.ModLoader.IO;

namespace Macrocosm.Common.Systems.Power;

public abstract class GeneratorTE : MachineTE
{
    public float GeneratedPower { get; set; }
    public float MaxGeneratedPower { get; set; }

    public override void UpdatePowerState()
    {
        if (IsOnFrame && GeneratedPower <= 0f)
            TurnOff(automatic: true);
        else if (!IsOnFrame && GeneratedPower > 0f && IsEnabledByPlayer)
            TurnOn(automatic: true);
    }

    public override Color DisplayColor => Color.LimeGreen;
    public override string GetPowerInfo() => $"{Language.GetText($"Mods.Macrocosm.Machines.Common.PowerInfo.Generator").Format($"{GeneratedPower:F2}", $"{MaxGeneratedPower:F2}")}";
    public override void DrawMachinePowerInfo(SpriteBatch spriteBatch, Vector2 basePosition, Color lightColor)
    {
        string power = Language.GetText($"Mods.Macrocosm.Machines.Common.PowerInfo.Simple").Format($"{GeneratedPower:F2}");

        Vector2 textSize = FontAssets.MouseText.Value.MeasureString(power);
        Vector2 position = new Vector2(basePosition.X + (MachineTile.Width * 16f / 2f) - (textSize.X / 2f) + 8f, basePosition.Y - 22f) - Main.screenPosition;
        Color color = DisplayColor;

        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.DeathText.Value, power, position - new Vector2(power.Length, 0), color, 0f, Vector2.Zero, Vector2.One * 0.4f, spread: 1.5f);
    }

    protected virtual void GeneratorNetSend(BinaryWriter writer) { }
    protected virtual void GeneratorNetReceive(BinaryReader reader) { }
    protected virtual void GeneratorSaveData(TagCompound tag) { }
    protected virtual void GeneratorLoadData(TagCompound tag) { }

    public sealed override void MachineNetSend(BinaryWriter writer)
    {
        writer.Write(GeneratedPower);
        writer.Write(MaxGeneratedPower);
        GeneratorNetSend(writer);
    }

    public sealed override void MachineNetReceive(BinaryReader reader)
    {
        GeneratedPower = reader.ReadSingle();
        MaxGeneratedPower = reader.ReadSingle();
        GeneratorNetReceive(reader);
    }

    public sealed override void MachineSaveData(TagCompound tag)
    {
        if (GeneratedPower != 0f)
            tag[nameof(GeneratedPower)] = GeneratedPower;
        if (MaxGeneratedPower != 0f)
            tag[nameof(MaxGeneratedPower)] = MaxGeneratedPower;
        GeneratorSaveData(tag);
    }

    public sealed override void MachineLoadData(TagCompound tag)
    {
        if (tag.ContainsKey(nameof(GeneratedPower)))
            GeneratedPower = tag.GetFloat(nameof(GeneratedPower));
        if (tag.ContainsKey(nameof(MaxGeneratedPower)))
            MaxGeneratedPower = tag.GetFloat(nameof(MaxGeneratedPower));
        GeneratorLoadData(tag);
    }
}
