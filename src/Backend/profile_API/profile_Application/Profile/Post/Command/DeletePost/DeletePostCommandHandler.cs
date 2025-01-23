using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using profile_Application.Core.Commands.Contracts;
using profile_Core.Profile;
using profile_Domain.Exception;

namespace profile_Application.Profile.Post.Command.DeletePost;

public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand, bool>
{
    private readonly IPostService _service;
    private readonly ILogger<DeletePostCommandHandler> _logger;

    public DeletePostCommandHandler(IPostService service, ILogger<DeletePostCommandHandler> logger)
    {
        _service = service;
        _logger = logger;
    }
    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Processing command: {nameof(DeletePostCommand)}");
      var result = await _service.DeletePost(request.PostId, request.UserId);
      if (result.IsFailure)
      {
          throw new ProfileException(400, result.Error);
      }
      _logger.LogInformation($"Processing command: {nameof(DeletePostCommand)}");
      return result.Value;
    }
}