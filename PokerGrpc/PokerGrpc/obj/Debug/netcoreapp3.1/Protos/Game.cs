// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/game.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace PokerGrpc {

  /// <summary>Holder for reflection information generated from Protos/game.proto</summary>
  public static partial class GameReflection {

    #region Descriptor
    /// <summary>File descriptor for Protos/game.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GameReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChFQcm90b3MvZ2FtZS5wcm90byIrCg5OZXdHYW1lUmVxdWVzdBIZCgdncGxh",
            "eWVyGAEgASgLMgguR1BsYXllciI9Cg9Kb2luR2FtZVJlcXVlc3QSDwoHZ2Ft",
            "ZVBpbhgBIAEoBRIZCgdncGxheWVyGAIgASgLMgguR1BsYXllciKOAQoJR2Ft",
            "ZUxvYmJ5Eg8KB2dhbWVQaW4YASABKAUSGgoIZ3BsYXllcnMYAiADKAsyCC5H",
            "UGxheWVyEhcKBXRvQWN0GAMgASgLMgguR1BsYXllchISCgp0YWJsZUNhcmRz",
            "GAQgASgJEgsKA3BvdBgFIAEoARILCgNiZXQYBiABKAESDQoFYmxpbmQYByAB",
            "KAUibQoHR1BsYXllchIOCgZ3YWxsZXQYAiABKAESDAoEbmFtZRgBIAEoCRIT",
            "Cgtpc1Jvb21Pd25lchgDIAEoCBIMCgRoYW5kGAQgASgJEhEKCWJlc3RDb21i",
            "bxgFIAEoCRIOCgZhY3Rpb24YBiABKAUyjQEKBEdhbWUSLAoNQ3JlYXRlTmV3",
            "R2FtZRIPLk5ld0dhbWVSZXF1ZXN0GgouR2FtZUxvYmJ5EigKCEpvaW5HYW1l",
            "EhAuSm9pbkdhbWVSZXF1ZXN0GgouR2FtZUxvYmJ5Ei0KC1N0YXJ0U3RyZWFt",
            "EhAuSm9pbkdhbWVSZXF1ZXN0GgouR2FtZUxvYmJ5MAFCDKoCCVBva2VyR3Jw",
            "Y2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::PokerGrpc.NewGameRequest), global::PokerGrpc.NewGameRequest.Parser, new[]{ "Gplayer" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::PokerGrpc.JoinGameRequest), global::PokerGrpc.JoinGameRequest.Parser, new[]{ "GamePin", "Gplayer" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::PokerGrpc.GameLobby), global::PokerGrpc.GameLobby.Parser, new[]{ "GamePin", "Gplayers", "ToAct", "TableCards", "Pot", "Bet", "Blind" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::PokerGrpc.GPlayer), global::PokerGrpc.GPlayer.Parser, new[]{ "Wallet", "Name", "IsRoomOwner", "Hand", "BestCombo", "Action" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class NewGameRequest : pb::IMessage<NewGameRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<NewGameRequest> _parser = new pb::MessageParser<NewGameRequest>(() => new NewGameRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<NewGameRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PokerGrpc.GameReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public NewGameRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public NewGameRequest(NewGameRequest other) : this() {
      gplayer_ = other.gplayer_ != null ? other.gplayer_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public NewGameRequest Clone() {
      return new NewGameRequest(this);
    }

    /// <summary>Field number for the "gplayer" field.</summary>
    public const int GplayerFieldNumber = 1;
    private global::PokerGrpc.GPlayer gplayer_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::PokerGrpc.GPlayer Gplayer {
      get { return gplayer_; }
      set {
        gplayer_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as NewGameRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(NewGameRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Gplayer, other.Gplayer)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (gplayer_ != null) hash ^= Gplayer.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (gplayer_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Gplayer);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (gplayer_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Gplayer);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (gplayer_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Gplayer);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(NewGameRequest other) {
      if (other == null) {
        return;
      }
      if (other.gplayer_ != null) {
        if (gplayer_ == null) {
          Gplayer = new global::PokerGrpc.GPlayer();
        }
        Gplayer.MergeFrom(other.Gplayer);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (gplayer_ == null) {
              Gplayer = new global::PokerGrpc.GPlayer();
            }
            input.ReadMessage(Gplayer);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            if (gplayer_ == null) {
              Gplayer = new global::PokerGrpc.GPlayer();
            }
            input.ReadMessage(Gplayer);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class JoinGameRequest : pb::IMessage<JoinGameRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<JoinGameRequest> _parser = new pb::MessageParser<JoinGameRequest>(() => new JoinGameRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<JoinGameRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PokerGrpc.GameReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public JoinGameRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public JoinGameRequest(JoinGameRequest other) : this() {
      gamePin_ = other.gamePin_;
      gplayer_ = other.gplayer_ != null ? other.gplayer_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public JoinGameRequest Clone() {
      return new JoinGameRequest(this);
    }

    /// <summary>Field number for the "gamePin" field.</summary>
    public const int GamePinFieldNumber = 1;
    private int gamePin_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int GamePin {
      get { return gamePin_; }
      set {
        gamePin_ = value;
      }
    }

    /// <summary>Field number for the "gplayer" field.</summary>
    public const int GplayerFieldNumber = 2;
    private global::PokerGrpc.GPlayer gplayer_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::PokerGrpc.GPlayer Gplayer {
      get { return gplayer_; }
      set {
        gplayer_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as JoinGameRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(JoinGameRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (GamePin != other.GamePin) return false;
      if (!object.Equals(Gplayer, other.Gplayer)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (GamePin != 0) hash ^= GamePin.GetHashCode();
      if (gplayer_ != null) hash ^= Gplayer.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (GamePin != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(GamePin);
      }
      if (gplayer_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Gplayer);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (GamePin != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(GamePin);
      }
      if (gplayer_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Gplayer);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (GamePin != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(GamePin);
      }
      if (gplayer_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Gplayer);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(JoinGameRequest other) {
      if (other == null) {
        return;
      }
      if (other.GamePin != 0) {
        GamePin = other.GamePin;
      }
      if (other.gplayer_ != null) {
        if (gplayer_ == null) {
          Gplayer = new global::PokerGrpc.GPlayer();
        }
        Gplayer.MergeFrom(other.Gplayer);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            GamePin = input.ReadInt32();
            break;
          }
          case 18: {
            if (gplayer_ == null) {
              Gplayer = new global::PokerGrpc.GPlayer();
            }
            input.ReadMessage(Gplayer);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            GamePin = input.ReadInt32();
            break;
          }
          case 18: {
            if (gplayer_ == null) {
              Gplayer = new global::PokerGrpc.GPlayer();
            }
            input.ReadMessage(Gplayer);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class GameLobby : pb::IMessage<GameLobby>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<GameLobby> _parser = new pb::MessageParser<GameLobby>(() => new GameLobby());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GameLobby> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PokerGrpc.GameReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GameLobby() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GameLobby(GameLobby other) : this() {
      gamePin_ = other.gamePin_;
      gplayers_ = other.gplayers_.Clone();
      toAct_ = other.toAct_ != null ? other.toAct_.Clone() : null;
      tableCards_ = other.tableCards_;
      pot_ = other.pot_;
      bet_ = other.bet_;
      blind_ = other.blind_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GameLobby Clone() {
      return new GameLobby(this);
    }

    /// <summary>Field number for the "gamePin" field.</summary>
    public const int GamePinFieldNumber = 1;
    private int gamePin_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int GamePin {
      get { return gamePin_; }
      set {
        gamePin_ = value;
      }
    }

    /// <summary>Field number for the "gplayers" field.</summary>
    public const int GplayersFieldNumber = 2;
    private static readonly pb::FieldCodec<global::PokerGrpc.GPlayer> _repeated_gplayers_codec
        = pb::FieldCodec.ForMessage(18, global::PokerGrpc.GPlayer.Parser);
    private readonly pbc::RepeatedField<global::PokerGrpc.GPlayer> gplayers_ = new pbc::RepeatedField<global::PokerGrpc.GPlayer>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::PokerGrpc.GPlayer> Gplayers {
      get { return gplayers_; }
    }

    /// <summary>Field number for the "toAct" field.</summary>
    public const int ToActFieldNumber = 3;
    private global::PokerGrpc.GPlayer toAct_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::PokerGrpc.GPlayer ToAct {
      get { return toAct_; }
      set {
        toAct_ = value;
      }
    }

    /// <summary>Field number for the "tableCards" field.</summary>
    public const int TableCardsFieldNumber = 4;
    private string tableCards_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string TableCards {
      get { return tableCards_; }
      set {
        tableCards_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "pot" field.</summary>
    public const int PotFieldNumber = 5;
    private double pot_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public double Pot {
      get { return pot_; }
      set {
        pot_ = value;
      }
    }

    /// <summary>Field number for the "bet" field.</summary>
    public const int BetFieldNumber = 6;
    private double bet_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public double Bet {
      get { return bet_; }
      set {
        bet_ = value;
      }
    }

    /// <summary>Field number for the "blind" field.</summary>
    public const int BlindFieldNumber = 7;
    private int blind_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Blind {
      get { return blind_; }
      set {
        blind_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GameLobby);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GameLobby other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (GamePin != other.GamePin) return false;
      if(!gplayers_.Equals(other.gplayers_)) return false;
      if (!object.Equals(ToAct, other.ToAct)) return false;
      if (TableCards != other.TableCards) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(Pot, other.Pot)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(Bet, other.Bet)) return false;
      if (Blind != other.Blind) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (GamePin != 0) hash ^= GamePin.GetHashCode();
      hash ^= gplayers_.GetHashCode();
      if (toAct_ != null) hash ^= ToAct.GetHashCode();
      if (TableCards.Length != 0) hash ^= TableCards.GetHashCode();
      if (Pot != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(Pot);
      if (Bet != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(Bet);
      if (Blind != 0) hash ^= Blind.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (GamePin != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(GamePin);
      }
      gplayers_.WriteTo(output, _repeated_gplayers_codec);
      if (toAct_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(ToAct);
      }
      if (TableCards.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(TableCards);
      }
      if (Pot != 0D) {
        output.WriteRawTag(41);
        output.WriteDouble(Pot);
      }
      if (Bet != 0D) {
        output.WriteRawTag(49);
        output.WriteDouble(Bet);
      }
      if (Blind != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(Blind);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (GamePin != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(GamePin);
      }
      gplayers_.WriteTo(ref output, _repeated_gplayers_codec);
      if (toAct_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(ToAct);
      }
      if (TableCards.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(TableCards);
      }
      if (Pot != 0D) {
        output.WriteRawTag(41);
        output.WriteDouble(Pot);
      }
      if (Bet != 0D) {
        output.WriteRawTag(49);
        output.WriteDouble(Bet);
      }
      if (Blind != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(Blind);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (GamePin != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(GamePin);
      }
      size += gplayers_.CalculateSize(_repeated_gplayers_codec);
      if (toAct_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ToAct);
      }
      if (TableCards.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(TableCards);
      }
      if (Pot != 0D) {
        size += 1 + 8;
      }
      if (Bet != 0D) {
        size += 1 + 8;
      }
      if (Blind != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Blind);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GameLobby other) {
      if (other == null) {
        return;
      }
      if (other.GamePin != 0) {
        GamePin = other.GamePin;
      }
      gplayers_.Add(other.gplayers_);
      if (other.toAct_ != null) {
        if (toAct_ == null) {
          ToAct = new global::PokerGrpc.GPlayer();
        }
        ToAct.MergeFrom(other.ToAct);
      }
      if (other.TableCards.Length != 0) {
        TableCards = other.TableCards;
      }
      if (other.Pot != 0D) {
        Pot = other.Pot;
      }
      if (other.Bet != 0D) {
        Bet = other.Bet;
      }
      if (other.Blind != 0) {
        Blind = other.Blind;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            GamePin = input.ReadInt32();
            break;
          }
          case 18: {
            gplayers_.AddEntriesFrom(input, _repeated_gplayers_codec);
            break;
          }
          case 26: {
            if (toAct_ == null) {
              ToAct = new global::PokerGrpc.GPlayer();
            }
            input.ReadMessage(ToAct);
            break;
          }
          case 34: {
            TableCards = input.ReadString();
            break;
          }
          case 41: {
            Pot = input.ReadDouble();
            break;
          }
          case 49: {
            Bet = input.ReadDouble();
            break;
          }
          case 56: {
            Blind = input.ReadInt32();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            GamePin = input.ReadInt32();
            break;
          }
          case 18: {
            gplayers_.AddEntriesFrom(ref input, _repeated_gplayers_codec);
            break;
          }
          case 26: {
            if (toAct_ == null) {
              ToAct = new global::PokerGrpc.GPlayer();
            }
            input.ReadMessage(ToAct);
            break;
          }
          case 34: {
            TableCards = input.ReadString();
            break;
          }
          case 41: {
            Pot = input.ReadDouble();
            break;
          }
          case 49: {
            Bet = input.ReadDouble();
            break;
          }
          case 56: {
            Blind = input.ReadInt32();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class GPlayer : pb::IMessage<GPlayer>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<GPlayer> _parser = new pb::MessageParser<GPlayer>(() => new GPlayer());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GPlayer> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PokerGrpc.GameReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GPlayer() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GPlayer(GPlayer other) : this() {
      wallet_ = other.wallet_;
      name_ = other.name_;
      isRoomOwner_ = other.isRoomOwner_;
      hand_ = other.hand_;
      bestCombo_ = other.bestCombo_;
      action_ = other.action_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GPlayer Clone() {
      return new GPlayer(this);
    }

    /// <summary>Field number for the "wallet" field.</summary>
    public const int WalletFieldNumber = 2;
    private double wallet_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public double Wallet {
      get { return wallet_; }
      set {
        wallet_ = value;
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "isRoomOwner" field.</summary>
    public const int IsRoomOwnerFieldNumber = 3;
    private bool isRoomOwner_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool IsRoomOwner {
      get { return isRoomOwner_; }
      set {
        isRoomOwner_ = value;
      }
    }

    /// <summary>Field number for the "hand" field.</summary>
    public const int HandFieldNumber = 4;
    private string hand_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Hand {
      get { return hand_; }
      set {
        hand_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "bestCombo" field.</summary>
    public const int BestComboFieldNumber = 5;
    private string bestCombo_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string BestCombo {
      get { return bestCombo_; }
      set {
        bestCombo_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "action" field.</summary>
    public const int ActionFieldNumber = 6;
    private int action_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Action {
      get { return action_; }
      set {
        action_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GPlayer);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GPlayer other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(Wallet, other.Wallet)) return false;
      if (Name != other.Name) return false;
      if (IsRoomOwner != other.IsRoomOwner) return false;
      if (Hand != other.Hand) return false;
      if (BestCombo != other.BestCombo) return false;
      if (Action != other.Action) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Wallet != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(Wallet);
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (IsRoomOwner != false) hash ^= IsRoomOwner.GetHashCode();
      if (Hand.Length != 0) hash ^= Hand.GetHashCode();
      if (BestCombo.Length != 0) hash ^= BestCombo.GetHashCode();
      if (Action != 0) hash ^= Action.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (Wallet != 0D) {
        output.WriteRawTag(17);
        output.WriteDouble(Wallet);
      }
      if (IsRoomOwner != false) {
        output.WriteRawTag(24);
        output.WriteBool(IsRoomOwner);
      }
      if (Hand.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Hand);
      }
      if (BestCombo.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(BestCombo);
      }
      if (Action != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(Action);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (Wallet != 0D) {
        output.WriteRawTag(17);
        output.WriteDouble(Wallet);
      }
      if (IsRoomOwner != false) {
        output.WriteRawTag(24);
        output.WriteBool(IsRoomOwner);
      }
      if (Hand.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Hand);
      }
      if (BestCombo.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(BestCombo);
      }
      if (Action != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(Action);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Wallet != 0D) {
        size += 1 + 8;
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (IsRoomOwner != false) {
        size += 1 + 1;
      }
      if (Hand.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Hand);
      }
      if (BestCombo.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(BestCombo);
      }
      if (Action != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Action);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GPlayer other) {
      if (other == null) {
        return;
      }
      if (other.Wallet != 0D) {
        Wallet = other.Wallet;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.IsRoomOwner != false) {
        IsRoomOwner = other.IsRoomOwner;
      }
      if (other.Hand.Length != 0) {
        Hand = other.Hand;
      }
      if (other.BestCombo.Length != 0) {
        BestCombo = other.BestCombo;
      }
      if (other.Action != 0) {
        Action = other.Action;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 17: {
            Wallet = input.ReadDouble();
            break;
          }
          case 24: {
            IsRoomOwner = input.ReadBool();
            break;
          }
          case 34: {
            Hand = input.ReadString();
            break;
          }
          case 42: {
            BestCombo = input.ReadString();
            break;
          }
          case 48: {
            Action = input.ReadInt32();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 17: {
            Wallet = input.ReadDouble();
            break;
          }
          case 24: {
            IsRoomOwner = input.ReadBool();
            break;
          }
          case 34: {
            Hand = input.ReadString();
            break;
          }
          case 42: {
            BestCombo = input.ReadString();
            break;
          }
          case 48: {
            Action = input.ReadInt32();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
