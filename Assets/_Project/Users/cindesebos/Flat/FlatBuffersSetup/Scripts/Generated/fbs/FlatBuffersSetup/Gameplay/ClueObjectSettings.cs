// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace FlatBuffersSetup.Gameplay
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct ClueObjectSettings : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_25_2_10(); }
  public static ClueObjectSettings GetRootAsClueObjectSettings(ByteBuffer _bb) { return GetRootAsClueObjectSettings(_bb, new ClueObjectSettings()); }
  public static ClueObjectSettings GetRootAsClueObjectSettings(ByteBuffer _bb, ClueObjectSettings obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ClueObjectSettings __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string TypeId { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTypeIdBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetTypeIdBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetTypeIdArray() { return __p.__vector_as_array<byte>(4); }
  public string TitleLid { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTitleLidBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetTitleLidBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetTitleLidArray() { return __p.__vector_as_array<byte>(6); }

  public static Offset<FlatBuffersSetup.Gameplay.ClueObjectSettings> CreateClueObjectSettings(FlatBufferBuilder builder,
      StringOffset typeIdOffset = default(StringOffset),
      StringOffset titleLidOffset = default(StringOffset)) {
    builder.StartTable(2);
    ClueObjectSettings.AddTitleLid(builder, titleLidOffset);
    ClueObjectSettings.AddTypeId(builder, typeIdOffset);
    return ClueObjectSettings.EndClueObjectSettings(builder);
  }

  public static void StartClueObjectSettings(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddTypeId(FlatBufferBuilder builder, StringOffset typeIdOffset) { builder.AddOffset(0, typeIdOffset.Value, 0); }
  public static void AddTitleLid(FlatBufferBuilder builder, StringOffset titleLidOffset) { builder.AddOffset(1, titleLidOffset.Value, 0); }
  public static Offset<FlatBuffersSetup.Gameplay.ClueObjectSettings> EndClueObjectSettings(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<FlatBuffersSetup.Gameplay.ClueObjectSettings>(o);
  }
  public ClueObjectSettingsT UnPack() {
    var _o = new ClueObjectSettingsT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(ClueObjectSettingsT _o) {
    _o.TypeId = this.TypeId;
    _o.TitleLid = this.TitleLid;
  }
  public static Offset<FlatBuffersSetup.Gameplay.ClueObjectSettings> Pack(FlatBufferBuilder builder, ClueObjectSettingsT _o) {
    if (_o == null) return default(Offset<FlatBuffersSetup.Gameplay.ClueObjectSettings>);
    var _typeId = _o.TypeId == null ? default(StringOffset) : builder.CreateString(_o.TypeId);
    var _titleLid = _o.TitleLid == null ? default(StringOffset) : builder.CreateString(_o.TitleLid);
    return CreateClueObjectSettings(
      builder,
      _typeId,
      _titleLid);
  }
}

public class ClueObjectSettingsT
{
  public string TypeId { get; set; }
  public string TitleLid { get; set; }

  public ClueObjectSettingsT() {
    this.TypeId = null;
    this.TitleLid = null;
  }
}


static public class ClueObjectSettingsVerify
{
  static public bool Verify(Google.FlatBuffers.Verifier verifier, uint tablePos)
  {
    return verifier.VerifyTableStart(tablePos)
      && verifier.VerifyString(tablePos, 4 /*TypeId*/, false)
      && verifier.VerifyString(tablePos, 6 /*TitleLid*/, false)
      && verifier.VerifyTableEnd(tablePos);
  }
}

}
