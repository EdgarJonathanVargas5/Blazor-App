namespace TechNotes.Application.Abstractions.RequestHandling;

public interface IQuery<IResponse> : IRequest<Result<IResponse>>
{

}
