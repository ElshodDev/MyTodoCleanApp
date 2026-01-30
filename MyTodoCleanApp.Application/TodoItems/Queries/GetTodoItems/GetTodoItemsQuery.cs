using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTodoCleanApp.Application.Common.Interfaces;

namespace MyTodoCleanApp.Application.TodoItems.Queries.GetTodoItems;

// 1. Request: Parametr sifatida UserId qabul qiladi (ixtiyoriy)
public record GetTodoItemsQuery(Guid? UserId = null) : IRequest<List<TodoItemDto>>;

// 2. DTO: Ma'lumotlarni tashqariga uzatish modeli
public record TodoItemDto(Guid Id, string Title, string Note, bool IsDone, Guid UserId);

// 3. Handler: Ma'lumotlarni filtrlash va bazadan olish mantiqi
public class GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, List<TodoItemDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTodoItemsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TodoItemDto>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        // Bazaga so'rovni shakllantirishni boshlaymiz
        var query = _context.TodoItems.AsNoTracking().AsQueryable();

        // Agar UserId yuborilgan bo'lsa, natijani filtrlaymiz
        if (request.UserId.HasValue)
        {
            query = query.Where(x => x.UserId == request.UserId.Value);
        }

        // Natijani DTO-ga o'girib (Projection) list shaklida qaytaramiz
        return await query
            .Select(x => new TodoItemDto(x.Id, x.Title, x.Note, x.IsDone, x.UserId))
            .ToListAsync(cancellationToken);
    }
}