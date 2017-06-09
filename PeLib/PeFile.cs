using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace YTY.PeLib
{
  public class PeFile
  {
    private const uint SIGNATURE = 0x4550;

    public MachineType Machine { get; set; }

    public uint TimeDateStamp { get; set; }

    public Characteristics Characteristics { get; set; }

    public PeFormat Magic { get; set; }

    public byte MajorLinkerVersion { get; set; }

    public byte MinorLinkerVersion { get; set; }

    public uint SizeOfCode { get; set; }

    public uint SizeOfInitializedData { get; set; }

    public uint SizeOfUninitializedData { get; set; }

    public uint AddressOfEntryPoint { get; set; }

    public uint BaseOfCode { get; set; }

    public uint BaseOfData { get; set; }

    public void Load(string fileName)
    {
      using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
      {
        using (var br = new BinaryReader(fs))
        {
          fs.Seek(0x3c, SeekOrigin.Begin);
          var lfanew = br.ReadInt32();
          fs.Seek(lfanew, SeekOrigin.Begin);
          if (br.ReadUInt32() != SIGNATURE)
          {
            throw new BadImageFormatException();
          }
          Machine = (MachineType)br.ReadUInt16();
          var numberOfSections = br.ReadUInt16();
          TimeDateStamp = br.ReadUInt32();
          var pointerToSymbolTable = br.ReadUInt32();
          var numberOfSymbols = br.ReadUInt32();
          var sizeOfOptionalHeader = br.ReadUInt16();
          Characteristics = (Characteristics)br.ReadUInt16();
          Magic = (PeFormat)br.ReadUInt16();
          MajorLinkerVersion = br.ReadByte();
          MinorLinkerVersion = br.ReadByte();
          SizeOfCode = br.ReadUInt32();
          SizeOfInitializedData = br.ReadUInt32();
          SizeOfUninitializedData = br.ReadUInt32();
          AddressOfEntryPoint = br.ReadUInt32();
          BaseOfCode = br.ReadUInt32();
          if (Magic == PeFormat.Pe32)
          {
            BaseOfData = br.ReadUInt32();
          }
        }
      }
    }
  }

  public enum MachineType : ushort
  {
    Unknown = 0x0,
    Am33 = 0x1d3,
    Amd64 = 0x8664,
    Arm = 0x1c0,
    Arm64 = 0xaa64,
    ArmNt = 0x1c4,
    Ebc = 0xebc,
    I386 = 0x14c,
    Ia64 = 0x200,
    M32R = 0x9041,
    Mips16 = 0x266,
    MipsFpu = 0x366,
    MipsFpu16 = 0x466,
    PowerPc = 0x1f0,
    PowerPcFp = 0x1f1,
    R4000 = 0x166,
    RiscV32 = 0x5032,
    RiscV64 = 0x5064,
    RiscV128 = 0x5128,
    Sh3 = 0x1a2,
    Sh3Dsp = 0x1a3,
    Sh4 = 0x1a6,
    Sh5 = 0x1a8,
    Thumb = 0x1c2,
    WceMipsV2 = 0x169,
  }

  [Flags]
  public enum Characteristics : uint
  {
    RelocsStripped = 1 << 0,
    ExecutableImage = 1 << 1,
    LineNumsStripped = 1 << 2,
    LocalSymsStripped = 1 << 3,
    AggressiveWsTrim = 1 << 4,
    LargeAddressAware = 1 << 5,
    // 1<<6
    BytesReservedLo = 1 << 7,
    Is32BitMachine = 1 << 8,
    DebugStripped = 1 << 9,
    RemovableRunFromSwap = 1 << 10,
    NetRunFromSwap = 1 << 11,
    System = 1 << 12,
    Dll = 1 << 13,
    UpSystemOnly = 1 << 14,
    BytesReservedHi = 1 << 15,
  }

  public enum PeFormat : uint
  {
    Pe32 = 0x10b,
    Pe32Plus = 0x20b,
    Rom = 0x107,
  }

  public class OptionalHeader32
  {
    public uint ImageBase { get; set; }

    public uint SectionAlignment { get; set; }

    public uint FileAlignment { get; set; }

    public ushort MajorOperatingSystemVersion { get; set; }

    public ushort MinorOperatingSystemVersion { get; set; }

    public ushort MajorImageVersion { get; set; }

    public ushort MinorImageVersion { get; set; }


  }
}
