using System;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.DataStructures;

namespace Macrocosm.Common.Systems.Connectors;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ConveyorData : ITileData
{
    /// <summary>
    /// <code>
    ///  bits 0-3: pipe mask R/G/B/Y
    ///  bits 4-5: endpoint mode, 0=None, 1=Inlet, 2=Outlet, 3=reserved
    ///  bit 6: attachment present
    ///  bit 7: attachment type, 0=Dropper, 1=Hopper
    ///  bits 8-9: attachment rotation, 0..3
    ///  bits 10-15: reserved for future use
    /// </code>
    /// </summary>
    private ushort data;

    private const ushort RedPipeBit = 1 << 0;
    private const ushort GreenPipeBit = 1 << 1;
    private const ushort BluePipeBit = 1 << 2;
    private const ushort YellowPipeBit = 1 << 3;
    private const ushort PipeMask = RedPipeBit | GreenPipeBit | BluePipeBit | YellowPipeBit;

    private const int EndpointShift = 4;
    private const ushort EndpointMask = 0b11 << EndpointShift;

    private const ushort AttachmentBit = 1 << 6;
    private const ushort AttachmentHopperBit = 1 << 7;

    private const int AttachmentRotationShift = 8;
    private const ushort AttachmentRotationMask = 0b11 << AttachmentRotationShift;

    private enum ConveyorEndpoint : byte
    {
        None = 0,
        Inlet = 1,
        Outlet = 2,
    }

    public ConveyorData()
    {
        data = 0;
    }

    public ConveyorData(byte packed)
    {
        data = packed;
    }

    public ConveyorData(ushort packed)
    {
        data = packed;
    }

    public ConveyorData(bool red = false, bool green = false, bool blue = false, bool yellow = false, bool outlet = false, bool inlet = false) : this()
    {
        RedPipe = red;
        GreenPipe = green;
        BluePipe = blue;
        YellowPipe = yellow;
        Outlet = outlet;
        Inlet = inlet;
    }

    public readonly ushort Packed => data;

    public bool RedPipe { readonly get => HasFlag(RedPipeBit); set => SetFlag(RedPipeBit, value); }
    public bool GreenPipe { readonly get => HasFlag(GreenPipeBit); set => SetFlag(GreenPipeBit, value); }
    public bool BluePipe { readonly get => HasFlag(BluePipeBit); set => SetFlag(BluePipeBit, value); }
    public bool YellowPipe { readonly get => HasFlag(YellowPipeBit); set => SetFlag(YellowPipeBit, value); }
    public bool Outlet { readonly get => AnyPipe && Endpoint == ConveyorEndpoint.Outlet; set => Endpoint = value && AnyPipe ? ConveyorEndpoint.Outlet : ConveyorEndpoint.None; }
    public bool Inlet { readonly get => AnyPipe && Endpoint == ConveyorEndpoint.Inlet; set => Endpoint = value && AnyPipe ? ConveyorEndpoint.Inlet : ConveyorEndpoint.None; }
    public bool Attachment { readonly get => HasFlag(AttachmentBit); set { SetFlag(AttachmentBit, value); if (!value) AttachmentRotation = 0; } }
    public bool AttachmentIsHopper { readonly get => HasFlag(AttachmentHopperBit); set => SetFlag(AttachmentHopperBit, value); }
    public byte AttachmentRotation
    {
        readonly get => (byte)((data & AttachmentRotationMask) >> AttachmentRotationShift);
        set
        {
            byte masked = (byte)(value & 0b11);
            data = (ushort)((data & ~AttachmentRotationMask) | (masked << AttachmentRotationShift));
        }
    }

    public bool Dropper { readonly get => Attachment && !AttachmentIsHopper; set { Attachment = value; AttachmentIsHopper = false; if (!value) AttachmentRotation = 0; } }
    public bool Hopper { readonly get => Attachment && AttachmentIsHopper; set { Attachment = value; AttachmentIsHopper = value; if (!value) AttachmentRotation = 0; } }

    public readonly bool AnyPipe => (data & PipeMask) != 0;
    public readonly int PipeCount => (RedPipe ? 1 : 0) + (GreenPipe ? 1 : 0) + (BluePipe ? 1 : 0) + (YellowPipe ? 1 : 0);

    private ConveyorEndpoint Endpoint
    {
        readonly get => (ConveyorEndpoint)((data & EndpointMask) >> EndpointShift);
        set => data = (ushort)((data & ~EndpointMask) | ((ushort)value << EndpointShift));
    }

    public readonly bool IsValidForConveyorNode(ConveyorPipeType? pipe = null)
    {
        bool hasPipe = pipe.HasValue ? HasPipe(pipe.Value) : AnyPipe;
        return (hasPipe && (Inlet || Outlet)) || Attachment;
    }

    public readonly bool HasPipe(ConveyorPipeType type)
    {
        return type switch
        {
            ConveyorPipeType.RedPipe => RedPipe,
            ConveyorPipeType.GreenPipe => GreenPipe,
            ConveyorPipeType.BluePipe => BluePipe,
            ConveyorPipeType.YellowPipe => YellowPipe,
            _ => false,
        };
    }

    public void SetPipe(ConveyorPipeType type)
    {
        switch (type)
        {
            case ConveyorPipeType.RedPipe: RedPipe = true; break;
            case ConveyorPipeType.GreenPipe: GreenPipe = true; break;
            case ConveyorPipeType.BluePipe: BluePipe = true; break;
            case ConveyorPipeType.YellowPipe: YellowPipe = true; break;
            default: break;
        }
    }

    public void ClearPipe(ConveyorPipeType type)
    {
        switch (type)
        {
            case ConveyorPipeType.RedPipe: RedPipe = false; break;
            case ConveyorPipeType.GreenPipe: GreenPipe = false; break;
            case ConveyorPipeType.BluePipe: BluePipe = false; break;
            case ConveyorPipeType.YellowPipe: YellowPipe = false; break;
            default: break;
        }

        if (!AnyPipe)
            Endpoint = ConveyorEndpoint.None;
    }

    public void ClearAll()
    {
        data = 0;
    }

    private readonly bool HasFlag(ushort flag)
    {
        return (data & flag) != 0;
    }

    private void SetFlag(ushort flag, bool value)
    {
        if (value)
            data |= flag;
        else
            data = (ushort)(data & ~flag);
    }
}
