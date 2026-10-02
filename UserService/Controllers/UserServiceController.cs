using Grpc.Core;
using UserService.Application.Users.Create;
using UserService.Application.Users.GetById;
using UserService.Application.Users.GetByName;
using UserService.Application.Users.Delete;
using UserService.Application.Users.Update;
using UserService.Mapping;
using GrpcContracts = UserService.Contracts;

namespace UserService.Controllers;

public sealed class UserServiceController(
    CreateUserHandler createUserHandler,
    CreateUserRequestMapper createUserRequestMapper,
    GetUserByIdHandler getUserByIdHandler,
    GetUserByIdRequestMapper getUserByIdRequestMapper,
    GetUserByNameHandler getUserByNameHandler,
    GetUserByNameRequestMapper getUserByNameRequestMapper,
    UpdateUserHandler updateUserHandler,
    UpdateUserRequestMapper updateUserRequestMapper,
    DeleteUserHandler deleteUserHandler,
    DeleteUserRequestMapper deleteUserRequestMapper,
    UserGrpcMapper userGrpcMapper) : GrpcContracts.UserService.UserServiceBase
{
    public override async Task<GrpcContracts.CreateUserResponse> CreateUser(
        GrpcContracts.CreateUserRequest request,
        ServerCallContext context)
    {
        var command = createUserRequestMapper.ToCommand(request);
        var result = await createUserHandler.ExecuteAsync(command, context.CancellationToken);

        return result switch
        {
            CreateUserResult.Success(var user) =>
                new GrpcContracts.CreateUserResponse { User = userGrpcMapper.ToProto(user) },
            CreateUserResult.LoginCollision =>
                throw new RpcException(new Status(StatusCode.AlreadyExists, "User already exists")),
            CreateUserResult.ValidationFailed(var errors) =>
                throw new RpcException(new Status(StatusCode.InvalidArgument, string.Join("; ", errors))),
            _ => throw new RpcException(new Status(StatusCode.Internal, "Unexpected result"))
        };
    }

    public override async Task<GrpcContracts.GetUserByIdResponse> GetUserById(
        GrpcContracts.GetUserByIdRequest request,
        ServerCallContext context)
    {
        var query = getUserByIdRequestMapper.ToQuery(request);
        var result = await getUserByIdHandler.ExecuteAsync(query, context.CancellationToken);

        return result switch
        {
            GetUserByIdResult.Success(var user) =>
                new GrpcContracts.GetUserByIdResponse { User = userGrpcMapper.ToProto(user) },
            GetUserByIdResult.NotFound =>
                throw new RpcException(new Status(StatusCode.NotFound, "User not found")),
            _ => throw new RpcException(new Status(StatusCode.Internal, "Unexpected result"))
        };
    }

    public override async Task<GrpcContracts.GetUserByNameResponse> GetUserByName(
        GrpcContracts.GetUserByNameRequest request,
        ServerCallContext context)
    {
        var query = getUserByNameRequestMapper.ToQuery(request);
        var result = await getUserByNameHandler.ExecuteAsync(query, context.CancellationToken);

        return result switch
        {
            GetUserByNameResult.Success(var user) =>
                new GrpcContracts.GetUserByNameResponse { User = userGrpcMapper.ToProto(user) },
            GetUserByNameResult.NotFound =>
                throw new RpcException(new Status(StatusCode.NotFound, "User not found")),
            _ => throw new RpcException(new Status(StatusCode.Internal, "Unexpected result"))
        };
    }

    public override async Task<GrpcContracts.UpdateUserResponse> UpdateUser(
        GrpcContracts.UpdateUserRequest request,
        ServerCallContext context)
    {
        var command = updateUserRequestMapper.ToCommand(request);
        var result = await updateUserHandler.ExecuteAsync(command, context.CancellationToken);

        return result switch
        {
            UpdateUserResult.Success(var user) =>
                new GrpcContracts.UpdateUserResponse { User = userGrpcMapper.ToProto(user) },
            UpdateUserResult.NotFound =>
                throw new RpcException(new Status(StatusCode.NotFound, "User not found")),
            UpdateUserResult.ValidationFailed(var errors) =>
                throw new RpcException(new Status(StatusCode.InvalidArgument, string.Join("; ", errors))),
            _ => throw new RpcException(new Status(StatusCode.Internal, "Unexpected result"))
        };
    }

    public override async Task<GrpcContracts.DeleteUserResponse> DeleteUser(
        GrpcContracts.DeleteUserRequest request,
        ServerCallContext context)
    {
        var command = deleteUserRequestMapper.ToCommand(request);
        var result = await deleteUserHandler.ExecuteAsync(command, context.CancellationToken);

        return result switch
        {
            DeleteUserResult.Success => new GrpcContracts.DeleteUserResponse(),
            DeleteUserResult.NotFound =>
                throw new RpcException(new Status(StatusCode.NotFound, "User not found")),
            _ => throw new RpcException(new Status(StatusCode.Internal, "Unexpected result"))
        };
    }
}
