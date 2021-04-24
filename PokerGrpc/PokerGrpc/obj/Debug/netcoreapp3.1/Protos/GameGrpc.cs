// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/game.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace PokerGrpc {
  public static partial class Game
  {
    static readonly string __ServiceName = "Game";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::PokerGrpc.NewGameRequest> __Marshaller_NewGameRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PokerGrpc.NewGameRequest.Parser));
    static readonly grpc::Marshaller<global::PokerGrpc.GameLobby> __Marshaller_GameLobby = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PokerGrpc.GameLobby.Parser));
    static readonly grpc::Marshaller<global::PokerGrpc.JoinGameRequest> __Marshaller_JoinGameRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PokerGrpc.JoinGameRequest.Parser));
    static readonly grpc::Marshaller<global::PokerGrpc.ActionRequest> __Marshaller_ActionRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PokerGrpc.ActionRequest.Parser));
    static readonly grpc::Marshaller<global::PokerGrpc.ActionResponse> __Marshaller_ActionResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PokerGrpc.ActionResponse.Parser));

    static readonly grpc::Method<global::PokerGrpc.NewGameRequest, global::PokerGrpc.GameLobby> __Method_CreateNewGame = new grpc::Method<global::PokerGrpc.NewGameRequest, global::PokerGrpc.GameLobby>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateNewGame",
        __Marshaller_NewGameRequest,
        __Marshaller_GameLobby);

    static readonly grpc::Method<global::PokerGrpc.JoinGameRequest, global::PokerGrpc.GameLobby> __Method_JoinGame = new grpc::Method<global::PokerGrpc.JoinGameRequest, global::PokerGrpc.GameLobby>(
        grpc::MethodType.Unary,
        __ServiceName,
        "JoinGame",
        __Marshaller_JoinGameRequest,
        __Marshaller_GameLobby);

    static readonly grpc::Method<global::PokerGrpc.JoinGameRequest, global::PokerGrpc.GameLobby> __Method_StartStream = new grpc::Method<global::PokerGrpc.JoinGameRequest, global::PokerGrpc.GameLobby>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "StartStream",
        __Marshaller_JoinGameRequest,
        __Marshaller_GameLobby);

    static readonly grpc::Method<global::PokerGrpc.ActionRequest, global::PokerGrpc.ActionResponse> __Method_Action = new grpc::Method<global::PokerGrpc.ActionRequest, global::PokerGrpc.ActionResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Action",
        __Marshaller_ActionRequest,
        __Marshaller_ActionResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::PokerGrpc.GameReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Game</summary>
    [grpc::BindServiceMethod(typeof(Game), "BindService")]
    public abstract partial class GameBase
    {
      public virtual global::System.Threading.Tasks.Task<global::PokerGrpc.GameLobby> CreateNewGame(global::PokerGrpc.NewGameRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::PokerGrpc.GameLobby> JoinGame(global::PokerGrpc.JoinGameRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task StartStream(global::PokerGrpc.JoinGameRequest request, grpc::IServerStreamWriter<global::PokerGrpc.GameLobby> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::PokerGrpc.ActionResponse> Action(global::PokerGrpc.ActionRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(GameBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_CreateNewGame, serviceImpl.CreateNewGame)
          .AddMethod(__Method_JoinGame, serviceImpl.JoinGame)
          .AddMethod(__Method_StartStream, serviceImpl.StartStream)
          .AddMethod(__Method_Action, serviceImpl.Action).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GameBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_CreateNewGame, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::PokerGrpc.NewGameRequest, global::PokerGrpc.GameLobby>(serviceImpl.CreateNewGame));
      serviceBinder.AddMethod(__Method_JoinGame, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::PokerGrpc.JoinGameRequest, global::PokerGrpc.GameLobby>(serviceImpl.JoinGame));
      serviceBinder.AddMethod(__Method_StartStream, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::PokerGrpc.JoinGameRequest, global::PokerGrpc.GameLobby>(serviceImpl.StartStream));
      serviceBinder.AddMethod(__Method_Action, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::PokerGrpc.ActionRequest, global::PokerGrpc.ActionResponse>(serviceImpl.Action));
    }

  }
}
#endregion
