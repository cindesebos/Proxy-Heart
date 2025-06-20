// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace FlatBuffersSetup.Gameplay
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct DialogueSettings : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_25_2_10(); }
  public static DialogueSettings GetRootAsDialogueSettings(ByteBuffer _bb) { return GetRootAsDialogueSettings(_bb, new DialogueSettings()); }
  public static DialogueSettings GetRootAsDialogueSettings(ByteBuffer _bb, DialogueSettings obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public DialogueSettings __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string TypeId { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTypeIdBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetTypeIdBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetTypeIdArray() { return __p.__vector_as_array<byte>(4); }
  public string MessageLid { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMessageLidBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetMessageLidBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetMessageLidArray() { return __p.__vector_as_array<byte>(6); }
  public FlatBuffersSetup.Gameplay.AuthorType AuthorType { get { int o = __p.__offset(8); return o != 0 ? (FlatBuffersSetup.Gameplay.AuthorType)__p.bb.GetSbyte(o + __p.bb_pos) : FlatBuffersSetup.Gameplay.AuthorType.Player; } }

  public static Offset<FlatBuffersSetup.Gameplay.DialogueSettings> CreateDialogueSettings(FlatBufferBuilder builder,
      StringOffset typeIdOffset = default(StringOffset),
      StringOffset messageLidOffset = default(StringOffset),
      FlatBuffersSetup.Gameplay.AuthorType authorType = FlatBuffersSetup.Gameplay.AuthorType.Player) {
    builder.StartTable(3);
    DialogueSettings.AddMessageLid(builder, messageLidOffset);
    DialogueSettings.AddTypeId(builder, typeIdOffset);
    DialogueSettings.AddAuthorType(builder, authorType);
    return DialogueSettings.EndDialogueSettings(builder);
  }

  public static void StartDialogueSettings(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddTypeId(FlatBufferBuilder builder, StringOffset typeIdOffset) { builder.AddOffset(0, typeIdOffset.Value, 0); }
  public static void AddMessageLid(FlatBufferBuilder builder, StringOffset messageLidOffset) { builder.AddOffset(1, messageLidOffset.Value, 0); }
  public static void AddAuthorType(FlatBufferBuilder builder, FlatBuffersSetup.Gameplay.AuthorType authorType) { builder.AddSbyte(2, (sbyte)authorType, 0); }
  public static Offset<FlatBuffersSetup.Gameplay.DialogueSettings> EndDialogueSettings(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<FlatBuffersSetup.Gameplay.DialogueSettings>(o);
  }
  public DialogueSettingsT UnPack() {
    var _o = new DialogueSettingsT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(DialogueSettingsT _o) {
    _o.TypeId = this.TypeId;
    _o.MessageLid = this.MessageLid;
    _o.AuthorType = this.AuthorType;
  }
  public static Offset<FlatBuffersSetup.Gameplay.DialogueSettings> Pack(FlatBufferBuilder builder, DialogueSettingsT _o) {
    if (_o == null) return default(Offset<FlatBuffersSetup.Gameplay.DialogueSettings>);
    var _typeId = _o.TypeId == null ? default(StringOffset) : builder.CreateString(_o.TypeId);
    var _messageLid = _o.MessageLid == null ? default(StringOffset) : builder.CreateString(_o.MessageLid);
    return CreateDialogueSettings(
      builder,
      _typeId,
      _messageLid,
      _o.AuthorType);
  }
}

public class DialogueSettingsT
{
  public string TypeId { get; set; }
  public string MessageLid { get; set; }
  public FlatBuffersSetup.Gameplay.AuthorType AuthorType { get; set; }

  public DialogueSettingsT() {
    this.TypeId = null;
    this.MessageLid = null;
    this.AuthorType = FlatBuffersSetup.Gameplay.AuthorType.Player;
  }
}


static public class DialogueSettingsVerify
{
  static public bool Verify(Google.FlatBuffers.Verifier verifier, uint tablePos)
  {
    return verifier.VerifyTableStart(tablePos)
      && verifier.VerifyString(tablePos, 4 /*TypeId*/, false)
      && verifier.VerifyString(tablePos, 6 /*MessageLid*/, false)
      && verifier.VerifyField(tablePos, 8 /*AuthorType*/, 1 /*FlatBuffersSetup.Gameplay.AuthorType*/, 1, false)
      && verifier.VerifyTableEnd(tablePos);
  }
}

}
